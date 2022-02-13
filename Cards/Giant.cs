﻿using System.Collections;
using APIPlugin;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using static GrimoraMod.GrimoraPlugin;

namespace GrimoraMod;

public partial class GrimoraPlugin
{
	public const string NameGiant = "GrimoraMod_Giant";

	private void Add_Giant()
	{
		NewCard.Add(CardBuilder.Builder
				.SetAsNormalCard()
				.SetAbilities(Ability.QuadrupleBones, Ability.SplitStrike)
				.SetBaseAttackAndHealth(2, 7)
				.SetBoneCost(15)
				.SetNames(NameGiant, "Giant")
				.SetTraits(Trait.Giant, Trait.Uncuttable)
				// .SetDescription("A vicious pile of bones. You can have it...")
				.Build()
			// , specialAbilitiesIdsParam: new List<SpecialAbilityIdentifier> { sbIds.id }
		);
	}
}

[HarmonyPatch]
public class ModifyLocalPositionsOfTableObjects
{
	[HarmonyPostfix,
	 HarmonyPatch(typeof(BoardManager3D), nameof(BoardManager3D.TransitionAndResolveCreatedCard))
	]
	public static IEnumerator ChangeScaleOfMoonCardToFitAcrossAllSlots(
		IEnumerator enumerator, PlayableCard card, CardSlot slot,
		float transitionLength, bool resolveTriggers = true)
	{
		if (GrimoraSaveUtil.isGrimora
		    && card.Info.HasTrait(Trait.Giant)
		    && card.Info.SpecialAbilities.Contains(GrimoraGiant.NewSpecialAbility.specialTriggeredAbility))
		{
			Log.LogDebug($"Setting new scaling and position of [{card.Info.name}]");
			// Card -> RotatingParent -> TombstoneParent -> Cardbase_StatsLayer
			Transform rotatingParent = card.transform.GetChild(0);
			Log.LogDebug($"Transforming [{rotatingParent.name}]");

			rotatingParent.localPosition = new Vector3(-0.7f, 1.05f, 0f);
			// GrimoraPlugin.Log.LogDebug($"Successfully set new localPosition for the giant");

			rotatingParent.localScale = new Vector3(2.1f, 2.1f, 1f);
			// GrimoraPlugin.Log.LogDebug($"Successfully set new scaling for the giant");
		}

		yield return enumerator;
	}
}

[HarmonyPatch]
public class KayceeModLogicForDeathTouchPrevention
{
	[HarmonyPostfix, HarmonyPatch(typeof(Deathtouch), nameof(Deathtouch.RespondsToDealDamage))]
	public static void AddLogicForDeathTouchToNotKillGiants(int amount, PlayableCard target, ref bool __result)
	{
		bool targetIsNotGrimoraGiant =
			!target.Info.SpecialAbilities.Contains(GrimoraGiant.NewSpecialAbility.specialTriggeredAbility);
		__result = __result && targetIsNotGrimoraGiant;
	}
}

[HarmonyPatch]
public class AddLogicForGiantStrikeAbility
{
	[HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.GetOpposingSlots))]
	public static void FixSlotsToAttackForGiantStrike(PlayableCard __instance, ref List<CardSlot> __result)
	{
		if (__instance.Info.HasTrait(Trait.Giant)
		    && __instance.HasAbility(GiantStrike.ability)
		    && __instance.Info.SpecialAbilities.Contains(GrimoraGiant.NewSpecialAbility.specialTriggeredAbility))
		{
			List<CardSlot> slotsToTarget = __instance.OpponentCard
				? BoardManager.Instance.PlayerSlotsCopy
				: BoardManager.Instance.OpponentSlotsCopy;

			__result = new List<CardSlot>(slotsToTarget.Where(slot => slot.opposingSlot.Card == __instance));
			Log.LogDebug($"[AllStrikePatch] Opposing slots is now [{string.Join(",", __result.Select(_ => _.Index))}]");
		}
	}
}
