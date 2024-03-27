using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
using CharacterSheetDnD.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace CharacterSheetDnD.Controllers
{
    public class CreateCharacterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CreateCharacterController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("character-creation")]
        public IActionResult DisplayCreateCharacter()
        {
            var viewModel = new CharacterCreationViewModel
            {
                AvailableClasses = GetAvailableClasses()
            };
            return View("CreateCharacter", viewModel);
        }

        private IEnumerable<SelectListItem> GetAvailableClasses()
        {
            var classes = new List<SelectListItem>
            {
                // Default selection
                new SelectListItem { Text = "Select a class", Value = "" },
                
                new SelectListItem { Text = "Barbarian", Value = "Barbarian" },
                new SelectListItem { Text = "Bard", Value = "Bard" },
                new SelectListItem { Text = "Cleric", Value = "Cleric" },
                new SelectListItem { Text = "Druid", Value = "Druid" },
                new SelectListItem { Text = "Fighter", Value = "Fighter" },
                new SelectListItem { Text = "Monk", Value = "Monk" },
                new SelectListItem { Text = "Paladin", Value = "Paladin" },
                new SelectListItem { Text = "Ranger", Value = "Ranger" },
                new SelectListItem { Text = "Rogue", Value = "Rogue" },
                new SelectListItem { Text = "Sorcerer", Value = "Sorcerer" },
                new SelectListItem { Text = "Warlock", Value = "Warlock" },
                new SelectListItem { Text = "Wizard", Value = "Wizard" }
            };

            return classes;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCharacter(CharacterCreationViewModel viewModel)
        {
            ModelState.Remove("AvailableClasses");

            if (!ModelState.IsValid)
            {
                viewModel.AvailableClasses = GetAvailableClasses();
                return View("CreateCharacter", viewModel);
            }
            
            if (ModelState.IsValid)
            {
                // I need this to track wich Character(s) belongs to wich User
                var userId = _userManager.GetUserId(User);

                var character = new Character
                {
                    Name = viewModel.Name,
                    Race = viewModel.Race,
                    Level = viewModel.Level,
                    UserId = userId              
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

                var characterHealth = new CharacterHealth
                { 
                    MaximumHitPoints = viewModel.MaximumHitPoints,
                    CurrentHitPoints = viewModel.MaximumHitPoints,
                    CharacterID = character.CharacterID
                };

                _context.CharacterHealths.Add(characterHealth);
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

                // SUCCESS - Redirect to the MyCharacter action in MyCharacterController, passing CharacterID
                return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });


            }

            // If we got this far, something failed; redisplay form
            return View("CreateCharacter", viewModel);
        }

    }
}
