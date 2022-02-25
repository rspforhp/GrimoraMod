﻿using APIPlugin;
using DiskCardGame;

namespace GrimoraMod;

public partial class GrimoraPlugin
{
	public const string NameDeathKnell = "GrimoraMod_DeathKnell";

	private void Add_DeathKnell()
	{
		NewCard.Add(CardBuilder.Builder
			.SetAsRareCard()
			.SetAbilities(CreateKnells.ability)
			.SetAbilities(SpecialTriggeredAbility.BellProximity, SpecialTriggeredAbility.Daus)
			.SetBaseAttackAndHealth(1, 2)
			.SetBoneCost(8)
			.SetDescription("FOR WHOM THE BELL TOLLS?")
			.SetNames(NameDeathKnell, "Death Knell")
			.Build()
		);
	}
}
