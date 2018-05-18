using System.Collections.Generic;
using System.Security.Claims;

using bbxp.lib.DAL;
using bbxp.lib.Settings;
using bbxp.web.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace bbxp.web.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Login() => View(new LoginViewModel());

        private void LoginUser(int userID)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userID.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
        }

        public IActionResult AttemptLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View("Login", new LoginViewModel
            {
                ErrorString = "Could not login"
            });
        }

        public ActionResult Index() => View();

        public ActionResult EditPost(int id) => View();

        public ActionResult NewPost() => View();

        public AdminController(GlobalSettings globalSettings, IMemoryCache cache, BbxpDbContext dbContext) : base(globalSettings, cache, dbContext)
        {
        }
    }
}