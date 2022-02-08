﻿using APIPlugin;
using DiskCardGame;

namespace GrimoraMod;

public partial class GrimoraPlugin
{
	public const string NameSilbon = "GrimoraMod_Silbon";

	private void Add_Silbon()
	{
		NewCard.Add(CardBuilder.Builder
			.SetAsNormalCard()
			.SetAbilities(InvertedStrike.ability, Ability.Strafe)
			.SetBaseAttackAndHealth(3, 2)
			.SetBoneCost(7)
			// .SetDescription("Going into that well wasn't the best idea...")
			.SetNames(NameSilbon, "Silbon")
			.Build()
		);
	}
}
