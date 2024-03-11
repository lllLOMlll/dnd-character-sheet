using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
    public class CharacterHealth
    {
        [Key]
        public int HealthID { get; set; }

        [Required]
        public int MaximumHitPoints { get; set; }

        [Required]
        public int CurrentHitPoints { get; set; }

        public int TemporaryHitPoints { get; set; }

        [ForeignKey("Character")]
        public int CharacterID { get; set; }

        // Navigation property back to the Character
        public virtual Character? Character { get; set; }
    }
}
