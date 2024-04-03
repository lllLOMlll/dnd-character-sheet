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
        [Route("my-character/update-acrobatics-proficiency/{id}")]
        public async Task<IActionResult> UpdateAcrobaticsProficiency(int id, IFormCollection formData)
        {
            var character = await _context.Characters
                                           .Include(c => c.CharacterSkills)
                                               .ThenInclude(cs => cs.Skill)
                                           .SingleOrDefaultAsync(c => c.CharacterID == id);

            if (character == null)
            {
                return NotFound($"Character with ID {id} not found.");
            }

            // Find the 'Acrobatics' skill entry
            var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Name == "Acrobatics");

            if (skill == null)
            {
                return NotFound("Skill 'Acrobatics' not found.");
            }

            // Parse the 'IsProficient' form value
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
