using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
	public class MagicItem
	{
		[Key]
		public int MagicItemID { get; set; }

		// Nullable FK to allow for magic items that might not be linked to a specific piece of equipment
		[ForeignKey("CharacterEquipmentBase")]
		public int? EquipmentID { get; set; }
		public virtual CharacterEquipmentBase? CharacterEquipmentBase { get; set; }

		[Required]
		public string? EffectDescription { get; set; }

		public string? EffectMechanics { get; set; }

		public int? Charges { get; set; }

		public string? RechargeRate { get; set; }

	}
}
