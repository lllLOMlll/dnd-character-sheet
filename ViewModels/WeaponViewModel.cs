using CharacterSheetDnD.Models;
using CharacterSheetDnD.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.ViewModels
{
    public class WeaponViewModel
    {
        public int WeaponID { get; set; }
        public int CharacterID { get; set; } // To know which character you're adding equipment to
        public string? WeaponName { get; set; }
		public bool IsProficient { get; set; }

		[Required(ErrorMessage = "Please select whether the weapon is Melee or Range.")]
		public MeleeRange? MeleeRange { get; set; }
        public EquipmentType? ItemType { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsEquipped { get; set; }
        public int ValueInGold { get; set; } = 0;
        [Required(ErrorMessage = "Damage Dice is required.")]
        public DamageDice? DamageDice { get; set; }
        [Required(ErrorMessage = "Damage Type is required.")]
        public DamageType? DamageType { get; set; }
        public int WeaponProperties { get; set; } = (int)WeaponProperty.None;
        public WeaponRange WeaponRange { get; set; }

        // Magic-specific propterties
        public bool IsMagic { get; set; }
		public bool RequiresAttunement { get; set; }
		public bool IsAttuned { get; set; }
		public BonusAttackDamageRolls BonusAttackDamageRolls { get; set; }
        public string? EffectDescription { get; set; }
        public string? EffectMechanics { get; set; }
        public int? Charges { get; set; }
        public string? RechargeRate { get; set; }


        // This will hold the values for the checkboxes in your form
        public List<WeaponPropertyOption> AvailableProperties { get; set; } = new List<WeaponPropertyOption>();

        // This will hold the selected values when the form is submitted
        public WeaponProperty SelectedProperties { get; set; } = WeaponProperty.None;

        public IEnumerable<CharacterWeapon>? Weapons { get; set; }

        public class WeaponPropertyOption
        {
            public WeaponProperty Value { get; set; }
            public string? Text { get; set; }
            public bool IsSelected { get; set; }
        }

    }
}


