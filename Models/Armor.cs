using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
	public class Armor : CharacterEquipmentBase
	{
		[Required]
		public int ArmorClass { get; set; }

		[Required]
		public bool StealthDisadvantage { get; set; }

	}
}
