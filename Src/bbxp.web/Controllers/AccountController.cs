using System.Collections.Generic;
using System.Security.Claims;

using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;
using bbxp.web.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        public ActionResult Login(LoginViewModel model = null) => View(model);

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

        [HttpPost]
        public IActionResult AttemptLogin(LoginViewModel model)
        {
            var result = new AdminManager(ManagerContainer).AttemptLogin(model.Username, model.Password);

            if (result.HasError)
            {
                return View("Login", new LoginViewModel
                {
                    ErrorString = "Could not login"
                });
            }

            LoginUser(result.ReturnValue);

            return RedirectToAction("Index", "Admin");
        }
    }
}