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
				CharacterID = selectedCharacterID.Value
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
				Rarity = armorToEdit.Rarity,
				Description = armorToEdit.Description,
				Quantity = armorToEdit.Quantity,
				ValueInGold = armorToEdit.ValueInGold,
				StealthDisadvantage = armorToEdit.StealthDisadvantage,
				ArmorType = armorToEdit.ArmorType,
				IsEquipped = armorToEdit.IsEquipped,
				IsMagic = armorToEdit.IsMagicItem,
				RequiresAttunement = armorToEdit.RequiresAttunement,
				IsAttuned = armorToEdit.IsAttuned,
				MagicBonusAC = (int?)armorToEdit.MagicBonusAC,
				MagicEffectDescription = armorToEdit.MagicEffectDescription,
				MagicCharges = armorToEdit.MagicCharges,
				MagicRechargeRate = armorToEdit.MagicRechargeRate


			};

			// To display the Specific armor type (Ligth, Medium and Heavy)
			switch (armorToEdit)
			{
				case LightArmor lightArmor:
					viewModel.SpecificLightArmorType = lightArmor.LightArmorType;
					break;
				case MediumArmor mediumArmor:
					viewModel.SpecificMediumArmorType = mediumArmor.MediumArmorType;
					break;
				case HeavyArmor heavyArmor:  
					viewModel.SpecificHeavyArmorType = heavyArmor.HeavyArmorType;
					break;
			}



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
				return RedirectToAction("AddArmor", model);
			}


			CharacterArmor? newArmor = CreateArmorBasedOnType(model);
			if (newArmor != null)
			{
				var selectedCharacterID = HttpContext.Session.GetInt32("SelectedCharacterID");
				if (selectedCharacterID == null)
				{
					TempData["ErrorMessage"] = "Character ID is missing. Please select a character.";
					return RedirectToAction("SelectCharacter", "Character"); 
				}

				// Assign the CharacterID to the newArmor instance
				newArmor.CharacterID = selectedCharacterID.Value;

				_context.CharacterArmors.Add(newArmor);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Armor added successfully!";
				return RedirectToAction("DisplayAddArmorPage"); 
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
					if (model.SpecificLightArmorType.HasValue) 
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

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/edit-armor/{armorId:int}")]
		public async Task<IActionResult> SaveArmorChanges(GenericArmorViewModel model, int armorId)
		{
			if (model == null)
			{
				TempData["ErrorMessage"] = "No data to save.";
				return View("EditArmor", model);
			}

			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Please correct the errors and try again.";
				return View("EditArmor", model);
			}

			var armorToEdit = await _context.CharacterArmors
											.FirstOrDefaultAsync(a => a.ArmorID == armorId);

			if (armorToEdit == null)
			{
				TempData["ErrorMessage"] = "The armor to edit was not found.";
				return View("EditArmor", model);
			}

		

			if (model.ArmorType != armorToEdit.ArmorType) // Check if the armor type has changed
			{
				var newArmor = CreateArmorBasedOnType(model); // You already have this method
			// Ensure the CharacterID is correctly set on the newArmor instance
				if (newArmor != null)
				{
					var selectedCharacterID = HttpContext.Session.GetInt32("SelectedCharacterID");
					if (selectedCharacterID == null)
					{
						TempData["ErrorMessage"] = "Character ID is missing. Please select a character.";
						return RedirectToAction("SelectCharacter", "Character");
					}

					newArmor.CharacterID = selectedCharacterID.Value; // Ensure this is set correctly

					_context.CharacterArmors.Remove(armorToEdit); // Remove the old armor
					_context.CharacterArmors.Add(newArmor); // Add the new armor

					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Armor updated successfully!";

					return RedirectToAction("DisplayCharacterArmors", new { id = newArmor.CharacterID });
				}
				else
				{
					TempData["ErrorMessage"] = "Failed to create new armor based on type.";
					return View("EditArmor", model);
				}
			}
			else
			{
				armorToEdit.ArmorName = model.ArmorName;
				armorToEdit.Rarity = model.Rarity;
				armorToEdit.Description = model.Description;
				armorToEdit.Quantity = model.Quantity;
				armorToEdit.ValueInGold = model.ValueInGold;
				armorToEdit.StealthDisadvantage = model.StealthDisadvantage;
				armorToEdit.ArmorType = (ArmorType)model.ArmorType;
				armorToEdit.IsEquipped = model.IsEquipped;
				armorToEdit.IsMagicItem = model.IsMagic;
				armorToEdit.RequiresAttunement = model.RequiresAttunement;
				armorToEdit.IsAttuned = model.IsAttuned;
				armorToEdit.MagicBonusAC = (MagicBonusAC?)model.MagicBonusAC;
				armorToEdit.MagicEffectDescription = model.MagicEffectDescription;
				armorToEdit.MagicEffectMechanics = model.MagicEffectMechanics;
				armorToEdit.MagicCharges = model.MagicCharges;
				armorToEdit.MagicRechargeRate = model.MagicRechargeRate;



				_context.CharacterArmors.Update(armorToEdit);
				await _context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Armor updated successfully!";
			}




			return RedirectToAction("DisplayCharacterArmors", new { id = armorToEdit.CharacterID });
		}

		[HttpGet]
		public async Task<IActionResult> CheckArmorEquipped(int characterId)
		{
			var isEquipped = await _context.CharacterArmors
				.AnyAsync(a => a.CharacterID == characterId && a.IsEquipped && a.ArmorType != ArmorType.Shield);

			return Json(isEquipped);
		}


		[HttpPost]
		public async Task<IActionResult> UnequipCurrentArmor(int characterId, int excludeArmorId)
		{
			var equippedArmors = await _context.CharacterArmors
				.Where(a => a.CharacterID == characterId &&
							a.ArmorID != excludeArmorId &&
							a.IsEquipped &&
							a.ArmorType != ArmorType.Shield)
				.ToListAsync();

			if (equippedArmors.Any())
			{
				foreach (var armor in equippedArmors)
				{
					armor.IsEquipped = false;
				}
				await _context.SaveChangesAsync();
			}

			return Json(new { success = true });
		}

		[HttpGet]
		public async Task<IActionResult> CheckShieldEquipped(int characterId)
		{
			var isEquipped = await _context.CharacterArmors
				.AnyAsync(a => a.CharacterID == characterId && a.IsEquipped && a.ArmorType == ArmorType.Shield);

			return Json(isEquipped);
		}

		[HttpPost]
		public async Task<IActionResult> UnequipCurrentShield(int characterId)
		{
			var equippedShields = await _context.CharacterArmors
				.Where(a => a.CharacterID == characterId && a.IsEquipped && a.ArmorType == ArmorType.Shield)
				.ToListAsync();

			if (equippedShields.Any())
			{
				foreach (var shield in equippedShields)
				{
					shield.IsEquipped = false;
				}
				await _context.SaveChangesAsync();
			}

			return Json(new { success = true });
		}




	}

}

