using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
	public enum DamageType
	{
		Slashing,
		Piercing,
		Bludgeoning,
		Magic,
		// Add additional damage types as needed
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
		// Add additional properties as needed
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

	public enum Range
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


	public class Weapon : CharacterEquipmentBase
	{
		[Required]
		[StringLength(255)]
		public DamageDice DamageDice { get; set; }

		[Required]
		public DamageType DamageType { get; set; }

		public Range Range { get; set; }

		// Now using the WeaponProperty enum to represent weapon properties
		public WeaponProperty Properties { get; set; } = WeaponProperty.None;

		// Additional functionality or properties can be added here
	}
}
