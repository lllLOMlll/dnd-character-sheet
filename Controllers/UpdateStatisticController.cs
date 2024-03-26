using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;
using Humanizer;

namespace CharacterSheetDnD.Controllers
{
	public class UpdateStatisticController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UpdateStatisticController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-strength/{id}")]
		public async Task<IActionResult> UpdateStrength(int id, int strength)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterStatistic)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			if (character.CharacterStatistic == null)
			{
				character.CharacterStatistic = new CharacterStatistic { CharacterID = id };
			}

			character.CharacterStatistic.Strength = strength;
			await _context.SaveChangesAsync();

			// MyCharacter action in MyCharacter Controller
			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });


		}


		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-dexterity/{id}")]
		public async Task<IActionResult> UpdateDexterity(int id, int dexterity)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterStatistic)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			if (character.CharacterStatistic == null)
			{
				// Initialize CharacterStatistic if it doesn't exist
				character.CharacterStatistic = new CharacterStatistic { CharacterID = id };
			}

			character.CharacterStatistic.Dexterity = dexterity;
			await _context.SaveChangesAsync();

			// Redirect to the character view or another appropriate page
			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });

		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-constitution/{id}")]
		public async Task<IActionResult> UpdateConstitution(int id, int constitution)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterStatistic)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			if (character.CharacterStatistic == null)
			{
				character.CharacterStatistic = new CharacterStatistic { CharacterID = id };
			}

			character.CharacterStatistic.Constitution = constitution;
			await _context.SaveChangesAsync();

			// Redirect to the character view or another appropriate page
			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });

		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-intelligence/{id}")]
		public async Task<IActionResult> UpdateIntelligence(int id, int intelligence)
		{

			var character = await _context.Characters
											.Include(c => c.CharacterStatistic)
											.SingleOrDefaultAsync(c => c.CharacterID == id);
			
			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			if (character.CharacterStatistic ==  null) 
			{
				character.CharacterStatistic = new CharacterStatistic { CharacterID = id };
			}

			character.CharacterStatistic.Intelligence = intelligence; 
			await _context.SaveChangesAsync();

			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });
		}


		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-wisdom/{id}")]

		public async Task<IActionResult> UpdateWisdom(int id, int wisdom)
		{
			var character = await _context.Characters
											.Include(c => c.CharacterStatistic)
											.SingleOrDefaultAsync(c => c.CharacterID == id);
			if (character == null) 
			{
				return NotFound($"Character with ID {id} not found");
			}

			if (character.CharacterStatistic == null)
			{
				character.CharacterStatistic = new CharacterStatistic { CharacterID = id };
			}

			character.CharacterStatistic.Wisdom = wisdom;
			await _context.SaveChangesAsync();

			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });
		}


		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-charisma/{id}")]

		public async Task<IActionResult> UpdateCharisma(int id, int charisma)
		{
			var character = await _context.Characters
										.Include(c => c.CharacterStatistic)
										.SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found");
			}

			if(character.CharacterStatistic == null) 
			{
				character.CharacterStatistic = new CharacterStatistic { CharacterID = id };
			}

			character.CharacterStatistic.Charisma = charisma;
			await _context.SaveChangesAsync();

			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });
		}
	}
}
