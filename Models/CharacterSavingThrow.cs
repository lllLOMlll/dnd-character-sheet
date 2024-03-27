using CharacterSheetDnD.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class CharacterSavingThrow
{
	[Key]
	public int CharacterSavingThrowID { get; set; }
	public int CharacterID { get; set; }
	public int SavingThrowID { get; set; }
	public bool IsProficient { get; set; } 

	[ForeignKey("CharacterID")]
	public virtual Character? Character { get; set; }

	[ForeignKey("SavingThrowID")]
	public virtual SavingThrow? SavingThrow { get; set; }
}
