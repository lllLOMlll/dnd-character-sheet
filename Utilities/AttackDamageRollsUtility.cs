namespace CharacterSheetDnD.Utilities
{
	public class AttackDamageRolls
	{
		public static int GetAttackBonus(int proficiencyBonus, int StrengthOrDexterityModifier)
		{
			return proficiencyBonus + StrengthOrDexterityModifier;
		}
	}
}
