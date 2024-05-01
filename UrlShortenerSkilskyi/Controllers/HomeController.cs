using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UrlShortenerSkilskyi.Data;
using UrlShortenerSkilskyi.Models;

namespace UrlShortenerSkilskyi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UrlShortenerDbContext _context;

        public HomeController(ILogger<HomeController> logger, UrlShortenerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
