using CharacterSheetDnD.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetDnD.ViewModels
{
    public class GenericArmorViewModel
    {
        public int ArmorID { get; set; }
        public int CharacterID { get; set; } // To know which character you're adding equipment to

        [Required(ErrorMessage = "Armor name is required.")]
        public string? ArmorName { get; set; }

        [Required(ErrorMessage = "Please select the armor type.")]
        public ArmorType? ArmorType { get; set; } // Enum for armor types (Light, Medium, Heavy, Shield)
                                                  // Helper property to determine if SpecificArmorType is required
        [Required(ErrorMessage = "Please select the specific armor type.")]
        public string SpecificArmorType { get; set; } = "";
        public bool IsSpecificTypeRequired { get; set; }

        public string? Description { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsEquipped { get; set; }
        public int ValueInGold { get; set; } = 0;

        public bool StealthDisadvantage { get; set; }

        [Required(ErrorMessage = "Please select the armor's rarity.")]
        public Rarity? Rarity { get; set; } // Enum for Rarity (Common, Uncommon, etc.)

        // Magic-specific properties
        public bool IsMagic { get; set; }
        public bool RequiresAttunement { get; set; }
        public bool IsAttuned { get; set; }

        [Range(0, 100, ErrorMessage = "Magic bonus to AC must be between 0 and 100.")]
        public int? MagicBonusAC { get; set; } // Assuming this is a numeric bonus to AC for magic armors

        public string? MagicEffectDescription { get; set; }
        public string? MagicEffectMechanics { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Charges must be a positive number.")]
        public int? MagicCharges { get; set; }
        public string? MagicRechargeRate { get; set; }
    }
}
