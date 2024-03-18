using CharacterSheetDnD.Data;
using Microsoft.AspNetCore.Mvc;

namespace CharacterSheetDnD.Controllers
{
    public class DeleteCharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeleteCharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
