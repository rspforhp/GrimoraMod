﻿using APIPlugin;
using DiskCardGame;
using static DiskCardGame.CardAppearanceBehaviour;

namespace GrimoraMod;

public partial class GrimoraPlugin
{
	public const string NameDeathKnellBell = "GrimoraMod_DeathKnell_Bell";

	private void Add_DeathKnellBell()
	{
		NewCard.Add(CardBuilder.Builder
			.SetAppearance(Appearance.TerrainBackground, Appearance.TerrainLayout)
			.SetBaseAttackAndHealth(0, 1)
			.SetNames(NameDeathKnellBell, "Chime")
			.SetTraits(Trait.Structure, Trait.Terrain)
			.Build()
		);
	}
}
