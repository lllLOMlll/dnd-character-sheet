using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CharacterSheetDnD.Controllers
{
	public class HitPointsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public HitPointsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-maximum-hit-points")]
		public async Task<IActionResult> UpdateMaximumHitPoints(int id, int maximumHitPoints)
		{
			var character = await _context.Characters
													.Include(c => c.CharacterHealth)
													.SingleOrDefaultAsync(c => c.CharacterID == id);

			
			if (character == null) 
			{
			 return NotFound($"Character with ID {id} not found.");
			}

			if (character.CharacterHealth == null)
			{
				character.CharacterHealth = new CharacterHealth { CharacterID = id };
			}

			character.CharacterHealth.MaximumHitPoints = maximumHitPoints;
			await _context.SaveChangesAsync();

			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id = character.CharacterID });
		}
	}
}
