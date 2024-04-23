using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
using System.Collections.Generic;
using System.Linq;
using static CharacterSheetDnD.ViewModels.MyCharacterViewModel;

namespace CharacterSheetDnD.Services
{
	public class ArmorService : IArmorService
	{
		private readonly ApplicationDbContext _context;

		public ArmorService(ApplicationDbContext context)
		{
			_context = context;
		}

		public int CalculateTotalAC(int characterId, int dexterityModifier)
		{
			var equippedArmors = _context.CharacterArmors
								  .Where(armor => armor.CharacterID == characterId && armor.IsEquipped)
								  .ToList();
			int baseAC = 10; // Base AC when no armor is equipped

			int highestBaseAC = 0; // Track the highest base AC from armor
			int shieldBonus = 0; // Separate calculation for shield bonus

			foreach (var armor in equippedArmors)
			{
				int armorAC = 0;
				int armorMagicBonus = armor.IsMagicItem && armor.MagicBonusAC.HasValue ? (int)armor.MagicBonusAC : 0;

				if (armor.ArmorType == ArmorType.Shield)
				{
					shieldBonus += 2; // Shields add +2 to AC
					continue; // Skip further processing for shields
				}

				switch (armor.ArmorType)
				{
					case ArmorType.Light:
						LightArmor lightArmor = armor as LightArmor;
						armorAC = lightArmor.LightArmorType switch
						{
							LightArmorType.Padded => 11 + dexterityModifier,
							LightArmorType.Leather => 11 + dexterityModifier,
							LightArmorType.StuddedLeather => 12 + dexterityModifier,
							_ => armorAC
						};
						break;
					case ArmorType.Medium:
						MediumArmor mediumArmor = armor as MediumArmor;
						armorAC = mediumArmor.MediumArmorType switch
						{
							MediumArmorType.Hide => 12 + Math.Min(dexterityModifier, 2),
							MediumArmorType.ChainShirt => 13 + Math.Min(dexterityModifier, 2),
							MediumArmorType.ScaleMail => 14 + Math.Min(dexterityModifier, 2),
							MediumArmorType.Breastplate => 14 + Math.Min(dexterityModifier, 2),
							MediumArmorType.HalfPlate => 14 + Math.Min(dexterityModifier, 2),
							_ => armorAC
						};
						break;
					case ArmorType.Heavy:
						HeavyArmor heavyArmor = armor as HeavyArmor;
						armorAC = heavyArmor.HeavyArmorType switch
						{
							HeavyArmorType.RingMail => 14,
							HeavyArmorType.ChainMail => 16,
							HeavyArmorType.Splint => 17,
							HeavyArmorType.Plate => 18,
							_ => armorAC
						};
						break;
				}

				armorAC += armorMagicBonus; // Add any magic bonus the armor may have
				highestBaseAC = Math.Max(highestBaseAC, armorAC); // Only the highest armor AC counts, except shields
			}

			if (highestBaseAC == 0) // If no armor is worn other than potentially a shield, start from default base AC
			{
				baseAC = 10 + dexterityModifier;
			}
			else
			{
				baseAC = highestBaseAC; // Start from highest armor AC instead of default base AC
			}

			return baseAC + shieldBonus; // Sum base AC with any shield bonus
		}


	}
}
