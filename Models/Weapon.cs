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

	public class Weapon : CharacterEquipmentBase
	{
		[Required]
		[StringLength(255)]
		public string? DamageDice { get; set; }

		[Required]
		public DamageType DamageType { get; set; }

		public string? Range { get; set; }

		// Now using the WeaponProperty enum to represent weapon properties
		public WeaponProperty Properties { get; set; } = WeaponProperty.None;

		// Additional functionality or properties can be added here
	}
}
