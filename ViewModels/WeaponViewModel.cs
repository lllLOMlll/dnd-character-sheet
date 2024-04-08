using CharacterSheetDnD.Models;
using CharacterSheetDnD.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.ViewModels
{
    public class WeaponViewModel
    {
        public int CharacterID { get; set; } // To know which character you're adding equipment to
        public string? WeaponName { get; set; }
        public EquipmentType? ItemType { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsEquipped { get; set; }
        public int ValueInGold { get; set; } = 0;

		// Weapon-specific properties
		public DamageDice? DamageDice {  get; set; }

		public DamageType? DamageType { get; set; }
      
		public int WeaponProperties { get; set; } = (int)WeaponProperty.None;
		public WeaponRange WeaponRange { get; set; }

		// Magic-specific propterties
		public bool IsMagic { get; set; } 
		public BonusAttackDamageRolls BonusAttackDamageRolls { get; set; }
		public string? EffectDescription { get; set; }
		public string? EffectMechanics { get; set; }
		public int? Charges { get; set; }
		public string? RechargeRate { get; set; }

		public IEnumerable<CharacterWeapon>? Weapons { get; set; }


	}
}


