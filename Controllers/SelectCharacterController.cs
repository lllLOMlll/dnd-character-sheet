﻿using Microsoft.AspNetCore.Mvc;
using CharacterSheetDnD.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CharacterSheetDnD.Controllers
{
    public class SelectCharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelectCharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("select-character")]
        [Authorize]
        public async Task<IActionResult> DisplaySelectCharacter()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var characters = await _context.Characters
                .Where(c => c.UserId == userId)
                .ToListAsync();
            return View("SelectCharacter", characters);
        }

       



    }
}
