using CharacterSheetDnD.Models;

namespace CharacterSheetDnD.ViewModels
{
    public class CharacterCreationViewModel
    {
        public Character Character { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public CharacterStatistic CharacterStatistic { get; set; }
        //public CharacterHealth characterHealth { get; set; }
    }
}
