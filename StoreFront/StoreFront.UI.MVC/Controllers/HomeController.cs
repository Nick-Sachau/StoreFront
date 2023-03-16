using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using StoreFront.UI.MVC.Models;
using System.Diagnostics;

namespace StoreFront.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreFrontContext _context;

        public HomeController(ILogger<HomeController> logger, StoreFrontContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var storeFrontContext = _context.Pokemons.Include(p => p.City).Include(p => p.PokeBall);
            return View(storeFrontContext.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var pokemon = _context.Pokemons;
            return View(pokemon);
        }
    }
}