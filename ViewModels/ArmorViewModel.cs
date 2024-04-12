using CharacterSheetDnD.Models;
using CharacterSheetDnD.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.ViewModels
{
    public class ArmorViewModel
    {
		public int CharacterID { get; set; } // To know which character you're adding equipment to
		public bool StealthDisadvantage { get; set; }
		public ArmorType ArmorType { get; set; }
		public LigthArmor LigthArmor { get; set; }
		public MediumArmor MediumArmor { get; set; }
		public HeavyArmor HeavyArmor { get; set; }
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
}


