using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
    public class CharacterStatistic
    {
        [Key]
        public int StatisticID { get; set; }
        [Required]
        public int Strength { get; set; }
        [Required]
        public int Dexterity { get; set; }
        [Required]
        public int Constitution { get; set; }
        [Required]
        public int Intelligence { get; set; }
        [Required]
        public int Wisdom { get; set; }
        [Required]
        public int Charisma { get; set; }
        [ForeignKey("CharacterID")]
        public int CharacterID { get; set; }

        // Navigation property back to the Character
        public virtual Character Character { get; set; }
    }
}
