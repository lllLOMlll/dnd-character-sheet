using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
    public class CharacterClass
    {
        [Key]
        public int CharacterID { get; set; }
        [Required, StringLength(50)]
        public string Class { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string Subclass { get; set; }= string.Empty;
        [Range(1,20)]
        public int Level { get; set; } = 1;
        public int CharacterClassId { get; set; }
        [ForeignKey("CharacterID")]

        // Navigation property back to the Character
        public Character? Character { get; set; }

    }
}
