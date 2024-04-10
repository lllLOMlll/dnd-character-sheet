using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{

	public enum MeleeRange
	{
		Melee,
		Range
	}

	public enum DamageType
	{
		Slashing,
		Piercing,
		Bludgeoning,
		Magic,
	}

	[Flags]
	public enum WeaponProperty
	{
		None = 0,
		Light = 1 << 0,
		Finesse = 1 << 1,
		Thrown = 1 << 2,
		Versatile = 1 << 3,
		Ammunition = 1 << 4,
		Heavy = 1 << 5,
		Reach = 1 << 6,
		TwoHanded = 1 << 7,
		Special = 1 << 8,
		Loading = 1 << 9
	}

	public enum DamageDice
	{
		[Display(Name = "1d4")]
		D4_1 = 1,
		[Display(Name = "1d6")]
		D6_1 = 2,
		[Display(Name = "1d8")]
		D8_1 = 3,
		[Display(Name = "1d10")]
		D10_1 = 4,
		[Display(Name = "1d12")]
		D12_1 = 5,
		[Display(Name = "2d4")]
		D4_2 = 6,
		[Display(Name = "2d6")]
		D6_2 = 7,
		[Display(Name = "2d8")]
		D8_2 = 8,
		[Display(Name = "2d10")]
		D10_2 = 9,
		[Display(Name = "2d12")]
		D12_2 = 10,
	}

	public enum WeaponRange
	{
		[Display(Name = "5/15")]
		R1 = 1,
		[Display(Name = "20/60")]
		R2 = 2,
		[Display(Name = "30/120")]
		R3 = 3,
		[Display(Name = "80/320")]
		R4 = 4,
		[Display(Name = "100/400")]
		R5 = 5,
		[Display(Name = "150/600")]
		R6 = 6,	
	}

    public enum BonusAttackDamageRolls
    {
        [Display(Name = "+0")]
        None = 0,

        [Display(Name = "+1")]
        B1 = 1,

        [Display(Name = "+2")]
        B2 = 2,

        [Display(Name = "+3")]
        B3 = 3,

        [Display(Name = "+4")]
        B4 = 4,

        [Display(Name = "+5")]
        B5 = 5,

    }


    public class CharacterWeapon 
	{
		[Key]
		public int WeaponID { get; set; }

		[ForeignKey("CharacterID")]
		public int CharacterID { get; set; }
		
		public virtual Character? Character { get; set; }

		[Required]
		[StringLength(255)]
		public string? WeaponName { get; set; }
		
		public bool IsProficient { get; set; }
		
		public MeleeRange MeleeRange { get; set; }
		
		public string? Description { get; set; }

		[Required]
		public int Quantity { get; set; } = 1;
	
		[Range(0, int.MaxValue, ErrorMessage = "Value in gold must be non-negative")]
		public int ValueInGold { get; set; }
		
		[Required]
		[StringLength(255)]
		public DamageDice DamageDice { get; set; }

		[Required]
		public DamageType DamageType { get; set; }

		public WeaponRange WeaponRange { get; set; }

		public WeaponProperty Properties { get; set; } = WeaponProperty.None;

		public bool IsEquipped { get; set; }
		
		public bool IsMagicItem { get; set; }
		public BonusAttackDamageRolls BonusAttackDamageRolls { get; set; }

		public string? MagicEffectDescription { get; set; }

		public string? MagicEffectMechanics { get; set; }
		[Range(0, 100, ErrorMessage = "Charges must be a positive number")]
		
		public int? MagicCharges { get; set; }

		public string? MagicRechargeRate { get; set; }
	}
}
