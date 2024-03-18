using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CharacterSheetDnD.ViewModels
{
    public class CharacterCreationViewModel
    {
        // From Character.cs (Models)
        public string Name { get; set; }
        public string Race { get; set; }
        // From CharacterClass.cs (Models)
        public string Class { get; set; }
        // I need that for the dropdown of classes (Bard, Clerir, Paladin, Wizard, etc.)
        // [BindNever] because I had a validation problem. I was not able to save a new character. 'AvailableClasses' should not be binded with the Model
        [BindNever] 
        public IEnumerable<SelectListItem> AvailableClasses { get; set; }
        public int Level { get; set; } = 1;
        // From CharacterStatistic.cs (Models)
        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Constitution { get; set; }

        public int Intelligence { get; set; }

        public int Wisdom { get; set; }

        public int Charisma { get; set; }

        public int MaximumHitPoints { get; set; }



    }
}

