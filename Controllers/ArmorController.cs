using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;
using CharacterSheetDnD.ViewModels;
using CharacterSheetDnD.Migrations;
using SQLitePCL;
using static CharacterSheetDnD.ViewModels.ArmorViewModel;
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
			
			var selectedCharacterID = HttpContext.Session.GetInt32("SelectedCharacterID");
			if (selectedCharacterID == null)
			{
				TempData["Message"] = "Please select a character before adding armor.";
				return RedirectToAction("SelectCharacter", "Character"); 
			}

			var viewModel = new GenericArmorViewModel
			{
				// Initialize your ViewModel here as needed
				// For example, you might want to load specific data based on the selected character
			};

			return View("AddArmor", viewModel);
		}

		[HttpPost]
		[Authorize]
		[Route("my-character/add-armor")]
		public async Task<IActionResult> AddArmor(ArmorViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			CharacterArmor newArmor = null;

			switch (model)
			{
				case LightArmorViewModel lightArmorViewModel:
					newArmor = new LightArmor
					{
						// Mapping common properties
						CharacterID = lightArmorViewModel.CharacterID,
						ArmorName = lightArmorViewModel.ArmorName,
						Description = lightArmorViewModel.Description,
						Rarity = lightArmorViewModel.Rarity,
						Quantity = lightArmorViewModel.Quantity,
						ValueInGold = lightArmorViewModel.ValueInGold,
						StealthDisadvantage = lightArmorViewModel.StealthDisadvantage,
						IsEquipped = lightArmorViewModel.IsEquipped,
						IsMagicItem = lightArmorViewModel.IsMagicItem,
						RequiresAttunement = lightArmorViewModel.RequiresAttunement,
						IsAttuned = lightArmorViewModel.IsAttuned,
						MagicBonusAC = lightArmorViewModel.MagicBonusAC,
						MagicEffectDescription = lightArmorViewModel.MagicEffectDescription,
						MagicEffectMechanics = lightArmorViewModel.MagicEffectMechanics,
						MagicCharges = lightArmorViewModel.MagicCharges,
						MagicRechargeRate = lightArmorViewModel.MagicRechargeRate,
						// Mapping specific property
						LightArmorType = lightArmorViewModel.LightArmorType
					};
					break;
				case MediumArmorViewModel mediumArmorViewModel:
					newArmor = new MediumArmor
					{
						// Mapping common properties
						CharacterID = mediumArmorViewModel.CharacterID,
						ArmorName = mediumArmorViewModel.ArmorName,
						Description = mediumArmorViewModel.Description,
						Rarity = mediumArmorViewModel.Rarity,
						Quantity = mediumArmorViewModel.Quantity,
						ValueInGold = mediumArmorViewModel.ValueInGold,
						StealthDisadvantage = mediumArmorViewModel.StealthDisadvantage,
						IsEquipped = mediumArmorViewModel.IsEquipped,
						IsMagicItem = mediumArmorViewModel.IsMagicItem,
						RequiresAttunement = mediumArmorViewModel.RequiresAttunement,
						IsAttuned = mediumArmorViewModel.IsAttuned,
						MagicBonusAC = mediumArmorViewModel.MagicBonusAC,
						MagicEffectDescription = mediumArmorViewModel.MagicEffectDescription,
						MagicEffectMechanics = mediumArmorViewModel.MagicEffectMechanics,
						MagicCharges = mediumArmorViewModel.MagicCharges,
						MagicRechargeRate = mediumArmorViewModel.MagicRechargeRate,
						// Mapping specific property
						MediumArmorType = mediumArmorViewModel.MediumArmorType
					};
					break;
				case HeavyArmorViewModel heavyArmorViewModel:
					newArmor = new HeavyArmor
					{
						// Mapping common properties
						CharacterID = heavyArmorViewModel.CharacterID,
						ArmorName = heavyArmorViewModel.ArmorName,
						Description = heavyArmorViewModel.Description,
						Rarity = heavyArmorViewModel.Rarity,
						Quantity = heavyArmorViewModel.Quantity,
						ValueInGold = heavyArmorViewModel.ValueInGold,
						StealthDisadvantage = heavyArmorViewModel.StealthDisadvantage,
						IsEquipped = heavyArmorViewModel.IsEquipped,
						IsMagicItem = heavyArmorViewModel.IsMagicItem,
						RequiresAttunement = heavyArmorViewModel.RequiresAttunement,
						IsAttuned = heavyArmorViewModel.IsAttuned,
						MagicBonusAC = heavyArmorViewModel.MagicBonusAC,
						MagicEffectDescription = heavyArmorViewModel.MagicEffectDescription,
						MagicEffectMechanics = heavyArmorViewModel.MagicEffectMechanics,
						MagicCharges = heavyArmorViewModel.MagicCharges,
						MagicRechargeRate = heavyArmorViewModel.MagicRechargeRate,
						// Mapping specific property
						HeavyArmorType = heavyArmorViewModel.HeavyArmorType
					};
					break;
				case ShieldViewModel shieldViewModel:
					newArmor = new ShieldArmor
					{
						// Mapping common properties
						CharacterID = shieldViewModel.CharacterID,
						ArmorName = shieldViewModel.ArmorName,
						Description = shieldViewModel.Description,
						Rarity = shieldViewModel.Rarity,
						Quantity = shieldViewModel.Quantity,
						ValueInGold = shieldViewModel.ValueInGold,
						StealthDisadvantage = shieldViewModel.StealthDisadvantage,
						IsEquipped = shieldViewModel.IsEquipped,
						IsMagicItem = shieldViewModel.IsMagicItem,
						RequiresAttunement = shieldViewModel.RequiresAttunement,
						IsAttuned = shieldViewModel.IsAttuned,
						MagicBonusAC = shieldViewModel.MagicBonusAC,
						MagicEffectDescription = shieldViewModel.MagicEffectDescription,
						MagicEffectMechanics = shieldViewModel.MagicEffectMechanics,
						MagicCharges = shieldViewModel.MagicCharges,
						MagicRechargeRate = shieldViewModel.MagicRechargeRate
						// Note: ShieldViewModel does not have a specific property in the provided code snippet.
						// If ShieldArmor has specific properties, they should be mapped here.
					};
					break;
			}

			if (newArmor != null)
			{
				_context.CharacterArmors.Add(newArmor);
				await _context.SaveChangesAsync();
				return RedirectToAction("AddArmor", model); // Make sure this is an actual page or route in your application
			}

			// If the model does not match any expected ViewModel, return to the form with the current model
			return View(model);
		}



	}




}

