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
using CharacterSheetDnD.Binders;

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

        [HttpGet]
        [Authorize]
        [Route("my-character/character-armors")]
        public IActionResult DisplayCharacterArmors()
        {
            var selectedCharacterID = HttpContext.Session.GetInt32("SelectedCharacterID");
            if (selectedCharacterID == null)
            {
                TempData["Message"] = "Please select a character before viewing armors.";
                return RedirectToAction("SelectCharacter", "Character");
            }

            var armors = _context.CharacterArmors
                .Where(ca => ca.CharacterID == selectedCharacterID.Value)
                .Select(ca => new GenericArmorViewModel
                {
                    ArmorID = ca.ArmorID,
                    ArmorName = ca.ArmorName,
                    // Populate other necessary fields here
                }).ToList();

            var viewModel = new GenericArmorViewModel
            {
                CharacterID = selectedCharacterID.Value,
                Armors = armors
            };

            return View("CharacterArmors", viewModel);
        }


        [Authorize]
        [Route("my-character/delete-armor")]
        public async Task<IActionResult> DeleteArmor(int armorId)
        {
			var selectedCharacterId = HttpContext.Session.GetInt32("SelectedCharacterID");

            var armorToDelete = await _context.CharacterArmors
										.FirstOrDefaultAsync(a => a.ArmorID == armorId);

			if (armorToDelete != null)
			{
				_context.CharacterArmors.Remove(armorToDelete);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Armor successfully deleted.";
			}
			else
			{
				TempData["ErrorMessage"] = "Armor not found.";
			}

			return RedirectToAction("DisplayCharacterArmors", selectedCharacterId);
        }

		[Authorize]
		[HttpGet("my-character/edit-amor/{armorId:int}")]
		public async Task<IActionResult> DisplaySelectedArmor(int armorId)
		{
			var selectedCharacterId = HttpContext.Session.GetInt32("SelectedCharacterID");
			
			if (selectedCharacterId == null) 
			{
				TempData["ErrorMessage"] = "Character ID not found.";
                return RedirectToAction("DisplayCharacterArmors", selectedCharacterId);            

			}

			var armorToEdit = await _context.CharacterArmors
										.FirstOrDefaultAsync(a => a.ArmorID == armorId);

			if (armorToEdit == null) 
			{
				return NotFound();
			}
			var viewModel = new GenericArmorViewModel
			{
				CharacterID = selectedCharacterId.Value,
				ArmorID = armorToEdit.ArmorID,
				ArmorName = armorToEdit.ArmorName,
			};

			return View("EditArmor", viewModel);
		}

        [HttpPost]
		[Authorize]
		[Route("my-character/add-armor")]
		public async Task<IActionResult> AddArmor(GenericArmorViewModel model)
		{
			if (model == null)
			{
				TempData["ErrorMessage"] = "Model==null";
				return View("AddArmor", model); // Handle null model
			}

			if (!ModelState.IsValid)
			{
				foreach (var state in ModelState)
				{
					if (state.Value.Errors.Count > 0)
					{
						TempData["ErrorMessage"] += $" {state.Key} has errors;";
					}
				}
				return View("AddArmor", model);
			}


			CharacterArmor? newArmor = CreateArmorBasedOnType(model);
			if (newArmor != null)
			{
				var selectedCharacterID = HttpContext.Session.GetInt32("SelectedCharacterID");
				if (selectedCharacterID == null)
				{
					TempData["ErrorMessage"] = "Character ID is missing. Please select a character.";
					return RedirectToAction("SelectCharacter", "Character"); // Or another appropriate action
				}

				// Assign the CharacterID to the newArmor instance
				newArmor.CharacterID = selectedCharacterID.Value;

				_context.CharacterArmors.Add(newArmor);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Armor added successfully!";
				return RedirectToAction("DisplayAddArmorPage"); // Redirect as appropriate
			}

			TempData["ErrorMessage"] = "There was an issue saving the armor. Please check the details and try again.";
			ModelState.AddModelError("", "There was an issue saving the armor. Please check the details and try again.");
			return View("AddArmor", model);
		}

		private CharacterArmor? CreateArmorBasedOnType(GenericArmorViewModel model)
		{
			CharacterArmor? armor = null;

			switch (model.ArmorType)
			{
				case ArmorType.Light:
					if (model.SpecificLightArmorType.HasValue) // Ensure the specific type is provided
					{

						armor = new LightArmor
						{
							ArmorName = model.ArmorName,
							Description = model.Description,
							Quantity = model.Quantity,
							ValueInGold = model.ValueInGold,
							StealthDisadvantage = model.StealthDisadvantage,
							Rarity = model.Rarity.HasValue ? model.Rarity.Value : default(Rarity), // Assuming Rarity is not null due to model validation
							IsEquipped = model.IsEquipped,
							IsMagicItem = model.IsMagic,
							RequiresAttunement = model.RequiresAttunement,
							IsAttuned = model.IsAttuned,
							MagicBonusAC = model.MagicBonusAC.HasValue ? (MagicBonusAC)model.MagicBonusAC.Value : default(MagicBonusAC), // Casting to the enum with a check
							MagicEffectDescription = model.MagicEffectDescription,
							MagicEffectMechanics = model.MagicEffectMechanics,
							MagicCharges = model.MagicCharges,
							MagicRechargeRate = model.MagicRechargeRate,
							LightArmorType = model.SpecificLightArmorType.Value
						};
					}
					break;
				case ArmorType.Medium:
					if (model.SpecificMediumArmorType.HasValue) // Ensure the specific type is provided
					{

						armor = new MediumArmor
						{
							// Common properties
							ArmorName = model.ArmorName,
							Description = model.Description,
							Quantity = model.Quantity,
							ValueInGold = model.ValueInGold,
							StealthDisadvantage = model.StealthDisadvantage,
							Rarity = model.Rarity.HasValue ? model.Rarity.Value : default(Rarity),
							IsEquipped = model.IsEquipped,
							IsMagicItem = model.IsMagic,
							RequiresAttunement = model.RequiresAttunement,
							IsAttuned = model.IsAttuned,
							MagicBonusAC = model.MagicBonusAC.HasValue ? (MagicBonusAC)model.MagicBonusAC.Value : default(MagicBonusAC),
							MagicEffectDescription = model.MagicEffectDescription,
							MagicEffectMechanics = model.MagicEffectMechanics,
							MagicCharges = model.MagicCharges,
							MagicRechargeRate = model.MagicRechargeRate,
							// Specific property
							MediumArmorType = model.SpecificMediumArmorType.Value
						};
					}
					break;

				case ArmorType.Heavy:
					if (model.SpecificHeavyArmorType.HasValue) // Ensure the specific type is provided
					{

						armor = new HeavyArmor
						{
							// Common properties
							ArmorName = model.ArmorName,
							Description = model.Description,
							Quantity = model.Quantity,
							ValueInGold = model.ValueInGold,
							StealthDisadvantage = model.StealthDisadvantage,
							Rarity = model.Rarity.HasValue ? model.Rarity.Value : default(Rarity),
							IsEquipped = model.IsEquipped,
							IsMagicItem = model.IsMagic,
							RequiresAttunement = model.RequiresAttunement,
							IsAttuned = model.IsAttuned,
							MagicBonusAC = model.MagicBonusAC.HasValue ? (MagicBonusAC)model.MagicBonusAC.Value : default(MagicBonusAC),
							MagicEffectDescription = model.MagicEffectDescription,
							MagicEffectMechanics = model.MagicEffectMechanics,
							MagicCharges = model.MagicCharges,
							MagicRechargeRate = model.MagicRechargeRate,
							// Specific property
							HeavyArmorType = model.SpecificHeavyArmorType.Value
						};
					}
					break;

				case ArmorType.Shield:
					// Assuming Shield does not have a specific type like LightArmorType, MediumArmorType, or HeavyArmorType
					// But if there's a specific type or category that needs parsing, apply a similar approach as above.

					armor = new ShieldArmor
					{
						// Common properties
						ArmorName = model.ArmorName,
						Description = model.Description,
						Quantity = model.Quantity,
						ValueInGold = model.ValueInGold,
						StealthDisadvantage = model.StealthDisadvantage,
						Rarity = model.Rarity.HasValue ? model.Rarity.Value : default(Rarity),
						IsEquipped = model.IsEquipped,
						IsMagicItem = model.IsMagic,
						RequiresAttunement = model.RequiresAttunement,
						IsAttuned = model.IsAttuned,
						MagicBonusAC = model.MagicBonusAC.HasValue ? (MagicBonusAC)model.MagicBonusAC.Value : default(MagicBonusAC),
						MagicEffectDescription = model.MagicEffectDescription,
						MagicEffectMechanics = model.MagicEffectMechanics,
						MagicCharges = model.MagicCharges,
						MagicRechargeRate = model.MagicRechargeRate,
						// No specific property to set for Shield, but include if applicable
					};
					break;

			}

			return armor;
		}



	}



}

