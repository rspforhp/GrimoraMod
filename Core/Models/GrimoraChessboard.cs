﻿using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using static GrimoraMod.GrimoraPlugin;

namespace GrimoraMod;

public class GrimoraChessboard
{
	private readonly Dictionary<System.Type, Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>> _nodesByPieceType;

	private Dictionary<Type, Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>> BuildDictionary()
	{
		return new Dictionary<Type, Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>>
		{
			{
				typeof(ChessboardBlockerPiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					GetBlockerNodes,
					() => null
				)
			},
			{
				typeof(ChessboardBoneyardPiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					GetBoneyardNodes,
					() => null // todo: change later after impl
				)
			},
			{
				typeof(ChessboardBossPiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					() => new List<ChessNode>() { GetBossNode() },
					() => null // todo: change later after impl
				)
			},
			{
				typeof(ChessboardCardRemovePiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					GetCardRemovalNodes,
					() => new CardRemoveNodeData()
				)
			},
			{
				typeof(ChessboardChestPiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					GetCardRemovalNodes,
					() => new CardChoicesNodeData()
				)
			},
			{
				typeof(ChessboardElectricChairPiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					GetElectricChairNodes,
					() => null // todo: impl after
				)
			},
			{
				typeof(ChessboardEnemyPiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					GetEnemyNodes,
					() => null // todo: impl after
				)
			},
			{
				typeof(ChessboardGoatEyePiece),
				new Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>>(
					GetGoatEyeNodes,
					() => null // todo: impl after
				)
			}
		};
	}

	private readonly Dictionary<string, Opponent.Type> _bossBySpecialId = new()
	{
		{ KayceeBossOpponent.SpecialId, BaseBossExt.KayceeOpponent },
		{ SawyerBossOpponent.SpecialId, BaseBossExt.SawyerOpponent },
		{ RoyalBossOpponentExt.SpecialId, BaseBossExt.RoyalOpponent },
		{ GrimoraBossOpponentExt.SpecialId, BaseBossExt.GrimoraOpponent }
	};

	public readonly int indexInList;
	public readonly ChessNode BossNode;

	protected internal ChessboardEnemyPiece BossPiece =>
		GetPieceAtSpace(BossNode.GridX, BossNode.GridY) as ChessboardEnemyPiece;

	public readonly List<ChessRow> Rows;


	public Opponent.Type ActiveBossType;


	public GrimoraChessboard(IEnumerable<List<int>> board, int indexInList)
	{
		Rows = board.Select((boardList, idx) => new ChessRow(boardList, idx)).ToList();
		BossNode = GetBossNode();
		this.indexInList = indexInList;
		_nodesByPieceType = BuildDictionary();
	}

	#region Getters

	public List<ChessNode> GetOpenPathNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(0)).ToList();
	}

	private List<ChessNode> GetBlockerNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(1)).ToList();
	}

	private List<ChessNode> GetChestNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(2)).ToList();
	}

	private List<ChessNode> GetEnemyNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(3)).ToList();
	}

	private ChessNode GetBossNode()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(4)).Single();
	}

	private List<ChessNode> GetCardRemovalNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(5)).ToList();
	}

	private List<ChessNode> GetBoneyardNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(6)).ToList();
	}

	private List<ChessNode> GetElectricChairNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(7)).ToList();
	}

	private List<ChessNode> GetGoatEyeNodes()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(8)).ToList();
	}

	public ChessNode GetPlayerNode()
	{
		return Rows.SelectMany(row => row.GetNodesOfType(9)).Single();
	}

	public static string GetBossSpecialIdForRegion()
	{
		switch (ConfigHelper.Instance.BossesDefeated)
		{
			case 1:
				Log.LogDebug($"[GetBossSpecialIdForRegion] Kaycee defeated");
				return SawyerBossOpponent.SpecialId;
			case 2:
				Log.LogDebug($"[GetBossSpecialIdForRegion] Sawyer defeated");
				return RoyalBossOpponentExt.SpecialId;
			case 3:
				Log.LogDebug($"[GetBossSpecialIdForRegion] Royal defeated");
				return GrimoraBossOpponentExt.SpecialId;
			default:
				Log.LogDebug($"[GetBossSpecialIdForRegion] No bosses defeated yet, creating Kaycee");
				return KayceeBossOpponent.SpecialId;
		}
	}

	#endregion


	public void SetupBoard()
	{
		PlaceBossPiece(GetBossSpecialIdForRegion());
		PlacePieces<ChessboardBlockerPiece>();
		PlacePieces<ChessboardBoneyardPiece>();
		PlacePieces<ChessboardCardRemovePiece>();
		PlacePieces<ChessboardChestPiece>();
		PlacePieces<ChessboardElectricChairPiece>();
		PlacePieces<ChessboardEnemyPiece>();
		PlacePieces<ChessboardGoatEyePiece>();
	}

	public void UpdatePlayerMarkerPosition(bool changingRegion)
	{
		int x = GrimoraSaveData.Data.gridX;
		int y = GrimoraSaveData.Data.gridY;

		Log.LogDebug($"[HandlePlayerMarkerPosition] " +
		             $"Player Marker name [{PlayerMarker.Instance.name}] " +
		             $"x{x}y{y} coords");

		var occupyingPiece = GetPieceAtSpace(x, y);

		bool isPlayerOccupied = occupyingPiece is not null && PlayerMarker.Instance.name == occupyingPiece.name;

		Log.LogDebug($"[HandlePlayerMarkerPosition] isPlayerOccupied? [{isPlayerOccupied}]");

		if (changingRegion || !StoryEventsData.EventCompleted(StoryEvent.GrimoraReachedTable))
		{
			// the PlayerNode will be different since this is now a different chessboard
			x = GetPlayerNode().GridX;
			y = GetPlayerNode().GridY;
		}

		MapNodeManager.Instance.ActiveNode = ChessboardNavGrid.instance.zones[x, y].GetComponent<MapNode>();
		Log.LogDebug($"[SetupGamePieces] MapNodeManager ActiveNode is x[{x}]y[{y}]");

		Log.LogDebug($"[SetupGamePieces] SetPlayerAdjacentNodesActive");
		ChessboardNavGrid.instance.SetPlayerAdjacentNodesActive();

		Log.LogDebug($"[SetupGamePieces] Setting player position to active node");
		PlayerMarker.Instance.transform.position = MapNodeManager.Instance.ActiveNode.transform.position;
	}

	public void SetSavePositions()
	{
		// set the updated position to spawn the player in
		GrimoraSaveData.Data.gridX = GetPlayerNode().GridX;
		GrimoraSaveData.Data.gridY = GetPlayerNode().GridY;
	}

	#region HelperMethods

	public static ChessboardMapNode GetNodeAtSpace(int x, int y)
	{
		return ChessboardNavGrid.instance.zones[x, y].GetComponent<ChessboardMapNode>();
	}

	public static ChessboardPiece GetPieceAtSpace(int x, int y)
	{
		// Log.LogDebug($"[GetPieceAtSpace] Getting piece at space x{x}y{y}");
		return GetNodeAtSpace(x, y).OccupyingPiece;
	}

	private EncounterBlueprintData GetBlueprint()
	{
		// Log.LogDebug($"[GetBlueprint] ActiveBoss [{ActiveBossType}]");
		var blueprints = BlueprintUtils.RegionWithBlueprints[ActiveBossType];
		return blueprints[UnityEngine.Random.RandomRangeInt(0, blueprints.Count)];
	}

	#endregion

	#region PlacingPieces

	public ChessboardEnemyPiece PlaceBossPieceDev(int x, int y, string bossName)
	{
		return PlacePiece<ChessboardEnemyPiece>(x, y, bossName);
	}

	public ChessboardBossPiece PlaceBossPiece(string bossName)
	{
		return CreateChessPiece<ChessboardBossPiece>(
			ChessboardMapExt.Instance.PrefabPieceHelper.PrefabBossPiece, BossNode.GridX, BossNode.GridY, bossName
		);
	}

	public T PlacePiece<T>(int x, int y, string id = "", SpecialNodeData specialNodeData = null) where T : ChessboardPiece
	{
		// out ChessboardPiece prefabToUse
		if (!ChessboardMapExt.Instance.PrefabPieceHelper
			    .PieceSetupByType
			    .TryGetValue(typeof(T), out Tuple<float, Func<GameObject>, Func<ChessboardPiece>> tuple))
		{
			throw new Exception($"Unable to find piece of type [{typeof(T)}] in PieceSetupByType!");
		}

		return CreateChessPiece<T>(tuple.Item3.Invoke(), x, y, id, specialNodeData);
	}

	public List<T> PlacePieces<T>() where T : ChessboardPiece
	{
		if (!_nodesByPieceType.TryGetValue(typeof(T), out Tuple<Func<List<ChessNode>>, Func<SpecialNodeData>> tuple))
		{
			throw new Exception($"Unable to find piece of type [{typeof(T)}] in _nodesByPieceType!");
		}

		List<ChessNode> nodes = tuple.Item1.Invoke();
		SpecialNodeData specialNodeData = tuple.Item2.Invoke();

		return nodes.Select(node => PlacePiece<T>(node.GridX, node.GridY, specialNodeData: specialNodeData)).ToList();
	}

	#endregion

	#region CreatePieces

	private T CreateChessPiece<T>(
		ChessboardPiece prefab,
		int x, int y,
		string id = "",
		SpecialNodeData specialNodeData = null) where T : ChessboardPiece
	{
		string coordName = $"x[{x}]y[{y}]";

		ChessboardPiece piece = GetPieceAtSpace(x, y);

		if (piece is not null && ChessboardMapExt.Instance.PieceExistsInActivePieces.Invoke(piece))
		{
			Log.LogDebug($"[CreateChessPiece<{typeof(T).Name}>] Skipping x{x}y{y}, {piece.name} already exists");
			return piece as T;
		}

		piece = Object.Instantiate(prefab, ChessboardMapExt.Instance.dynamicElementsParent);
		piece.gridXPos = x;
		piece.gridYPos = y;

		// ChessboardEnemyPiece => EnemyPiece_x[]y[]
		string nameTemp = piece.GetType().Name.Replace("Chessboard", "") + "_" + coordName;

		switch (piece)
		{
			case ChessboardBossPiece bossPiece:
				ActiveBossType = _bossBySpecialId.GetValueSafe(id);
				// Log.LogDebug($"[CreateChessPiece] id is not null, setting ActiveBossType to [{id}]");
				bossPiece.blueprint = BlueprintUtils.BossInitialBlueprints[id];
				break;
			case ChessboardEnemyPiece enemyPiece:
			{
				enemyPiece.GoalPosX = x;
				enemyPiece.GoalPosX = y;
				enemyPiece.blueprint = GetBlueprint();
				enemyPiece.specialEncounterId = "GrimoraModBattleSequencer";
				break;
			}
		}

		if (specialNodeData is not null)
		{
			piece.NodeData = specialNodeData;
		}

		piece.name = nameTemp;

		// Log.LogDebug($"[CreatingPiece] {piece.name}");
		ChessboardMapExt.Instance.pieces.Add(piece);
		// ChessboardNavGrid.instance.zones[x, y].GetComponent<ChessboardMapNode>().OccupyingPiece = piece;
		return piece as T;
	}

	#endregion
}
