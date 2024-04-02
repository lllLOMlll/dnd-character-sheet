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

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-dexterity-proficiency/{id}")]
		public async Task<IActionResult> UpdateDexterityProficiency(int id, IFormCollection formData)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterSavingThrows)
											   .ThenInclude(cst => cst.SavingThrow)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			// Find the 'Dexterity' saving throw entry
			var savingThrow = await _context.SavingThrows
											.FirstOrDefaultAsync(st => st.Name == "Dexterity");

			if (savingThrow == null)
			{
				return NotFound("Saving throw 'Dexterity' not found.");
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

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-constitution-proficiency/{id}")]
		public async Task<IActionResult> UpdateConstitutionProficiency(int id, IFormCollection formData)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterSavingThrows)
											   .ThenInclude(cst => cst.SavingThrow)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			// Find the 'Constitution' saving throw entry
			var savingThrow = await _context.SavingThrows
											.FirstOrDefaultAsync(st => st.Name == "Constitution");

			if (savingThrow == null)
			{
				return NotFound("Saving throw 'Constitution' not found.");
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

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-intelligence-proficiency/{id}")]
		public async Task<IActionResult> UpdateIntelligenceProficiency(int id, IFormCollection formData)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterSavingThrows)
											   .ThenInclude(cst => cst.SavingThrow)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			// Find the 'Intelligence' saving throw entry
			var savingThrow = await _context.SavingThrows
											.FirstOrDefaultAsync(st => st.Name == "Intelligence");

			if (savingThrow == null)
			{
				return NotFound("Saving throw 'Intelligence' not found.");
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

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-wisdom-proficiency/{id}")]
		public async Task<IActionResult> UpdateWisdomProficiency(int id, IFormCollection formData)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterSavingThrows)
											   .ThenInclude(cst => cst.SavingThrow)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			// Find the 'Wisdom' saving throw entry
			var savingThrow = await _context.SavingThrows
											.FirstOrDefaultAsync(st => st.Name == "Wisdom");

			if (savingThrow == null)
			{
				return NotFound("Saving throw 'Wisdom' not found.");
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

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-charisma-proficiency/{id}")]
		public async Task<IActionResult> UpdateCharismaProficiency(int id, IFormCollection formData)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterSavingThrows)
											   .ThenInclude(cst => cst.SavingThrow)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			// Find the 'Charisma' saving throw entry
			var savingThrow = await _context.SavingThrows
											.FirstOrDefaultAsync(st => st.Name == "Charisma");

			if (savingThrow == null)
			{
				return NotFound("Saving throw 'Charisma' not found.");
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
