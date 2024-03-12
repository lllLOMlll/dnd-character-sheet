using CharacterSheetDnD.Models;
using CharacterSheetDnD.Services;
using CharacterSheetDnD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Text;
using System.Linq;
using static CharacterSheetDnD.ViewModels.CharacterCreationViewModel;


namespace CharacterSheetDnD.Controllers
{
    public class CharacterCreationController : Controller
    {

        private readonly IClassService _classService;

        // Inject IClassService through the controller's constructor
        public CharacterCreationController(IClassService classService)
        {
            _classService = classService;
        }

        [Route("Home/character-creation")]
        public IActionResult CharacterCreation()
        {
            var viewModel = new CharacterCreationViewModel
            {
                Character = new Character(),
                CharacterClasses = new List<ClassLevelViewModel> { new ClassLevelViewModel() }, // Ensure there's at least one entry
                CharacterClass = new CharacterClass(),
                CharacterStatistic = new CharacterStatistic(),
                CharacterHealth = new CharacterHealth(),
                AvailableClasses = _classService.GetAvailableClasses()
                    .Select(c => new SelectListItem { Value = c, Text = c })
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CharacterCreation(CharacterCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the character to the database
                // Redirect to the newly created character page

                return RedirectToAction("Index", "Home"); // Adjust as needed
            }

            // If we're here, something went wrong
            return View(model);
        }

        // Other character-related actions can be added here
    }
}