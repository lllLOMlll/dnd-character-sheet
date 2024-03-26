namespace CharacterSheetDnD.Utilities
{
	public class AbilityScoreUtility
	{
		public static int CalculateModifier(int abilityScore)
		{
			return (int)Math.Floor((abilityScore - 10) / 2.0);
		}
	}
}
