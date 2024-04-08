using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;
using CharacterSheetDnD.ViewModels;

namespace CharacterSheetDnD.Controllers
{
    public class WeaponController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeaponController(ApplicationDbContext context)
        {
            _context = context;
        }

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/add-weapon/{id}")]
		public async Task<IActionResult> AddWeapon(int id, WeaponViewModel viewModel)
		{
			// Manually map "on" value to true for checkboxes
			viewModel.IsEquipped = Request.Form["IsEquipped"] == "true" || Request.Form["IsEquipped"].Contains("on");
			viewModel.IsMagic = Request.Form["IsMagic"] == "true" || Request.Form["IsMagic"].Contains("on");


			var characterExists = await _context.Characters.AnyAsync(c => c.CharacterID == id);
			if (!characterExists)
			{
				return NotFound($"Character with ID {id} does not exist.");
			}

			var weapon = new CharacterWeapon
			{
				CharacterID = id,
				WeaponName = viewModel.WeaponName,
				Description = viewModel.Description,
				Quantity = viewModel.Quantity,
				ValueInGold = viewModel.ValueInGold,
				DamageDice = viewModel.DamageDice.GetValueOrDefault(),
				DamageType = viewModel.DamageType.GetValueOrDefault(),
				WeaponRange = viewModel.WeaponRange,
				Properties = (WeaponProperty)viewModel.WeaponProperties,
				IsEquipped = viewModel.IsEquipped,
				IsMagicItem = viewModel.IsMagic,
				BonusAttackDamageRolls = viewModel.BonusAttackDamageRolls,
				MagicEffectDescription = viewModel.EffectDescription,
				MagicEffectMechanics = viewModel.EffectMechanics,
				MagicCharges = viewModel.Charges,
				MagicRechargeRate = viewModel.RechargeRate
				
			};

			await _context.Weapons.AddAsync(weapon);
			

			await _context.SaveChangesAsync();

			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id });
		}

		[Authorize]
		[Route("my-character/choose-weapon/{id}")]
		public async Task<IActionResult> DisplayCharacterWeapons(int id)
		{
			var weapons = await _context.Weapons
			.Where(w => w.CharacterID == id)
			.ToListAsync();

			var ViewModel = new WeaponViewModel
			{
				Weapons = weapons
			};

			return View("SelectWeapon", ViewModel);
		}



		//var characters = await _context.Characters
		//		.Where(c => c.UserId == userId)
		//		.ToListAsync();
		//          return View("SelectCharacter", characters);
	}




}

