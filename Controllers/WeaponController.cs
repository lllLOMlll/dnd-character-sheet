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
            viewModel.IsProficient = Request.Form["IsProficient"] == "true" || Request.Form["IsProficient"].Contains("on");
            viewModel.IsEquipped = Request.Form["IsEquipped"] == "true" || Request.Form["IsEquipped"].Contains("on");
            viewModel.IsMagic = Request.Form["IsMagic"] == "true" || Request.Form["IsMagic"].Contains("on");
            viewModel.RequiresAttunement = Request.Form["RequiresAttunement"] == "true" || Request.Form["RequiresAttunement"].Contains("on");
			viewModel.IsAttuned = Request.Form["IsAttuned"] == "true" || Request.Form["IsAttuned"].Contains("on");


			var characterExists = await _context.Characters.AnyAsync(c => c.CharacterID == id);
            if (!characterExists)
            {
                return NotFound($"Character with ID {id} does not exist.");
            }

            var weapon = new CharacterWeapon
            {
                CharacterID = id,       
                WeaponName = viewModel.WeaponName,
                IsProficient = viewModel.IsProficient,
                Description = viewModel.Description,
                MeleeRange = viewModel.MeleeRange.GetValueOrDefault(),
				Quantity = viewModel.Quantity,
                ValueInGold = viewModel.ValueInGold,
                DamageDice = viewModel.DamageDice.GetValueOrDefault(),
                DamageType = viewModel.DamageType.GetValueOrDefault(),
                WeaponRange = viewModel.WeaponRange,
                Properties = (WeaponProperty)viewModel.WeaponProperties,
                IsEquipped = viewModel.IsEquipped,
                IsMagicItem = viewModel.IsMagic,
                RequiresAttunement = viewModel.RequiresAttunement,
                IsAttuned = viewModel.IsAttuned,
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
                Weapons = weapons,
                CharacterID = id
            };

            return View("SelectWeapon", ViewModel);
        }

        [Authorize]
        [Route("my-character/delete-weapon")]
        public async Task<IActionResult> DeleteWeapon(int weaponId, int characterId)
        {
            var weaponToDelete = await _context.Weapons
        .FirstOrDefaultAsync(w => w.WeaponID == weaponId);

            if (weaponToDelete != null)
            {
                _context.Weapons.Remove(weaponToDelete);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Weapon successfully deleted!";
            }
            else
            {
                // Handle the case where the weapon wasn't found
                TempData["ErrorMessage"] = "Weapon not found.";
            }

            return RedirectToAction("DisplayCharacterWeapons", new { id = characterId });
        }

        [Authorize]
        [HttpGet("my-character/edit-weapon/{weaponId:int}/{characterId:int}")]
        public async Task<IActionResult> DisplaySelectedWeapon(int weaponId, int characterId)
        {
            var weaponToEdit = await _context.Weapons.FirstOrDefaultAsync(w => w.WeaponID == weaponId);

            if (weaponToEdit == null)
            {
                return NotFound(); // Handle the case where the weapon doesn't exist
            }

            // Prepare the view model with the details of the weapon to edit
            var viewModel = new WeaponViewModel
            {
                // Assuming CharacterID is used to link back to the character, and it's known       
                CharacterID = characterId,
                WeaponID = weaponToEdit.WeaponID,
                WeaponName = weaponToEdit.WeaponName,
                IsProficient = weaponToEdit.IsProficient,
                MeleeRange = weaponToEdit.MeleeRange,
                WeaponRange = weaponToEdit.WeaponRange,
                Description = weaponToEdit.Description,
                Quantity = weaponToEdit.Quantity,
                IsEquipped = weaponToEdit.IsEquipped,
                ValueInGold = weaponToEdit.ValueInGold,
                DamageDice = weaponToEdit.DamageDice,
                DamageType = weaponToEdit.DamageType,
                AvailableProperties = Enum.GetValues(typeof(WeaponProperty))
        .Cast<WeaponProperty>()
        .Where(p => p != WeaponProperty.None) 
        .Select(p => new WeaponPropertyOption
        {
            Value = p,
            Text = p.ToString(),
            IsSelected = (weaponToEdit.Properties & p) == p
        }).ToList(),     
                IsMagic = weaponToEdit.IsMagicItem,
                RequiresAttunement = weaponToEdit.RequiresAttunement,
                IsAttuned = weaponToEdit.IsAttuned,
                BonusAttackDamageRolls = weaponToEdit.BonusAttackDamageRolls,
                EffectDescription = weaponToEdit.MagicEffectDescription,
                EffectMechanics = weaponToEdit.MagicEffectMechanics,
                Charges = weaponToEdit.MagicCharges,
                RechargeRate = weaponToEdit.MagicRechargeRate
            };

            return View("EditWeapon", viewModel);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("my-character/save-weapon-changes/{id}")]
        public async Task<IActionResult> SaveWeaponChanges(int id, int characterId, WeaponViewModel viewModel)
        {
            //viewModel.IsEquipped = Request.Form["IsEquipped"] == "true" || Request.Form["IsEquipped"].Contains("on");
            //viewModel.IsMagic = Request.Form["IsMagic"] == "true" || Request.Form["IsMagic"].Contains("on");


            if (!ModelState.IsValid)
            {
                return View("EditWeapon", viewModel); // Return the view with validation errors.
            }

            var weaponToUpdate = await _context.Weapons.FirstOrDefaultAsync(w => w.WeaponID == id);

            if (weaponToUpdate == null)
            {
                return NotFound($"Weapon with ID {id} does not exist.");
            }

            // Updating the weapon properties from the viewModel.
            weaponToUpdate.WeaponName = viewModel.WeaponName;
            weaponToUpdate.IsProficient = viewModel.IsProficient;
            weaponToUpdate.MeleeRange = viewModel.MeleeRange.GetValueOrDefault();
            weaponToUpdate.WeaponRange = viewModel.WeaponRange;
            weaponToUpdate.Description = viewModel.Description;
            weaponToUpdate.Quantity = viewModel.Quantity;
            weaponToUpdate.IsEquipped = viewModel.IsEquipped;
            weaponToUpdate.ValueInGold = viewModel.ValueInGold;
            weaponToUpdate.DamageDice = viewModel.DamageDice.GetValueOrDefault();
            weaponToUpdate.DamageType = viewModel.DamageType.GetValueOrDefault();
            weaponToUpdate.Properties = viewModel.SelectedProperties; 
            weaponToUpdate.IsMagicItem = viewModel.IsMagic;
            weaponToUpdate.RequiresAttunement = viewModel.RequiresAttunement;
            weaponToUpdate.IsAttuned = viewModel.IsAttuned;
            weaponToUpdate.BonusAttackDamageRolls = viewModel.BonusAttackDamageRolls;
            weaponToUpdate.MagicEffectDescription = viewModel.EffectDescription;
            weaponToUpdate.MagicEffectMechanics = viewModel.EffectMechanics;
            weaponToUpdate.MagicCharges = viewModel.Charges;
            weaponToUpdate.MagicRechargeRate = viewModel.RechargeRate;

               _context.Weapons.Update(weaponToUpdate);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Weapon updated successfully.";

            var weapons = await _context.Weapons
       .Where(w => w.CharacterID == characterId)
       .ToListAsync();

            var ViewModel = new WeaponViewModel
            {
                Weapons = weapons,
                CharacterID = characterId
            };

            return View("SelectWeapon", ViewModel);
        }






    }




}

