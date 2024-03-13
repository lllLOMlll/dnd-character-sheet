using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
using CharacterSheetDnD.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace CharacterSheetDnD.Controllers
{
    public class CharacterCreationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterCreationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("Home/character-creation")]
        public IActionResult CharacterCreation()
        {
            var viewModel = new CharacterCreationViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCharacter(CharacterCreationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var character = new Character
                {
                    Name = viewModel.Name,
                    Race = viewModel.Race,
                    // Assuming you initialize related collections in the Character constructor
                };

                _context.Characters.Add(character);
                await _context.SaveChangesAsync();

                var characterClass = new CharacterClass
                {
                    Class = viewModel.Class,
                    Level = viewModel.Level,
                    CharacterID = character.CharacterID // EF Core should have filled this in after SaveChangesAsync
                };

                _context.CharacterClasses.Add(characterClass);
                await _context.SaveChangesAsync();
    
                var characterStatistic = new CharacterStatistic
                {
                    Strength = viewModel.Strength,
                    Dexterity = viewModel.Dexterity,
                    Constitution = viewModel.Constitution,
                    Intelligence = viewModel.Intelligence,
                    Wisdom = viewModel.Wisdom,
                    Charisma = viewModel.Charisma,
                    CharacterID = character.CharacterID
                };

                _context.CharacterStatistics.Add(characterStatistic);
                await _context.SaveChangesAsync();

                // Redirect to the MyCharacter action in MyCharacterController, passing CharacterID
                return RedirectToAction("MyCharacter", "MyCharacter", new { id = character.CharacterID });


            }

            // If we got this far, something failed; redisplay form
            return View("CharacterCreation", viewModel);
        }

    }
}
