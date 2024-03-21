using Microsoft.AspNetCore.Mvc;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CharacterSheetDnD.Controllers
{
    public class DeleteCharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeleteCharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // This method is for the deletion operation, which you already have
        
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            // Set a TempData Success message
            TempData["SuccessMessage"] = "Character successfully deleted!";

            return RedirectToAction("Index", "Home");
        }

        [Route("delete-character")]
        [Authorize]
        public async Task<IActionResult> DisplayDeleteCharacter()
        {
            var characters = await _context.Characters.ToListAsync();
            return View("DeleteCharacter", characters); 
        }
    }
}
