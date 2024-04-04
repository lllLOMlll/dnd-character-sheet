using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;
using CharacterSheetDnD.ViewModels;

namespace CharacterSheetDnD.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

		//[HttpPost]
		//[Authorize]
		//[ValidateAntiForgeryToken]
		//[Route("my-character/add-weapon/{id}")]
		//public async Task<IActionResult> AddWeapon(int id, EquipmentAddViewModel viewModel)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		// Create the base equipment entry
		//		var equipmentBase = new CharacterEquipmentBase
		//		{
		//			CharacterID = id,
		//			ItemName = viewModel.ItemName,
		//			ItemType = EquipmentType.Weapon,
		//			Description = viewModel.Description,
		//			Quantity = viewModel.Quantity,
		//			IsEquipped = viewModel.IsEquipped,
		//			ValueInGold = viewModel.ValueInGold
		//		};

		//		_context.CharacterEquipmentBases.Add(equipmentBase);
		//		await _context.SaveChangesAsync(); // Save to get EquipmentID for FK in Weapon

		//		// Create the weapon entry
		//		var weapon = new Weapon
		//		{
		//			EquipmentID = equipmentBase.EquipmentID, // FK set here
		//			DamageDice = viewModel.DamageDice.Value,
		//			DamageType = viewModel.DamageType.Value,
		//			Range = viewModel.Range.Value,
		//			Properties = (WeaponProperty)viewModel.WeaponProperties
		//		};

		//		_context.Weapons.Add(weapon);

		//		// Check if the weapon is a magic item
		//		if (viewModel.IsMagic)
		//		{
		//			var magicItem = new MagicItem
		//			{
		//				EquipmentID = equipmentBase.EquipmentID, // FK set here
		//				EffectDescription = viewModel.EffectDescription,
		//				EffectMechanics = viewModel.EffectMechanics,
		//				Charges = viewModel.Charges,
		//				RechargeRate = viewModel.RechargeRate
		//			};

		//			_context.MagicItems.Add(magicItem);
		//		}

		//		await _context.SaveChangesAsync(); // Save changes for Weapon and potentially MagicItem

		//		return RedirectToAction("DisplayCharacter", "MyCharacter", new { id });
		//	}

		//	// If we got this far, something failed, redisplay form
		//	return View(viewModel);
		//}



	}
}
