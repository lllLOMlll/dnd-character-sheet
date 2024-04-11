using CharacterSheetDnD.Migrations;

namespace CharacterSheetDnD.Utilities
{
	public class AttackDamageRolls
	{
		public static int GetAttackBonus(int proficiencyBonus, int StrengthOrDexterityModifier)
		{
			return proficiencyBonus + StrengthOrDexterityModifier;
		}

		public static int GetProficiencyBonusAttackRoll(bool isProficient, int profiencyBonus)
		{
			if (isProficient)
			{
				return profiencyBonus;
			}
			else
			{
				return 0;
			}
		}

		public static int GetStrengthOrDexterityModifier(int strengthModifier, int dexterityModifier, bool isMelee, bool isRange, bool isFinesse, bool isThrown)
		{
			if (isMelee)
			{
				if (!isFinesse)
				{
					return strengthModifier;
				}
				return Math.Max(strengthModifier, dexterityModifier);
			}
			if (isRange)
			{
				if (!isThrown)
				{
					return dexterityModifier;
				}
				return Math.Max(strengthModifier, dexterityModifier);
			}
			else
				throw new InvalidOperationException("Invalid combination of parameters for weapon attack type.");
			
	
		}

	}
}
