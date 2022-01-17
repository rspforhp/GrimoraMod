﻿using System.Collections.Generic;
using APIPlugin;
using DiskCardGame;
using GrimoraMod.Properties;

namespace GrimoraMod;

public partial class GrimoraPlugin
{
	public const string NameFlames = "ara_Flames";

	private void AddAra_Flames()
	{
		List<Ability> abilities = new List<Ability>
		{
			Ability.BuffNeighbours,
			Ability.Brittle
		};

		NewCard.Add(CardBuilder.Builder
			.WithAbilities(abilities)
			.WithBaseAttackAndHealth(0, 1)
			.WithBonesCost(2)
			.WithNames(NameFlames, "Flames")
			.WithPortrait(Resources.Flames)
			.Build()
		);
	}
}