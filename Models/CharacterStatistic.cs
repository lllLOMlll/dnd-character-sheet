using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
    public class CharacterStatistic
    {
        [Key]
        public int StatisticID { get; set; }
      
        public int Strength { get; set; }
     
        public int Dexterity { get; set; }
        
        public int Constitution { get; set; }
      
        public int Intelligence { get; set; }
        
        public int Wisdom { get; set; }
       
        public int Charisma { get; set; }
        [ForeignKey("CharacterID")]
        public int CharacterID { get; set; }

        // Navigation property back to the Character
        public virtual Character? Character { get; set; }
    }
}
