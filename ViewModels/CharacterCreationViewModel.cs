using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CharacterSheetDnD.ViewModels
{
    public class CharacterCreationViewModel
    {
        public Character Character { get; set; }
        public List<ClassLevelViewModel> CharacterClasses { get; set; } = new List<ClassLevelViewModel>();
        public CharacterClass CharacterClass { get; set; }
        public CharacterStatistic CharacterStatistic { get; set; }
        public CharacterHealth CharacterHealth { get; set; }

        // For displaying available classes in a dropdown
        public IEnumerable<SelectListItem> AvailableClasses { get; set; }

        // To capture the selected class
        public string SelectedClass { get; set; }

        public class ClassLevelViewModel
        {
            public string ClassName { get; set; }
            public int Level { get; set; }
        }
    }
}
