using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
using CharacterSheetDnD.Services;
using CharacterSheetDnD.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static CharacterSheetDnD.ViewModels.MyCharacterViewModel;

namespace CharacterSheetDnD.Controllers
{
	public class MyCharacterController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IArmorService _armorService;

		public MyCharacterController(ApplicationDbContext context, IArmorService armorService)
		{
			_context = context;
			_armorService = armorService;
		}

		[HttpGet]
		[Authorize]
		[Route("my-character/{id}")]


		public async Task<IActionResult> DisplayCharacter(int id)
		{

			// Store the selected character's ID in session
			HttpContext.Session.SetInt32("SelectedCharacterID", id);


			// Inclue all relevent tables from my database
			var character = await _context.Characters
		   .Include(c => c.CharacterClasses)
		   .Include(c => c.CharacterHealth)
		   .Include(c => c.CharacterStatistic)
		   .Include(c => c.CharacterSavingThrows)
			   .ThenInclude(cst => cst.SavingThrow)
		   .Include(c => c.CharacterSkills)
			   .ThenInclude(cs => cs.Skill)
		   .Include(c => c.CharacterWeapons)
		   .Include(c => c.CharacterArmors)
		   .FirstOrDefaultAsync(c => c.CharacterID == id)


		   ;

			if (character == null)
			{
				return NotFound();
			}

			var viewModel = new MyCharacterViewModel
			{
				// Character
				Name = character.Name,
				Level = character.Level,
				CharacterID = character.CharacterID,
				Background = character.Background,
				PlayerName = character.PlayerName,
				Race = character.Race,
				Alignment = character.Alignment,
				ExperiencePoints = character.ExperiencePoints,
				PersonalityTraits = character.PersonalityTraits,
				Ideals = character.Ideals,
				Bonds = character.Bonds,
				Flaws = character.Flaws,
				Age = character.Age,
				Height = character.Height,
				Weight = character.Weight,
				Eyes = character.Eyes,
				Skin = character.Skin,
				Hair = character.Hair,
				AvatarUrl = character.AvatarUrl,

				// Class
				CharacterClasses = character.CharacterClasses.Select(cc => new CharacterClassViewModel
				{
					Class = cc.Class,
					Subclass = cc.Subclass,
					Level = cc.Level
				}).ToList(),

				// Health
				MaximumHitPoints = character.CharacterHealth?.MaximumHitPoints ?? 0,
				CurrentHitPoints = character.CharacterHealth?.CurrentHitPoints ?? 0,
				TemporaryHitPoints = character.CharacterHealth?.TemporaryHitPoints ?? 0,

				// Statistic
				Strength = character.CharacterStatistic?.Strength ?? 0,
				Dexterity = character.CharacterStatistic?.Dexterity ?? 0,
				Constitution = character.CharacterStatistic?.Constitution ?? 0,
				Intelligence = character.CharacterStatistic?.Intelligence ?? 0,
				Wisdom = character.CharacterStatistic?.Wisdom ?? 0,
				Charisma = character.CharacterStatistic?.Charisma ?? 0,

				// Initialize the WeaponAddViewModel within MyCharacterViewModel
				WeaponViewModel = new WeaponViewModel
				{
					CharacterID = id,
				},

				GenericArmorViewModel = new GenericArmorViewModel
				{ 
					CharacterID = id
				}


			};

			// Saving throws
			var savingThrows = await _context.SavingThrows.ToListAsync();
			foreach (var savingThrow in savingThrows)
			{
				var characterSavingThrow = character.CharacterSavingThrows
					.FirstOrDefault(cst => cst.SavingThrowID == savingThrow.SavingThrowID);

				viewModel.CharacterSavingThrows.Add(new CharacterSavingThrowViewModel
				{
					SavingThrowID = savingThrow.SavingThrowID,
					Name = savingThrow.Name,
					IsProficient = characterSavingThrow?.IsProficient ?? false
				});
			}


			// Skills
			var allSkills = await _context.Skills.ToListAsync(); // Retrieve all skills to ensure we have a comprehensive list
			foreach (var skill in allSkills)
			{
				var characterSkill = character.CharacterSkills
					.FirstOrDefault(cs => cs.SkillID == skill.SkillID);

				viewModel.CharacterSkills.Add(new CharacterSkillViewModel
				{
					SkillID = skill.SkillID,
					Name = skill.Name,
					IsProficient = characterSkill?.IsProficient ?? false
				});
			}

			// Weapons         
			if (character.CharacterWeapons != null)
			{
				viewModel.Weapons = character.CharacterWeapons.Select(cw => new WeaponViewModel
				{
					WeaponID = cw.WeaponID,
					CharacterID = cw.CharacterID,
					WeaponName = cw.WeaponName,
					IsProficient = cw.IsProficient,
					MeleeRange = cw.MeleeRange,
					Description = cw.Description,
					Quantity = cw.Quantity,
					IsEquipped = cw.IsEquipped,
					ValueInGold = cw.ValueInGold,
					DamageDice = cw.DamageDice,
					DamageType = cw.DamageType,
					WeaponRange = cw.WeaponRange,
					WeaponProperties = (int)cw.Properties, // This line assumes WeaponProperties in ViewModel is an int. Adjust if it's actually an enum type.
					IsMagic = cw.IsMagicItem,
					RequiresAttunement = cw.RequiresAttunement,
					IsAttuned = cw.IsAttuned,
					BonusAttackDamageRolls = cw.BonusAttackDamageRolls,
					EffectDescription = cw.MagicEffectDescription,
					EffectMechanics = cw.MagicEffectMechanics,
					Charges = cw.MagicCharges,
					RechargeRate = cw.MagicRechargeRate,

				}).ToList();
			}

			// Armors
			// Armors
			if (character.CharacterArmors != null)
			{
				viewModel.Armors = character.CharacterArmors.Select(ca =>
				{
					var armorViewModel = new GenericArmorViewModel
					{
						ArmorID = ca.ArmorID,
						CharacterID = ca.CharacterID,
						ArmorName = ca.ArmorName,
						ArmorType = ca.ArmorType,
						Description = ca.Description,
						Quantity = ca.Quantity,
						IsEquipped = ca.IsEquipped,
						ValueInGold = ca.ValueInGold,
						StealthDisadvantage = ca.StealthDisadvantage,
						Rarity = ca.Rarity,
						IsMagic = ca.IsMagicItem,
						RequiresAttunement = ca.RequiresAttunement,
						IsAttuned = ca.IsAttuned,
						MagicBonusAC = (int?)ca.MagicBonusAC,
						MagicEffectDescription = ca.MagicEffectDescription,
						MagicCharges = ca.MagicCharges,
						MagicRechargeRate = ca.MagicRechargeRate,
					};

					// Determine the specific armor type and set it accordingly
					switch (ca)
					{
						case LightArmor la:
							armorViewModel.SpecificLightArmorType = la.LightArmorType;
							break;
						case MediumArmor ma:
							armorViewModel.SpecificMediumArmorType = ma.MediumArmorType;
							break;
						case HeavyArmor ha:
							armorViewModel.SpecificHeavyArmorType = ha.HeavyArmorType;
							break;
					}

					return armorViewModel;
				}).ToList();


			}

			// Here's an example of using the armor service to calculate total AC
			int dexterityModifier = CharacterSheetDnD.Utilities.AbilityScoreUtility.CalculateModifier(character.CharacterStatistic?.Dexterity ?? 0);
			int totalAC = _armorService.CalculateTotalAC(id, dexterityModifier);
			viewModel.TotalAC = totalAC; // Assuming your viewModel has a TotalAC property


			// Return the view viewModel -> MyCharacterViewModel
			return View("MyCharacter", viewModel);
		}




	}
}
