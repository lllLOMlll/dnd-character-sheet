using CharacterSheetDnD.Models;
using CharacterSheetDnD.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        // WEAPON
		public WeaponViewModel WeaponViewModel { get; set; }
        public class CharacterWeapon
		{
			public int WeaponID { get; set; }
			public int CharacterID { get; set; } // To know which character you're adding equipment to
			public string? WeaponName { get; set; }
			public bool IsProficient { get; set; }
			public MeleeRange? MeleeRange { get; set; }
			public string? Description { get; set; }
			public int Quantity { get; set; } = 1;
			public bool IsEquipped { get; set; }
			public int ValueInGold { get; set; } = 0;
			public DamageDice? DamageDice { get; set; }
			public DamageType? DamageType { get; set; }
			public int WeaponProperties { get; set; } = (int)WeaponProperty.None;
			public WeaponRange WeaponRange { get; set; }

			// Magic-specific propterties
			public bool IsMagic { get; set; }
			public bool RequiresAttunement { get; set; }
			public bool IsAttuned { get; set; }
			public BonusAttackDamageRolls BonusAttackDamageRolls { get; set; }
			public string? EffectDescription { get; set; }
			public string? EffectMechanics { get; set; }
			public int? Charges { get; set; }
			public string? RechargeRate { get; set; }


			// This will hold the values for the checkboxes in your form
			public List<WeaponPropertyOption> AvailableProperties { get; set; } = new List<WeaponPropertyOption>();

			// This will hold the selected values when the form is submitted
			public WeaponProperty SelectedProperties { get; set; } = WeaponProperty.None;

			public IEnumerable<CharacterWeapon>? Weapons { get; set; }

			public class WeaponPropertyOption
			{
				public WeaponProperty Value { get; set; }
				public string? Text { get; set; }
				public bool IsSelected { get; set; }
			}

	

		}

		// ARMOR
		public GenericArmorViewModel? GenericArmorViewModel { get; set; }

		public class CharacterArmor
		{
			public int ArmorID { get; set; }
			public int CharacterID { get; set; }
			public virtual Character? Character { get; set; }
			public string? ArmorName { get; set; }
			public Rarity? Rarity { get; set; }
			public string? Description { get; set; }
			public int Quantity { get; set; }
			public int ValueInGold { get; set; }
			public bool StealthDisadvantage { get; set; }
			public ArmorType ArmorType { get; set; }
			public bool IsEquipped { get; set; }
			public bool IsMagicItem { get; set; }
			public bool RequiresAttunement { get; set; }
			public bool IsAttuned { get; set; }
			public MagicBonusAC? MagicBonusAC { get; set; }
			public string? MagicEffectDescription { get; set; }
			public string? MagicEffectMechanics { get; set; }
			public int? MagicCharges { get; set; }
			public string? MagicRechargeRate { get; set; }
			public IEnumerable<CharacterArmor>? Armors { get; set; }
		}


		public List<WeaponViewModel> Weapons { get; set; } = new List<WeaponViewModel>();
		public List<GenericArmorViewModel> Armors { get; set; } = new List<GenericArmorViewModel>();

		// Constructor
		public MyCharacterViewModel()
		{
			CharacterSavingThrows = new List<CharacterSavingThrowViewModel>();
			CharacterSkills = new List<CharacterSkillViewModel>();

			// ViewModel
			Weapons = new List<WeaponViewModel>();
            WeaponViewModel = new WeaponViewModel();
			Armors = new List<GenericArmorViewModel>();
			GenericArmorViewModel = new GenericArmorViewModel();
        }


	}


}


