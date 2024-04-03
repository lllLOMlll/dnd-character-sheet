using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetDnD.Models
{
	public class CharacterSkill
	{
		[Key]
		public int CharacterSkillID { get; set; }
		public int CharacterID { get; set; }
		public int SkillID { get; set; }
		public bool IsProficient { get; set; }

		[ForeignKey("CharacterID")]
		public virtual Character? Character { get; set; }

		[ForeignKey("SkillID")]
		public virtual Skill? Skill{ get; set; }
	}
}
