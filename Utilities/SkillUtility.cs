namespace CharacterSheetDnD.Utilities
{
	public static class SkillUtility
	{
		public static int CalculateSkillModifier(bool isProficient, int abilityScoreModifier, int proficiencyBonus)
		{
			return abilityScoreModifier + (isProficient ? proficiencyBonus : 0);
		}
	}
}

