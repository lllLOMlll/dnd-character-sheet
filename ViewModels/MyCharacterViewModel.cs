﻿namespace CharacterSheetDnD.ViewModels
{
    public class MyCharacterViewModel
    {
        // CHARACTER
        public string Name { get; set; }
        public string Background { get; set; }
        public string PlayerName { get; set; }
        public string Race { get; set; }
        public string Alignment { get; set; }
        public int ExperiencePoints { get; set; }
        public string PersonalityTraits { get; set; }
        public string Ideals { get; set; }
        public string Bonds { get; set; }
        public string Flaws { get; set; }
        public int? Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Eyes { get; set; }
        public string Skin { get; set; }
        public string Hair { get; set; }
        public string AvatarUrl { get; set; }

        // MULTICLASS   
        // CharacterClass Properties (Assuming multiple classes)
        public List<CharacterClassViewModel> CharacterClasses { get; set; }

        // ViewModel to encapsulate CharacterClass details
        public class CharacterClassViewModel
        {
            public string Class { get; set; }
            public string Subclass { get; set; }
            public int Level { get; set; }
        }

        // CHARACTERHEALTH
        public int MaximumHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }

        // CHARACTERCLASS
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        // Additional properties can be added here as needed
    }


}

