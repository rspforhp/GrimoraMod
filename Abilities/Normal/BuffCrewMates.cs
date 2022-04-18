﻿using DiskCardGame;
using InscryptionAPI.Triggers;

namespace GrimoraMod;

public class BuffCrewMates : AbilityBehaviour, IPassiveAttackBuff
{
	public static Ability ability;

	public override Ability Ability => ability;

	private bool IsSkeleton(PlayableCard playableCard) => playableCard.InfoName().Equals(GrimoraPlugin.NameSkeleton);

	public int GetPassiveAttackBuff(PlayableCard target) => IsSkeleton(target) ? 1 : 0;
}

public partial class GrimoraPlugin
{
	public void Add_Ability_BuffCrewMates()
	{
		const string rulebookDescription =
			"[creature] empowers each Skeleton on the owner's side of the board, providing a +1 buff their power.";

		AbilityBuilder<BuffCrewMates>.Builder
		 .SetRulebookDescription(rulebookDescription)
		 .SetRulebookName("Sea Shanty")
		 .Build();
	}
}
