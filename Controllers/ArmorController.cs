using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;
using CharacterSheetDnD.ViewModels;
using CharacterSheetDnD.Migrations;
using SQLitePCL;
using static CharacterSheetDnD.ViewModels.WeaponViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CharacterSheetDnD.Controllers
{
    public class ArmorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArmorController(ApplicationDbContext context)
        {
            _context = context;
        }


		[HttpGet]
		[Authorize]
		[Route("my-character/add-armor")]
		public IActionResult DisplayAddArmorPage()
		{
			// Check if a character has been selected (i.e., if SelectedCharacterID exists in the session)
			var selectedCharacterID = HttpContext.Session.GetInt32("SelectedCharacterID");
			if (selectedCharacterID == null)
			{
				// No character has been selected; redirect or inform the user appropriately
				// For example, redirect to a page where a character can be selected
				// You could also use TempData to pass a message to be displayed on the redirected page
				TempData["Message"] = "Please select a character before adding armor.";
				return RedirectToAction("SelectCharacter", "Character"); // Adjust controller and action names as necessary
			}

			// If a character has been selected, proceed with creating the ViewModel
			var viewModel = new ArmorViewModel
			{
				// Initialize your ViewModel here as needed
				// For example, you might want to load specific data based on the selected character
			};

			return View("AddArmor", viewModel);
		}


	}




}

