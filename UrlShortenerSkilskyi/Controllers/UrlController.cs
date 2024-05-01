using Microsoft.AspNetCore.Mvc;
using UrlShortenerSkilskyi.Data;
using UrlShortenerSkilskyi.Models;
using UrlShortenerSkilskyi.ViewModel;

namespace UrlShortenerSkilskyi.Controllers
{
    public class UrlController : Controller
    {
        private UrlShortenerDbContext _context;
        public UrlController(UrlShortenerDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UrlRequest request)
        {
            var url = new Url
            {
                OriginalLink = request.OriginalUrl,
                ShortLink = GenerateShortUrl(7),
                DateCreated = DateTime.Now
            };
            _context.Add(url);
            _context.SaveChanges();
            return View("Add", url.ShortLink);
        }


        [HttpGet("{shortLink}")]
        public IActionResult RedirectToOriginal(string shortLink)
        {
            var url = _context.Urls.FirstOrDefault(u => u.ShortLink == shortLink);

            if (url != null)
            {
                return Redirect(url.OriginalLink);
            }
            else
            {
                return NotFound();
            }
        }

        private string GenerateShortUrl(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(
                Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
                );

        }
    }
}
