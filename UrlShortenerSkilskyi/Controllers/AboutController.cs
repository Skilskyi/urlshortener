using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrlShortenerSkilskyi.Data;
using UrlShortenerSkilskyi.Models;

public class AboutController : Controller
{
    private readonly UrlShortenerDbContext _context;

    public AboutController(UrlShortenerDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        var about = _context.About.FirstOrDefault();
        if (about == null)
        {
            about = new About(); 
        }
        return View(about);
    }


    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Edit(string description)
    {
        var about = _context.About.FirstOrDefault();
        if (about != null)
        {
            about.Description = description;
            _context.SaveChanges();
        }
        else
        {
            about = new About { Description = description };
            _context.About.Add(about);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}
