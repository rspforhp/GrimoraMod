﻿using System.Collections;
using DiskCardGame;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using static GrimoraMod.GrimoraPlugin;

namespace GrimoraMod;

public class SkinCrawler : AbilityBehaviour
{
	public const string ModSingletonId = "GrimoraMod_SkinCrawler";
	
	public static Ability ability;

	public override Ability Ability => ability;

	private SkinCrawlerSlot _slotHidingUnderCard = null;

	public static bool SlotDoesNotHaveSkinCrawler(CardSlot cardSlot)
	{
		if (cardSlot && cardSlot.Card)
		{
			Log.LogDebug($"[Crawler.SlotDoesNotHave] {cardSlot.Card.GetNameAndSlot()}");
			var crawlerSlot = cardSlot.GetComponentInChildren<SkinCrawlerSlot>();
			if (crawlerSlot)
			{
				Log.LogDebug($"[Crawler.SlotDoesNotHave] --> has crawler slot. Is HidingOnSlot null? [{crawlerSlot.hidingOnSlot.IsNull()}] ");
				return crawlerSlot.hidingOnSlot.IsNull();
			}

			Log.LogDebug($"[Crawler.SlotDoesNotHave] -> Has no skin crawler ");
			return true;
		}

		return false;
	}

	public CardSlot FindValidHostSlot()
	{
		Log.LogDebug($"[Crawler.FindValid] Checking if adj slots from [{Card.Slot}] are not null");
		CardSlot slotToPick = null;

		CardSlot toLeftSlot = BoardManager.Instance.GetAdjacent(Card.Slot, true);
		CardSlot toRightSlot = BoardManager.Instance.GetAdjacent(Card.Slot, false);
		Log.LogDebug($"[Crawler.FindValid] Slot for SkinCrawler [{Card.Slot}] toLeftSlot [{toLeftSlot}] toRightSlot [{toRightSlot}]");
		bool toLeftSlotHasNoCrawler = SlotDoesNotHaveSkinCrawler(toLeftSlot);
		bool toRightSlotHasNoCrawler = SlotDoesNotHaveSkinCrawler(toRightSlot);

		if (toLeftSlotHasNoCrawler)
		{
			slotToPick = toLeftSlot;
			Log.LogDebug($"[Crawler.FindValid] LeftSlot has card [{slotToPick.Card.GetNameAndSlot()}]");
		}
		else if (toRightSlotHasNoCrawler)
		{
			slotToPick = toRightSlot;
			Log.LogDebug($"[Crawler.FindValid] RightSlot has card [{slotToPick.Card.GetNameAndSlot()}]");
		}

		return slotToPick;
	}

	public IEnumerator AssignSkinCrawlerCardToHost(CardSlot slotToPick)
	{
		if (slotToPick)
		{
			PlayableCard cardToPick = slotToPick.Card;
			CardSlot cardSlotToPick = slotToPick.Card.Slot;

			Log.LogDebug($"[Crawler.AssignSkinCrawlerCardToHost] Playing animation sequence");
			yield return ApplyModAndDoAnimationSequence(cardToPick, cardSlotToPick);

			Log.LogDebug($"[Crawler.AssignSkinCrawlerCardToHost] Nulling slots out");
			BoardManager.Instance.GetSlots(!Card.OpponentCard)[Card.Slot.Index].Card = null;
			Card.slot = null;
			Log.LogDebug($"[Crawler.AssignSkinCrawlerCardToHost] Setting up slot.");
			_slotHidingUnderCard = SkinCrawlerSlot.SetupSlot(Card, cardToPick);

			yield return new WaitForSeconds(0.25f);
			ViewManager.Instance.SwitchToView(View.Default);
			ViewManager.Instance.SetViewUnlocked();
		}
	}

	public IEnumerator ApplyModAndDoAnimationSequence(PlayableCard cardToPick, CardSlot cardSlotToPick)
	{
		ViewManager.Instance.SwitchToView(View.Board);

		yield return new WaitForSeconds(0.4f);

		// to the left and up, like something is being added under it
		Log.LogDebug($"[ApplyModAndDoAnimationSequence] CardToPick [{cardToPick.GetNameAndSlot()}] Current Position [{cardToPick.transform.position}] SlotHeightOffset [{cardToPick.SlotHeightOffset}]");

		Vector3 vectorHeight = new Vector3(0f, 0.25f, 0f);

		// card that will be hiding Boo Hag
		Tween.Position(
			cardToPick.transform,
			cardSlotToPick.transform.position + vectorHeight,
			0.1f,
			0f,
			Tween.EaseInOut
		);

		Vector3 cardRot = cardToPick.transform.rotation.eulerAngles;

		// rotate on z-axis, as if you rotated your hand holding the card counter-clockwise
		// Tween.Rotate(toRightTransform, new Vector3(0f, 0f, 25f), Space.World, 0.1f, 0f, Tween.EaseInOut);

		// Vector3 positionFurtherAwayFromBaseCard = toRightSlotTransform.position + Vector3.forward * 8f;
		// set starting position 
		// base.Card.transform.position = positionFurtherAwayFromBaseCard;

		// move pack from current position to the baseCardSlotPosition
		TweenBase tweenCrawlerMoveIntoCardSlot = Tween.Position(
			Card.transform,
			cardSlotToPick.transform.position,
			0.4f,
			0f,
			Tween.EaseOut
		);

		while (tweenCrawlerMoveIntoCardSlot.Status is not Tween.TweenStatus.Finished)
		{
			cardToPick.Anim.StrongNegationEffect();
			yield return new WaitForSeconds(0.1f);
		}
		
		cardToPick.AddTemporaryMod(new CardModificationInfo(1, 1)
		{
			singletonId = ModSingletonId
		});

		TweenBase tweenCrawlerMoveUpward = Tween.Position(
			Card.transform,
			cardSlotToPick.transform.position + new Vector3(0f, 0f, 0.31f),
			0.2f,
			0f,
			Tween.EaseOut
		);
		yield return new WaitForSeconds(0.1f);

		// rotate base card with its original rotation values so that it lays flat on the board again
		Tween.Rotation(cardToPick.transform, cardRot, 0.1f, 0f, Tween.EaseInOut);

		// offset the card to be a little higher
		cardToPick.SlotHeightOffset = 0.13f;
		
		Log.LogDebug($"[Crawler.AssignSkinCrawlerCardToHost] Assigning [{cardToPick.Info.name}] to slot, current position [{cardToPick.transform.position}]");
		yield return BoardManager.Instance.AssignCardToSlot(cardToPick, cardSlotToPick);
		
		yield return new WaitUntil(
			() => !Tween.activeTweens.Exists(t => t.targetInstanceID == cardToPick.transform.GetInstanceID())
		);
	}


	private bool CardIsAdjacent(PlayableCard playableCard)
	{
		return BoardManager.Instance.GetAdjacentSlots(Card.Slot)
		 .Exists(slot => slot && slot.Card == playableCard);
	}

	public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
	{
		Log.LogDebug(
			$"[Crawler.RespondsToOtherCardAssignedToSlot]"
		+ $" This {Card.GetNameAndSlot()} OtherCard {otherCard.GetNameAndSlot()} "
		+ $"_slotHidingUnderCard [{_slotHidingUnderCard}] "
		+ $"otherCard.Slot != Card.Slot [{otherCard.Slot != Card.Slot}]"
		);

		return _slotHidingUnderCard.IsNull()
		    && otherCard.Slot != Card.Slot
		    && CardIsAdjacent(otherCard);
	}

	public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
	{
		Log.LogDebug($"[Crawler.OnOtherCardAssignedToSlot] [{Card.GetNameAndSlot()}] Will now buff [{otherCard.GetNameAndSlot()}]");
		yield return AssignSkinCrawlerCardToHost(FindValidHostSlot());
	}


	public override bool RespondsToResolveOnBoard() => true;

	public override IEnumerator OnResolveOnBoard()
	{
		Log.LogDebug($"[Crawler.OnResolve] {Card.GetNameAndSlot()}");
		yield return AssignSkinCrawlerCardToHost(FindValidHostSlot());
	}
}

public partial class GrimoraPlugin
{
	public void Add_Ability_SkinCrawler()
	{
		const string rulebookDescription =
			"[creature] will attempt to find a host in an adjacent friendly slot, hiding under it providing a +1/+1 buff. "
		+ "Cards on the left take priority.";

		ApiUtils.CreateAbility<SkinCrawler>(rulebookDescription);
	}
}

public class SkinCrawlerSlot : NonCardTriggerReceiver
{
	[SerializeField] public CardSlot hidingOnSlot;
	[SerializeField] public PlayableCard hidingUnderCard;
	[SerializeField] public PlayableCard skinCrawlerCard;

	public static SkinCrawlerSlot SetupSlot(PlayableCard skinCrawler, PlayableCard hidingUnderCard)
	{
		SkinCrawlerSlot crawlerSlot = new GameObject("SkinCrawler_" + skinCrawler.Info.DisplayedNameEnglish).AddComponent<SkinCrawlerSlot>();
		crawlerSlot.transform.SetParent(hidingUnderCard.Slot.transform);
		skinCrawler.transform.SetParent(hidingUnderCard.Slot.transform);
		crawlerSlot.skinCrawlerCard = skinCrawler;
		crawlerSlot.hidingOnSlot = hidingUnderCard.Slot;
		crawlerSlot.hidingUnderCard = hidingUnderCard;
		Log.LogDebug($"[Crawler.AssignSkinCrawlerCardToHost] Finished setting up slot.");
		return crawlerSlot;
	}

	public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
	{
		return skinCrawlerCard != otherCard && otherCard.Slot == hidingOnSlot && hidingUnderCard.Dead;
	}

	public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
	{
		Log.LogDebug($"[CrawlerSlot.OnOtherCardAssignedToSlot] Card {skinCrawlerCard.GetNameAndSlot()} will now hide under {otherCard.GetNameAndSlot()}");
		hidingUnderCard = otherCard;
		transform.SetParent(otherCard.Slot.transform);
		yield return skinCrawlerCard.GetComponent<SkinCrawler>().ApplyModAndDoAnimationSequence(hidingUnderCard, hidingOnSlot);
	}

	public override bool RespondsToOtherCardDie(
		PlayableCard card,
		CardSlot deathSlot,
		bool fromCombat,
		PlayableCard killer
	)
	{
		Log.LogDebug($"[CrawlerSlot.RespondsToOtherCardDie] "
		           + $"Crawler {skinCrawlerCard.GetNameAndSlot()} Dying Card [{card.GetNameAndSlot()}] deathSlot [{deathSlot.name}] "
		           + $"_slotHidingUnderCard [{hidingOnSlot}] is deathSlot? [{hidingOnSlot == deathSlot}]"
		);
		return hidingOnSlot == deathSlot && card == hidingUnderCard && !card.HasAbility(Ability.IceCube);
	}

	public override IEnumerator OnOtherCardDie(
		PlayableCard card,
		CardSlot deathSlot,
		bool fromCombat,
		PlayableCard killer
	)
	{
		hidingOnSlot = null;
		hidingUnderCard = null;
		Log.LogInfo($"[CrawlerSlot.OnOtherCardDie] Resolving [{skinCrawlerCard.GetNameAndSlot()}] to deathSlot [{deathSlot.Index}]");
		yield return BoardManager.Instance.TransitionAndResolveCreatedCard(skinCrawlerCard, deathSlot, 0, false);
		yield return new WaitForSeconds(0.25f);
		Log.LogInfo($"[CrawlerSlot.OnOtherCardDie] Destroying [{gameObject}] at slot [{deathSlot.Index}]");
		Destroy(gameObject);
	}
}
