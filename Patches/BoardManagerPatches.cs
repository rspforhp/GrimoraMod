﻿using System.Collections;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;

namespace GrimoraMod;

[HarmonyPatch(typeof(BoardManager))]
public class BoardManagerPatches
{
	[HarmonyPostfix, HarmonyPatch(nameof(BoardManager.ClearBoard))]
	public static IEnumerator CheckForNonCardTriggersOnTheBoardStill(IEnumerator enumerator, BoardManager __instance)
	{
		yield return enumerator;

		if (GrimoraSaveUtil.isGrimora)
		{
			foreach (var slot in __instance.AllSlotsCopy)
			{
				var nonCardReceivers = slot.GetComponentsInChildren<NonCardTriggerReceiver>();
				foreach (var nonCardTriggerReceiver in nonCardReceivers)
				{
					GrimoraPlugin.Log.LogWarning($"[CleanUp] Destroying NonCardTriggerReceiver [{nonCardTriggerReceiver}] from slot [{slot}]");
					UnityObject.Destroy(nonCardTriggerReceiver.gameObject);
				}

				var playableCardsNotOnTheBoard = slot.GetComponentsInChildren<PlayableCard>().Where(card => card.HasAbility(SkinCrawler.ability));
				foreach (var card in playableCardsNotOnTheBoard)
				{
					GrimoraPlugin.Log.LogWarning($"[CleanUp] Playing exit for playable card as it exists but not on the board technically [{card}] from slot [{slot}]");
					card.ExitBoard(0.3f, Vector3.zero);
					yield return new WaitForSeconds(0.1f);
				}
			}
		}
	}
}

[HarmonyPatch(typeof(BoardManager3D))]
public class BoardManager3DPatches
{
	[HarmonyPostfix, HarmonyPatch(nameof(BoardManager3D.MoveQueuedCardToSlot))]
	public static void CheckForNonCardTriggersOnTheBoardStill(
		BoardManager3D __instance,
		PlayableCard card,
		CardSlot slot,
		float transitionDuration = 0.1f,
		bool doTween = true,
		bool setStartPosition = true
	)
	{
		card.transform.SetParent(BoardManager.Instance.OpponentQueueSlots[card.Slot.Index].transform);
	}
}
