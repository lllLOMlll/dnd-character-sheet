using CharacterSheetDnD.Models;
using CharacterSheetDnD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CharacterSheetDnD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
        public IActionResult CharacterSelection()
        {
            return View();
        }

        //[Route("Home/character-creation")]
        //public IActionResult CharacterCreation()
        //{
        //    var viewModel = new CharacterCreationViewModel
        //    {
        //        // Initialiaze all the Models I need
        //        Character = new Character(),
        //        CharacterClass = new CharacterClass(),
        //        CharacterStatistic = new CharacterStatistic(),
        //        CharacterHealth = new CharacterHealth()
        //    };


        //    return View(viewModel);
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
