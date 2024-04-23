using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace CharacterSheetDnD.Utilities
{
	//public class ArmorClassUtility
	//{
	//	private readonly List<CharacterArmor> CharacterArmors;

	//	public ArmorClassUtility(List<CharacterArmor> armors)
	//	{
	//		CharacterArmors = armors;
	//	}

	//	private List<CharacterArmor> GetEquippedArmors(int characterId)
	//	{
	//		return CharacterArmors.Where(armor => armor.CharacterID == characterId && armor.IsEquipped).ToList();
	//	}

	//	// Method to calculate the total AC for a given character
	//	public static int CalculateTotalAC(List<CharacterArmor> armors, int characterId, int dexterityModifier)
	//	{
			
	//		var equippedArmors = armors.Where(armor => armor.CharacterID == characterId && armor.IsEquipped).ToList();
	//		int totalAC = 10; // Default AC when no armor is equipped


	//		foreach (var armor in equippedArmors)
	//		{
	//			int armorAC = 0;
	//			int armorMagicBonus = armor.IsMagicItem && armor.MagicBonusAC.HasValue ? (int)armor.MagicBonusAC : 0;

	//			if (armor is LightArmor lightArmor)
	//			{
	//				// Handling Light Armor
	//				switch (lightArmor.LightArmorType)
	//				{
	//					case LightArmorType.Padded:
	//						armorAC = 11 + dexterityModifier;
	//						break;
	//					case LightArmorType.Leather:
	//						armorAC = 11 + dexterityModifier;
	//						break;
	//					case LightArmorType.StuddedLeather:
	//						armorAC = 12 + dexterityModifier;
	//						break;
	//				}

	//			}
	//			else if (armor is MediumArmor mediumArmor)
	//			{
	//				// Handling Medium and Heavy Armor
	//				switch (mediumArmor.MediumArmorType)
	//				{
	//					case MediumArmorType.Hide:
	//						armorAC = 12 + System.Math.Min(dexterityModifier, 2);
	//						break;
	//					case MediumArmorType.ChainShirt:
	//						armorAC = 13 + System.Math.Min(dexterityModifier, 2);
	//						break;
	//					case MediumArmorType.ScaleMail:
	//						armorAC = 14 + System.Math.Min(dexterityModifier, 2);
	//						break;
	//					case MediumArmorType.Breastplate:
	//						armorAC = 14 + System.Math.Min(dexterityModifier, 2);
	//						break;
	//					case MediumArmorType.HalfPlate:
	//						armorAC = 14 + System.Math.Min(dexterityModifier, 2);
	//						break;
	//				}
	//			}
	//			else if (armor is HeavyArmor heavyArmor)
	//			{
	//				switch(heavyArmor.HeavyArmorType) 
	//				{
	//					case HeavyArmorType.RingMail:
	//						armorAC = 14;
	//						break;
	//					case HeavyArmorType.ChainMail:
	//						armorAC = 16;
	//						break;
	//					case HeavyArmorType.Splint:
	//						armorAC = 17;
	//						break;
	//					case HeavyArmorType.Plate:
	//						armorAC = 18;
	//						break;
	//				}
	//			}
	//			if (armor.ArmorType == ArmorType.Shield)
	//			{
	//				// Shields add +2 to AC on top of any armor
	//				armorAC += 2;
	//			}

	//			armorAC += armorMagicBonus; // Add any magic bonus the armor may have

	//			// Update totalAC with the highest armor AC value if armor is equipped

	//			totalAC = System.Math.Max(totalAC, armorAC);

	//		}

	//		return totalAC;
	//	}
	//}
}
