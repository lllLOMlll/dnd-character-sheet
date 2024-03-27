using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
	public class SavingThrow
	{
		[Key]
		public int SavingThrowID { get; set; }

		[StringLength(50)]
		public string Name { get; set; } = string.Empty; 

		[StringLength(500)]
		public string? Description { get; set; } 

		public virtual ICollection<CharacterSavingThrow> CharacterSavingThrows { get; set; }

		// Constructor
		public SavingThrow()
		{
			// Initialize the collection
			this.CharacterSavingThrows = new HashSet<CharacterSavingThrow>();
		}

	}
}
