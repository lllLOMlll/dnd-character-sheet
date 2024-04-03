using CharacterSheetDnD.Models;
using CharacterSheetDnD.Utilities;

namespace CharacterSheetDnD.ViewModels
{
	public class MyCharacterViewModel
	{
		// CHARACTER
		public int CharacterID { get; set; }
		public string? Name { get; set; }
		public int Level { get; set; } = 1;
		public string? Background { get; set; }
		public string? PlayerName { get; set; }
		public string? Race { get; set; }
		public string? Alignment { get; set; }
		public int ExperiencePoints { get; set; }
		public string? PersonalityTraits { get; set; }
		public string? Ideals { get; set; }
		public string? Bonds { get; set; }
		public string? Flaws { get; set; }
		public int? Age { get; set; }
		public string? Height { get; set; }
		public string? Weight { get; set; }
		public string? Eyes { get; set; }
		public string? Skin { get; set; }
		public string? Hair { get; set; }
		public string? AvatarUrl { get; set; }

		// MULTICLASS   
		// CharacterClass Properties (Assuming multiple classes)
		// Initialize CharacterClasses as an empty list
		public List<CharacterClassViewModel> CharacterClasses { get; set; } = new List<CharacterClassViewModel>();

		// ViewModel to encapsulate CharacterClass details
		public class CharacterClassViewModel
		{
			public string? Class { get; set; }
			public string? Subclass { get; set; }
			public int Level { get; set; }
		}

		// CHARACTERHEALTH
		public int MaximumHitPoints { get; set; }
		public int CurrentHitPoints { get; set; }
		public int TemporaryHitPoints { get; set; }

		// CHARACTERSTATISTICS
		public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Constitution { get; set; }
		public int Intelligence { get; set; }
		public int Wisdom { get; set; }
		public int Charisma { get; set; }

		// SAVING THROWS
		public class CharacterSavingThrowViewModel
		{
			public int SavingThrowID { get; set; }
			public string? Name { get; set; }

			public bool IsProficient { get; set; }
		}
		public List<CharacterSavingThrowViewModel> CharacterSavingThrows { get; set; }

		

		// SKILLS
		public class CharacterSkillViewModel
		{
			public int SkillID { get; set; }
			public string? Name { get; set; }

			public bool IsProficient { get; set; }
		}
		public List<CharacterSkillViewModel> CharacterSkills { get; set; }

        // EQUIPMENT
        // To get the View
		public EquipmentAddViewModel EquipmentAddViewModel { get; set; }
        public class EquipmentViewModel
		{
			public int EquipmentID { get; set; }
			public string? ItemName { get; set; }
			public string? RechargeRate { get; set; }
            public EquipmentType? ItemType { get; set; }

            public string? Description { get; set; }
			public int Quantity { get; set; }
			public bool IsEquipped { get; set; }
			public int ValueInGold { get; set; }
		}
		public List<EquipmentViewModel> Equipment { get; set; } = new List<EquipmentViewModel>();

		// WEAPONS
		public class WeaponViewModel
		{
			public int EquipmentID { get; set; }
			public string? ItemName { get; set; }
			public string? DamageDice { get; set; }
			public string? DamageType { get; set; } // Enum as string for view purposes
			public string? Properties { get; set; } // Enum flags as string for view purposes
			public string? Range { get; set; }
		}
		public List<WeaponViewModel> Weapons { get; set; } = new List<WeaponViewModel>();

		// ARMORS
		public class ArmorViewModel
		{
			public int EquipmentID { get; set; }
			public string? ItemName { get; set; }
			public int ArmorClass { get; set; }
			public bool StealthDisadvantage { get; set; }
		}
		public List<ArmorViewModel> Armors { get; set; } = new List<ArmorViewModel>();

		// MAGIC ITEMS
		public class MagicItemViewModel
		{
			public int MagicItemID { get; set; }
			public string? EffectDescription { get; set; }
			public string? EffectMechanics { get; set; }
			public int? Charges { get; set; }
			public string? RechargeRate { get; set; }
			public string? LinkedEquipmentItemName { get; set; } // To show if linked to a specific item
		}
		public List<MagicItemViewModel> MagicItems { get; set; } = new List<MagicItemViewModel>();

		// Constructor
		public MyCharacterViewModel()
		{
			CharacterSavingThrows = new List<CharacterSavingThrowViewModel>();
			CharacterSkills = new List<CharacterSkillViewModel>();

            EquipmentAddViewModel = new EquipmentAddViewModel();
        }


	}


}


