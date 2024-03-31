using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;

namespace CharacterSheetDnD.Controllers
{
	public class SavingThrowController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SavingThrowController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-strength-proficiency/{id}")]
		public async Task<IActionResult> UpdateStrengthProficiency(int id, IFormCollection formData)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterSavingThrows)
											   .ThenInclude(cst => cst.SavingThrow)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			// Find the 'Strength' saving throw entry
			var savingThrow = await _context.SavingThrows
							.FirstOrDefaultAsync(st => st.Name == "Strength");


			if (savingThrow == null)
			{
				return NotFound("Saving throw 'Strength' not found.");
			}

			// Parse the 'IsProficient' form value
			bool isProficient = formData["IsProficient"].Contains("True");

			var characterSavingThrow = character.CharacterSavingThrows
												 .FirstOrDefault(cst => cst.SavingThrowID == savingThrow.SavingThrowID);
			if (characterSavingThrow == null)
			{
				characterSavingThrow = new CharacterSavingThrow
				{
					CharacterID = id,
					SavingThrowID = savingThrow.SavingThrowID,
					IsProficient = isProficient
				};
				_context.CharacterSavingThrows.Add(characterSavingThrow);
			}
			else
			{
				characterSavingThrow.IsProficient = isProficient;
			}

			await _context.SaveChangesAsync();

			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id });
		}


	}
}
