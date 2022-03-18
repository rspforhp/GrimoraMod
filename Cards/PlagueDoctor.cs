using DiskCardGame;

namespace GrimoraMod;

public partial class GrimoraPlugin
{
	public const string NamePlagueDoctor = $"{GUID}_PlagueDoctor";

	private void Add_PlagueDoctor()
	{
		CardBuilder.Builder
			.SetAsNormalCard()
			.SetAbilities(Ability.Deathtouch)
			.SetBaseAttackAndHealth(1, 1)
			.SetBoneCost(6)
			.SetNames(NamePlagueDoctor, "Plague Doctor")
			.SetDescription("IRONICALLY ENOUGH, NOT A REAL DOCTOR.")
			.Build();
	}
}
