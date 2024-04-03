using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
	public enum EquipmentType
	{
		Weapon,
		Armor,
		WondrousItem,
		Consumable,
		Tool
		// Add other types as needed
	}

	public class CharacterEquipmentBase
	{
		[Key]
		public int EquipmentID { get; set; }

		[ForeignKey("Character")]
		public int CharacterID { get; set; }
		public virtual Character? Character { get; set; }

		[Required]
		[StringLength(255)]
		public string? ItemName { get; set; }

		[Required]
		public EquipmentType ItemType { get; set; }

		public string? Description { get; set; }

		[Required]
		public int Quantity { get; set; }

		public bool IsEquipped { get; set; }
		public bool IsMagicItem { get; set; }

		// Added ValueInGold property
		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "Value in gold must be non-negative")]
		public int ValueInGold { get; set; }
	}
}
