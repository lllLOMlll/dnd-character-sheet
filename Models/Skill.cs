using System.ComponentModel.DataAnnotations;

namespace CharacterSheetDnD.Models
{
	public class Skill
	{
		[Key]
		public int SkillID { get; set; }

		[StringLength(50)]
		public string Name { get; set; } = string.Empty;

		[StringLength(500)]
		public string? Description { get; set; }

		public virtual ICollection<CharacterSkill> CharacterSkills { get; set; }

		// Constructor
		public Skill()
		{
			// Initialize the collection
			this.CharacterSkills = new HashSet<CharacterSkill>();
		}
	}
}
