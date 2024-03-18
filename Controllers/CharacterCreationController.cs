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
    public class CharacterCreationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CharacterCreationController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("Home/character-creation")]
        public IActionResult CharacterCreation()
        {
            var viewModel = new CharacterCreationViewModel
            {
                AvailableClasses = GetAvailableClasses()
            };
            return View(viewModel);
        }

        private IEnumerable<SelectListItem> GetAvailableClasses()
        {
            // This method would fetch available classes from a database or any other source
            // For demonstration, it's hard-coded
            var classes = new List<string> { "Bard", "Cleric", "Paladin", "Wizard" };
            return classes.Select(c => new SelectListItem(c, c));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCharacter(CharacterCreationViewModel viewModel)
        {
            ModelState.Remove("AvailableClasses");

            if (!ModelState.IsValid)
            {
                viewModel.AvailableClasses = GetAvailableClasses();
                return View("CharacterCreation", viewModel);
            }
            
            if (ModelState.IsValid)
            {
                // I need this to track wich Character(s) belongs to wich User
                var userId = _userManager.GetUserId(User);

                var character = new Character
                {
                    Name = viewModel.Name,
                    Race = viewModel.Race,
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
                return RedirectToAction("MyCharacter", "MyCharacter", new { id = character.CharacterID });


            }

            // If we got this far, something failed; redisplay form
            return View("CharacterCreation", viewModel);
        }

    }
}
