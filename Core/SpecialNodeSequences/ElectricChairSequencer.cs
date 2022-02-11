﻿using System.Collections;
using DiskCardGame;
using Sirenix.Utilities;
using UnityEngine;
using static GrimoraMod.GrimoraPlugin;

namespace GrimoraMod;

public class ElectricChairSequencer : CardStatBoostSequencer
{
	public static ElectricChairSequencer Instance => FindObjectOfType<ElectricChairSequencer>();

	public IEnumerator StartSequence()
	{
		yield return InitialSetup();

		if (GetValidCards().IsNullOrEmpty())
		{
			// no valid cards
			yield return new WaitForSeconds(1f);
			yield return TextDisplayer.Instance.PlayDialogueEvent(
				"GainConsumablesFull",
				TextDisplayer.MessageAdvanceMode.Input
			);
			yield return new WaitForSeconds(0.5f);
		}
		else
		{
			// play dialogue
			yield return TextDisplayer.Instance.PlayDialogueEvent("StatBoostIntro", TextDisplayer.MessageAdvanceMode.Input);
			yield return WhileNotFinishedBuffingAndDestroyedCardIsNull();
		}

		yield return OutroEnvTeardown();

		if (GameFlowManager.Instance != null)
		{
			GameFlowManager.Instance.TransitionToGameState(GameState.Map);
		}
	}

	private IEnumerator WhileNotFinishedBuffingAndDestroyedCardIsNull()
	{
		yield return confirmStone.WaitUntilConfirmation();
		CardInfo destroyedCard = null;
		bool finishedBuffing = false;
		int numBuffsGiven = 0;
		while (!finishedBuffing && destroyedCard == null)
		{
			numBuffsGiven++;
			selectionSlot.Disable();
			RuleBookController.Instance.SetShown(false);
			yield return new WaitForSeconds(0.25f);
			AudioController.Instance.PlaySound3D(
				"card_blessing",
				MixerGroup.TableObjectsSFX,
				selectionSlot.transform.position
			);
			selectionSlot.Card.Anim.PlayTransformAnimation();
			ApplyModToCard(selectionSlot.Card.Info);
			yield return new WaitForSeconds(0.15f);
			selectionSlot.Card.SetInfo(selectionSlot.Card.Info);
			selectionSlot.Card.SetInteractionEnabled(false);
			yield return new WaitForSeconds(0.75f);

			if (numBuffsGiven == 4)
			{
				break;
			}

			if (!RunState.Run.survivorsDead)
			{
				yield return TextDisplayer.Instance.PlayDialogueEvent("StatBoostPushLuck" + numBuffsGiven,
					TextDisplayer.MessageAdvanceMode.Input);
				yield return new WaitForSeconds(0.1f);
				switch (numBuffsGiven)
				{
					case 1:
						TextDisplayer.Instance.ShowMessage("Push your luck? Or pull away?", Emotion.Neutral,
							TextDisplayer.LetterAnimation.WavyJitter);
						break;
					case 2:
						TextDisplayer.Instance.ShowMessage("Push your luck further? Or run back?", Emotion.Neutral,
							TextDisplayer.LetterAnimation.WavyJitter);
						break;
					case 3:
						TextDisplayer.Instance.ShowMessage("Recklessly continue?", Emotion.Neutral,
							TextDisplayer.LetterAnimation.WavyJitter);
						break;
				}
			}

			bool cancelledByClickingCard = false;
			retrieveCardInteractable.gameObject.SetActive(true);
			retrieveCardInteractable.CursorSelectEnded = null;
			GenericMainInputInteractable genericMainInputInteractable = retrieveCardInteractable;
			genericMainInputInteractable.CursorSelectEnded = (Action<MainInputInteractable>)Delegate.Combine(
				genericMainInputInteractable.CursorSelectEnded,
				(Action<MainInputInteractable>)delegate { cancelledByClickingCard = true; });
			confirmStone.Unpress();
			StartCoroutine(confirmStone.WaitUntilConfirmation());
			yield return new WaitUntil(() =>
				confirmStone.SelectionConfirmed || InputButtons.GetButton(Button.LookDown) ||
				InputButtons.GetButton(Button.Cancel) || cancelledByClickingCard);
			TextDisplayer.Instance.Clear();
			retrieveCardInteractable.gameObject.SetActive(false);
			confirmStone.Disable();
			yield return new WaitForSeconds(0.1f);
			if (confirmStone.SelectionConfirmed)
			{
				float num = 1f - numBuffsGiven * 0.225f;
				if (SeededRandom.Value(SaveManager.SaveFile.GetCurrentRandomSeed()) > num)
				{
					destroyedCard = selectionSlot.Card.Info;
					selectionSlot.Card.Anim.PlayDeathAnimation();
					GrimoraSaveData.Data.deck.RemoveCard(selectionSlot.Card.Info);
					yield return new WaitForSeconds(1f);
				}
			}
			else
			{
				finishedBuffing = true;
			}
		}

		if (destroyedCard != null)
		{
			yield return TextDisplayer.Instance.PlayDialogueEvent(
				"StatBoostCardEaten",
				TextDisplayer.MessageAdvanceMode.Input,
				TextDisplayer.EventIntersectMode.Wait, new[] { selectionSlot.Card.Info.DisplayedNameLocalized }
			);
			yield return new WaitForSeconds(0.1f);
			selectionSlot.DestroyCard();
		}
		else
		{
			yield return TextDisplayer.Instance.PlayDialogueEvent("StatBoostOutro", TextDisplayer.MessageAdvanceMode.Input);

			yield return new WaitForSeconds(0.1f);
			selectionSlot.FlyOffCard();
		}
	}

	private new void OnSlotSelected(MainInputInteractable slot)
	{
		selectionSlot.SetEnabled(false);
		selectionSlot.ShowState(HighlightedInteractable.State.NonInteractable);
		confirmStone.Exit();
		List<CardInfo> validCards = GetValidCards();
		((SelectCardFromDeckSlot)slot).SelectFromCards(validCards, OnSelectionEnded, false);
	}

	private new static void ApplyModToCard(CardInfo card)
	{
		Ability randomSigil = AbilitiesUtil.GetRandomAbility(
			RandomUtils.GenerateRandomSeed(new List<CardInfo> { card }), true
		);
		Log.LogDebug($"[ApplyModToCard] Ability [{randomSigil}]");
		CardModificationInfo cardModificationInfo = new CardModificationInfo
		{
			abilities = new List<Ability> { randomSigil },
			singletonId = "GrimoraMod_ElectricChaired"
		};
		GrimoraSaveUtil.DeckInfo.ModifyCard(card, cardModificationInfo);
	}

	private new static List<CardInfo> GetValidCards()
	{
		List<CardInfo> list = GrimoraSaveUtil.DeckListCopy;
		list.RemoveAll(card => card.Abilities.Count == 4
		                       || card.SpecialAbilities.Contains(SpecialTriggeredAbility.RandomCard)
		                       || card.traits.Contains(Trait.Pelt)
		                       || card.traits.Contains(Trait.Terrain)
		);

		return list;
	}

	private IEnumerator InitialSetup()
	{
		stakeRingParent.SetActive(false);
		campfireLight.gameObject.SetActive(false);
		campfireLight.intensity = 0f;
		campfireCardLight.intensity = 0f;
		selectionSlot.Disable();
		selectionSlot.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.3f);

		ExplorableAreaManager.Instance.HangingLight.gameObject.SetActive(false);
		ExplorableAreaManager.Instance.HandLight.gameObject.SetActive(false);
		ViewManager.Instance.SwitchToView(View.Default, false, true);
		ViewManager.Instance.OffsetPosition(new Vector3(0f, 0f, 2.25f), 0.1f);
		yield return new WaitForSeconds(1f);

		figurines.ForEach(delegate(CompositeFigurine x) { x.gameObject.SetActive(true); });

		stakeRingParent.SetActive(true);
		ExplorableAreaManager.Instance.HandLight.gameObject.SetActive(true);
		campfireLight.gameObject.SetActive(true);
		selectionSlot.gameObject.SetActive(true);
		selectionSlot.RevealAndEnable();
		selectionSlot.ClearDelegates();

		SelectCardFromDeckSlot selectCardFromDeckSlot = selectionSlot;
		selectCardFromDeckSlot.CursorSelectStarted =
			(Action<MainInputInteractable>)Delegate.Combine(
				selectCardFromDeckSlot.CursorSelectStarted,
				new Action<MainInputInteractable>(OnSlotSelected)
			);
		if (UnityEngine.Random.value < 0.25f && VideoCameraRig.Instance != null)
		{
			VideoCameraRig.Instance.PlayCameraAnim("refocus_quick");
		}

		AudioController.Instance.PlaySound3D(
			"campfire_light",
			MixerGroup.TableObjectsSFX,
			selectionSlot.transform.position
		);
		AudioController.Instance.SetLoopAndPlay("campfire_loop", 1);
		AudioController.Instance.SetLoopVolumeImmediate(0f, 1);
		AudioController.Instance.FadeInLoop(0.5f, 0.75f, 1);
		InteractionCursor.Instance.SetEnabled(false);
		yield return new WaitForSeconds(0.25f);

		yield return pile.SpawnCards(GrimoraSaveUtil.DeckList.Count, 0.5f);
		TableRuleBook.Instance.SetOnBoard(true);
		InteractionCursor.Instance.SetEnabled(true);
	}

	private IEnumerator OutroEnvTeardown()
	{
		ViewManager.Instance.SwitchToView(View.Default);
		yield return new WaitForSeconds(0.25f);

		AudioController.Instance.PlaySound3D(
			"campfire_putout",
			MixerGroup.TableObjectsSFX,
			selectionSlot.transform.position
		);
		AudioController.Instance.StopLoop(1);
		campfireLight.gameObject.SetActive(false);
		ExplorableAreaManager.Instance.HandLight.gameObject.SetActive(false);
		yield return pile.DestroyCards();
		yield return new WaitForSeconds(0.2f);

		figurines.ForEach(delegate(CompositeFigurine x) { x.gameObject.SetActive(false); });

		stakeRingParent.SetActive(false);
		confirmStone.SetStoneInactive();
		selectionSlot.gameObject.SetActive(false);

		CustomCoroutine.WaitThenExecute(0.4f, delegate
		{
			ExplorableAreaManager.Instance.HangingLight.intensity = 0f;
			ExplorableAreaManager.Instance.HangingLight.gameObject.SetActive(true);
			ExplorableAreaManager.Instance.HandLight.intensity = 0f;
			ExplorableAreaManager.Instance.HandLight.gameObject.SetActive(true);
		});
	}

	public static void CreateSequencerInScene()
	{
		if (SpecialNodeHandler.Instance is null)
		{
			return;
		}

		Log.LogDebug("[ElectricChair] Creating boneyard burial");
		GameObject cardStatObj = Instantiate(
			PrefabConstants.CardStatBoostSequencer,
			SpecialNodeHandler.Instance.transform
		);
		cardStatObj.name = "ElectricChairSequencer_Grimora";

		Log.LogDebug("[ElectricChair] getting selection slot");
		var selectionSlot = cardStatObj.transform.GetChild(1);

		Log.LogDebug("[ElectricChair] getting stake ring");
		var stakeRing = cardStatObj.transform.Find("StakeRing");

		// destroying things

		Log.LogDebug("[ElectricChair] destroying fireanim");
		Destroy(selectionSlot.GetChild(1).gameObject); //FireAnim 
		for (int i = 0; i < cardStatObj.transform.childCount; i++)
		{
			var child = cardStatObj.transform.GetChild(i);
			if (child.name.Equals("Figurine"))
			{
				Destroy(child.gameObject);
			}
		}

		Log.LogDebug($"[ElectricChair] destroying existing stake rings [{stakeRing.childCount}]");
		for (int i = 0; i < stakeRing.childCount; i++)
		{
			// don't need the stake rings
			Destroy(stakeRing.GetChild(i).gameObject);
		}

		var oldSequencer = cardStatObj.GetComponent<CardStatBoostSequencer>();

		Log.LogDebug("Adding component");
		var newSequencer = cardStatObj.AddComponent<ElectricChairSequencer>();

		Log.LogDebug("Transferring old to new");
		newSequencer.campfireLight = oldSequencer.campfireLight;
		newSequencer.campfireLight.transform.localPosition = new Vector3(0, 6.75f, 0.63f);
		newSequencer.campfireLight.color = new Color(0, 1, 1, 1);
		newSequencer.campfireCardLight = oldSequencer.campfireCardLight;
		newSequencer.campfireCardLight.color = new Color(0, 1, 1, 1);

		newSequencer.confirmStone = oldSequencer.confirmStone;
		newSequencer.confirmStone.confirmView = View.CardMergeSlots;

		newSequencer.figurines = new List<CompositeFigurine>();
		newSequencer.figurines.AddRange(CreateElectricChair(cardStatObj));

		newSequencer.pile = oldSequencer.pile;
		newSequencer.pile.cardbackPrefab = PrefabConstants.GrimoraCardBack;

		newSequencer.selectionSlot = oldSequencer.selectionSlot;
		newSequencer.selectionSlot.transform.localPosition = new Vector3(0, 7, 1);
		newSequencer.selectionSlot.transform.localRotation = Quaternion.Euler(270, 0, 0);
		newSequencer.selectionSlot.cardSelector.selectableCardPrefab = PrefabConstants.GrimoraSelectableCard;
		newSequencer.selectionSlot.pile.cardbackPrefab = PrefabConstants.GrimoraCardBack;

		newSequencer.retrieveCardInteractable = oldSequencer.retrieveCardInteractable;
		newSequencer.stakeRingParent = oldSequencer.stakeRingParent;
		// this will throw an exception if we don't remove the specific renderer for fire anim
		newSequencer.selectionSlot.specificRenderers.RemoveAt(1);

		Log.LogDebug("Destroying old sequencer");
		Destroy(oldSequencer);
	}

	private static List<CompositeFigurine> CreateElectricChair(GameObject cardStatObj)
	{
		Log.LogDebug("[ElectricChair] creating chair");
		CompositeFigurine chairFigurine = Instantiate(
			PrefabConstants.ElectricChair,
			new Vector3(0, 5.85f, 1),
			Quaternion.Euler(0, -90, 0),
			cardStatObj.transform
		).AddComponent<CompositeFigurine>();
		chairFigurine.name = "Electric Chair Figurine";
		chairFigurine.transform.localScale = new Vector3(60, 60, 60);
		chairFigurine.gameObject.SetActive(false);

		CompositeFigurine vfxElectricity = Instantiate(
			AssetUtils.GetPrefab<GameObject>("VfxBoltLightning"),
			new Vector3(0.1f, 7.5f, 1.25f),
			Quaternion.identity,
			cardStatObj.transform
		).AddComponent<CompositeFigurine>();
		vfxElectricity.gameObject.SetActive(false);

		return new List<CompositeFigurine> { chairFigurine, vfxElectricity };
	}
}
