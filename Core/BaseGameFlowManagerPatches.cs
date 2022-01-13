﻿using System.Collections;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;

namespace GrimoraMod
{
	[HarmonyPatch(typeof(GameFlowManager))]
	public class BaseGameFlowManagerPatches
	{
		[HarmonyPostfix, HarmonyPatch(nameof(GameFlowManager.TransitionTo))]
		public static IEnumerator Postfix(
			IEnumerator enumerator,
			GameFlowManager __instance,
			GameState gameState,
			NodeData triggeringNodeData = null,
			bool immediate = false,
			bool unlockViewAfterTransition = true
		)
		{
			// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] GameState is [{gameState}]");

			if (SaveManager.SaveFile.IsGrimora && gameState == GameState.Map)
			{
				// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] SaveFile is Grimora and GameState is GameState.Map");
				ViewManager.Instance.Controller.SwitchToControlMode(ViewController.ControlMode.Map);

				ViewManager.Instance.Controller.LockState = ViewLockState.Locked;

				// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] SceneSpecificTransitionTo");
				__instance.SceneSpecificTransitionTo(GameState.Map, immediate);

				// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] SaveToFile");
				SaveManager.SaveToFile();

				yield return new WaitForSeconds(0.2f);
				// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] " +
				//                            $"map is null? [{RunState.Run.map == null}])");

				if (ChessboardMapExt.Instance.pieces.Count > 0)
				{
					// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] ChessboardMapExt is not null");
					var bossPiece = ChessboardMapExt.Instance.BossPiece;

					if (bossPiece is not null && bossPiece.NodeData is not null)
					{
						if (RunState.Run.map != null
						    && RunState.Run.currentNodeId == bossPiece.NodeData.id + RunState.Run.regionTier + 1)
						{
							// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] Completing region sequence");
							yield return ChessboardMapExt.Instance.CompleteRegionSequence();
						}
					}
				}

				// GrimoraPlugin.Log.LogDebug($"[GameFlowManager.TransitionTo] unlockViewAfterTransition");
				if (unlockViewAfterTransition)
				{
					ViewManager.Instance.Controller.LockState = ViewLockState.Unlocked;
				}
			}

			yield return enumerator;
			yield break;
		}
	}
}