namespace CharacterSheetDnD.ViewModels
{
    public class CharacterCreationViewModel
    {
        // From Character.cs (Models)
        public string Name { get; set; }
        public string Race { get; set; }
        // From CharacterClass.cs (Models)
        public string Class { get; set; }
        public int Level { get; set; } = 1;
        // From CharacterStatistic.cs (Models)
        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Constitution { get; set; }

        public int Intelligence { get; set; }

        public int Wisdom { get; set; }

        public int Charisma { get; set; }



    }
}

