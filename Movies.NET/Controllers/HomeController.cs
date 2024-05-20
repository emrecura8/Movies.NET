using Microsoft.AspNetCore.Mvc;
using Movies.NET.Controllers.Bases;
using Movies.NET.Models;
using System.Diagnostics;

namespace Movies.NET.Controllers
{
    public class HomeController : MVCControllerBase
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
    }
}
