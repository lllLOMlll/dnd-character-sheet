using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
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

        public MyCharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("my-character/{id}")]
        public async Task<IActionResult> DisplayCharacter(int id)
        {
			var character = await _context.Characters
		   .Include(c => c.CharacterClasses)
		   .Include(c => c.CharacterHealth)
		   .Include(c => c.CharacterStatistic)
		   .Include(c => c.CharacterSavingThrows)
			   .ThenInclude(cst => cst.SavingThrow)
           .Include(c => c.CharacterSkills)
               .ThenInclude(cs => cs.Skill)
		   .FirstOrDefaultAsync(c => c.CharacterID == id);

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

			// Equipment
			var equipment = await _context.CharacterEquipmentBases
				.Where(e => e.CharacterID == id && !e.IsMagicItem)
				.ToListAsync();
			viewModel.Equipment = equipment.Select(e => new EquipmentViewModel
			{
				EquipmentID = e.EquipmentID,
				ItemName = e.ItemName,
				Description = e.Description,
				Quantity = e.Quantity,
				IsEquipped = e.IsEquipped,
				ValueInGold = e.ValueInGold
			}).ToList();

			// Weapons
			var weapons = await _context.Weapons
				.Where(w => w.CharacterID == id)
				.ToListAsync();
			viewModel.Weapons = weapons.Select(w => new WeaponViewModel
			{
				EquipmentID = w.EquipmentID,
				ItemName = w.ItemName,
				DamageDice = w.DamageDice.ToString(),
				DamageType = w.DamageType.ToString(),
				Properties = w.Properties.ToString(),
				Range = w.Range.ToString()
			}).ToList();

			// Armors
			var armors = await _context.Armors
				.Where(a => a.CharacterID == id)
				.ToListAsync();
			viewModel.Armors = armors.Select(a => new ArmorViewModel
			{
				EquipmentID = a.EquipmentID,
				ItemName = a.ItemName,
				ArmorClass = a.ArmorClass,
				StealthDisadvantage = a.StealthDisadvantage
			}).ToList();

			// Magic Items
			var magicItems = await _context.MagicItems
				.Include(mi => mi.CharacterEquipmentBase)
				.Where(mi => mi.CharacterEquipmentBase.CharacterID == id)
				.ToListAsync();
			viewModel.MagicItems = magicItems.Select(mi => new MagicItemViewModel
			{
				MagicItemID = mi.MagicItemID,
				EffectDescription = mi.EffectDescription,
				EffectMechanics = mi.EffectMechanics,
				Charges = mi.Charges,
				RechargeRate = mi.RechargeRate,
				LinkedEquipmentItemName = mi.CharacterEquipmentBase?.ItemName // Assuming you want to show if it's linked to a specific item
			}).ToList();

			// Return the view viewModel -> MyCharacterViewModel
			return View("MyCharacter", viewModel);
        }




	}
}
