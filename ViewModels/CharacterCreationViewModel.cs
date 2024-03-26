using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CharacterSheetDnD.ViewModels
{
    public class CharacterCreationViewModel
    {
        // From Character.cs (Models)
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Race { get; set; }
        // From CharacterClass.cs (Models)
        [Required (ErrorMessage = "Class is required")]
        public string? Class { get; set; }
        // I need that for the dropdown of classes (Bard, Clerir, Paladin, Wizard, etc.)
        // [BindNever] because I had a validation problem. I was not able to save a new character. 'AvailableClasses' should not be binded with the Model
        [BindNever] 
        public IEnumerable<SelectListItem>? AvailableClasses { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "Level must be between 1 and 30")]
        public int Level { get; set; } = 1;
        // From CharacterStatistic.cs (Models)
        [Required]
        [Range(1, 30, ErrorMessage = "Strength must be between 1 and 30")]
        public int Strength { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "Dexterity must be between 1 and 30")]
        public int Dexterity { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "Constitution must be between 1 and 30")]
        public int Constitution { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "Intelligence must be between 1 and 30")]
        public int Intelligence { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "Wisdom must be between 1 and 30")]
        public int Wisdom { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "Charisma must be between 1 and 30")]
        public int Charisma { get; set; }
        [Required]
        [Range(1, 425, ErrorMessage = "You hit points must be between 1 and 450. 450hp, really???")]
        public int MaximumHitPoints { get; set; }



    }
}

