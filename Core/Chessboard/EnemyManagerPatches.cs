﻿using DiskCardGame;
using HarmonyLib;

namespace GrimoraMod
{
	[HarmonyPatch(typeof(ChessboardEnemyManager))]
	public class EnemyManagerPatches
	{
		[HarmonyPrefix, HarmonyPatch(nameof(ChessboardEnemyManager.MoveOpponentPieces))]
		public static bool DisableMovingOpponentPiecesPrefix()
		{
			return false;
		}
		
		[HarmonyPrefix, HarmonyPatch(nameof(ChessboardEnemyManager.StartCombatWithEnemy))]
		public static bool ModifyPieceToBeAddedToConfigListPrefix(
			ChessboardEnemyManager __instance, ChessboardEnemyPiece enemy, bool playerStarted)
		{
			
			// this will correctly place the pieces back if they aren't defeated.
			// e.g. from quitting mid match
			EnemyBattleSequencerPatches.activeEnemyPiece = enemy;

			// __instance.enemyPieces.Remove(enemy);
			// enemy.MapNode.OccupyingPiece = null;
			Singleton<MapNodeManager>.Instance.SetAllNodesInteractable(nodesInteractable: false);
			__instance.StartCoroutine(__instance.StartCombatSequence(enemy, playerStarted));
			
			return false;
		}
	}
}