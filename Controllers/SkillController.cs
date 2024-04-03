using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Authorization;

namespace CharacterSheetDnD.Controllers
{
    public class SkillController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillController(ApplicationDbContext context)
        {
            _context = context;
        }

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		[Route("my-character/update-skill/{id}")]
		public async Task<IActionResult> UpdateSkill(int id, IFormCollection formData)
		{
			var character = await _context.Characters
										   .Include(c => c.CharacterSkills)
											   .ThenInclude(cs => cs.Skill)
										   .SingleOrDefaultAsync(c => c.CharacterID == id);

			if (character == null)
			{
				return NotFound($"Character with ID {id} not found.");
			}

			string skillName = formData["SkillName"].FirstOrDefault() ?? string.Empty;

			var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Name == skillName);

			if (skill == null)
			{
				return NotFound($"Skill '{skillName}' not found.");
			}

			bool isProficient = formData["IsProficient"].Contains("True");

			var characterSkill = character.CharacterSkills.FirstOrDefault(cs => cs.SkillID == skill.SkillID);
			if (characterSkill == null)
			{
				characterSkill = new CharacterSkill
				{
					CharacterID = id,
					SkillID = skill.SkillID,
					IsProficient = isProficient
				};
				_context.CharacterSkills.Add(characterSkill);
			}
			else
			{
				characterSkill.IsProficient = isProficient;
			}

			await _context.SaveChangesAsync();

			return RedirectToAction("DisplayCharacter", "MyCharacter", new { id });
		}


	}
}
