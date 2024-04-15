using CharacterSheetDnD.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetDnD.ViewModels
{
	public abstract class ArmorViewModel
	{
		public int ArmorID { get; set; }
		public int CharacterID { get; set; }
		public string? ArmorName { get; set; }

		[Required]
		[StringLength(255)]
		public string? Description { get; set; }
		public Rarity Rarity { get; set; }
		public int Quantity { get; set; } = 1;
		public int ValueInGold { get; set; } = 0;
		public bool StealthDisadvantage { get; set; }
		public bool IsEquipped { get; set; }
		public bool IsMagicItem { get; set; }
		public bool RequiresAttunement { get; set; }
		public bool IsAttuned { get; set; }
		public MagicBonusAC MagicBonusAC { get; set; }
		public string? MagicEffectDescription { get; set; }
		public string? MagicEffectMechanics { get; set; }
		[Range(0, 100, ErrorMessage = "Charges must be a positive number")]
		public int? MagicCharges { get; set; }
		public string? MagicRechargeRate { get; set; }
	}

	public class LightArmorViewModel : ArmorViewModel
	{
		public LightArmorType LightArmorType { get; set; }
	}

	public class MediumArmorViewModel : ArmorViewModel
	{
		public MediumArmorType MediumArmorType { get; set; }
	}

	public class HeavyArmorViewModel : ArmorViewModel
	{
		public HeavyArmorType HeavyArmorType { get; set; }
	}

	public class ShieldViewModel : ArmorViewModel
	{
		// Specific properties for shields if any
	}
}
