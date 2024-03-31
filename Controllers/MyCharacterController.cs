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
		   .FirstOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
            {
                return NotFound();
            }

            var viewModel = new MyCharacterViewModel
            {
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

                CharacterClasses = character.CharacterClasses.Select(cc => new CharacterClassViewModel
                {
                    Class = cc.Class,
                    Subclass = cc.Subclass,
                    Level = cc.Level
                }).ToList(),

                MaximumHitPoints = character.CharacterHealth?.MaximumHitPoints ?? 0,
                CurrentHitPoints = character.CharacterHealth?.CurrentHitPoints ?? 0,
                TemporaryHitPoints = character.CharacterHealth?.TemporaryHitPoints ?? 0,

                Strength = character.CharacterStatistic?.Strength ?? 0,
                Dexterity = character.CharacterStatistic?.Dexterity ?? 0,
                Constitution = character.CharacterStatistic?.Constitution ?? 0,
                Intelligence = character.CharacterStatistic?.Intelligence ?? 0,
                Wisdom = character.CharacterStatistic?.Wisdom ?? 0,
                Charisma = character.CharacterStatistic?.Charisma ?? 0,
            };

			// Now, perform the operations that require awaiting or looping
			var savingThrows = await _context.SavingThrows.ToListAsync();
			foreach (var savingThrow in savingThrows)
			{
				var characterSavingThrow = character.CharacterSavingThrows
					.FirstOrDefault(cst => cst.SavingThrowID == savingThrow.SavingThrowID);

				viewModel.CharacterSavingThrows.Add(new CharacterSavingThrowViewModel
				{
					SavingThrowID = savingThrow.SavingThrowID,
					IsProficient = characterSavingThrow?.IsProficient ?? false
				});
			}
			// MyCharacter is the cshtml file 
			return View("MyCharacter", viewModel);
        }




	}
}
