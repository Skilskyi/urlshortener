using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortenerSkilskyi.ViewModel;
using UrlShortenerSkilskyi.Data;
using Microsoft.EntityFrameworkCore;
using UrlShortenerSkilskyi.Models.ViewModel;
using UrlShortenerSkilskyi.Models;

namespace UrlShortenerSkilskyi.Controllers
{
    public class AccountController : Controller
    {
        private UrlShortenerDbContext _context;
        public AccountController(UrlShortenerDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (!_context.Users.Any(u => u.Email == model.Email))
                {

                    var user = new User
                    {
                        Email = model.Email,
                        Password = model.Password,
                        RoleId = 2
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();


                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "User with this email already exists");
                }
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email));
            }

            if (user.Role != null && !string.IsNullOrEmpty(user.Role.Name))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name));
            }

            if (claims.Any())
            {
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login and (or) password");
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
