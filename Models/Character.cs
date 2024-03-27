using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }

        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;
        // This the Character's Level (and not the class level)
        public int Level { get; set; } = 0;
        
        [StringLength(200)]
        public string Background { get; set; } = string.Empty;

        [StringLength(100)]
        public string PlayerName { get; set; } = string.Empty;

        [StringLength(50)]
        public string Race { get; set; } = string.Empty;

        [StringLength(50)]
        public string Alignment { get; set; } = string.Empty;

        public string Deity { get; set; } = string.Empty;

        public int ExperiencePoints { get; set; } = 0;

        [StringLength(500)]
        public string PersonalityTraits { get; set; } = string.Empty;

        [StringLength(500)]
        public string Ideals { get; set; } = string.Empty;

        [StringLength(500)]
        public string Bonds { get; set; } = string.Empty;

        [StringLength(500)]
        public string Flaws { get; set; } = string.Empty;

        public int? Age { get; set; }

        public string Size { get; set; } = string.Empty;

        [StringLength(50)]
        public string Height { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Weight { get; set; } = string.Empty;

        [StringLength(50)]
        public string Eyes { get; set; } = string.Empty;

        [StringLength(50)]
        public string Skin { get; set; } = string.Empty;

        [StringLength(50)]
        public string Hair { get; set; } = string.Empty;

        [Url]
        public string AvatarUrl { get; set; } = string.Empty;

        // Foreign for ASP.NET Core Identity User
        public string? UserId {  get; set; }
        // Navigation property to the Identity User
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }



        // Navigation properties
        public virtual ICollection<CharacterClass> CharacterClasses { get; set; }
        public virtual CharacterStatistic? CharacterStatistic { get; set; }
        public virtual CharacterHealth? CharacterHealth { get; set; }
        // Add other navigation properties as needed

        // Constructor to initialize the collections
        public Character()
        {
            this.CharacterClasses = new HashSet<CharacterClass>();
            // Initialize other collections here
        }
    }
}
