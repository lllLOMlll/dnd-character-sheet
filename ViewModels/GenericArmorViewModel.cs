using CharacterSheetDnD.Models;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetDnD.ViewModels
{
	public class GenericArmorViewModel
	{
		public int ArmorID { get; set; }
		public int CharacterID { get; set; }

		[Required(ErrorMessage = "Armor name is required.")]
		public string? ArmorName { get; set; }

		[Required(ErrorMessage = "Please select the armor type.")]
		public ArmorType? ArmorType { get; set; }

		public LightArmorType? SpecificLightArmorType { get; set; }
		public MediumArmorType? SpecificMediumArmorType { get; set; }
		public HeavyArmorType? SpecificHeavyArmorType { get; set; }

		public string? Description { get; set; }
		public int Quantity { get; set; } = 1;
		public bool IsEquipped { get; set; }
		public int ValueInGold { get; set; } = 0;

		public bool StealthDisadvantage { get; set; }

		[Required(ErrorMessage = "Please select the armor's rarity.")]
		public Rarity? Rarity { get; set; }

		// Magic-specific properties
		public bool IsMagic { get; set; }
		public bool RequiresAttunement { get; set; }
		public bool IsAttuned { get; set; }

		[Range(0, 100, ErrorMessage = "Magic bonus to AC must be between 0 and 100.")]
		public int? MagicBonusAC { get; set; }

		public string? MagicEffectDescription { get; set; }
		public string? MagicEffectMechanics { get; set; }
		[Range(0, int.MaxValue, ErrorMessage = "Charges must be a positive number.")]
		public int? MagicCharges { get; set; }
		public string? MagicRechargeRate { get; set; }
	}
}
