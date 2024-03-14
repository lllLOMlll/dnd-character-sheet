using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
using CharacterSheetDnD.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CharacterSheetDnD.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/character-selection")]
        [Authorize]
        public async Task<IActionResult> CharacterSelection()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var characters = await _context.Characters
                                .Where(c => c.UserId == userId)
                                .Include(c => c.CharacterClasses)
                                .ToListAsync();

            return View(characters); 
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
