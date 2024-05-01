using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortenerSkilskyi.Data;
using UrlShortenerSkilskyi.Models;
using UrlShortenerSkilskyi.ViewModel;

namespace UrlShortenerSkilskyi.Controllers
{
    public class ShortUrlTableController : Controller
    {
        private readonly UrlShortenerDbContext _context;

        public ShortUrlTableController(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var urls = await _context.Urls.ToListAsync();
            return View(urls);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Urls.FirstOrDefaultAsync(m => m.Id == id);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Urls.FindAsync(id);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var url = await _context.Urls.FindAsync(id);
            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
