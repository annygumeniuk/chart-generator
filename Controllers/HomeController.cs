using ChartGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChartGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly Random _random = new Random();
        private static readonly List<RandomNumber> _history = new List<RandomNumber>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult GetRandomNumber()
        {
            // Generate a new random number with timestamp
            var newNumber = new RandomNumber
            {
                Value = _random.Next(1, 11), // Random number from 1 to 10
                TimeGenerated = DateTime.Now
            };

            // Add to history (limit history to the last 10 entries)
            _history.Add(newNumber);
            if (_history.Count > 10)
            {
                _history.RemoveAt(0);
            }

            return Json(newNumber); // Return the latest random number
        }

        [HttpGet]
        public JsonResult GetHistory()
        {
            // Return the full history as JSON
            return Json(_history);
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
