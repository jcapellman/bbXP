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

namespace bbxp.web.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index(LoginViewModel model = null) => View(model);

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
            var result = new AdminManager(ManagerContainer).AttemptLogin(model.Username, model.Password);

            if (!result.HasError)
            {
                LoginUser(result.ReturnValue);
            }

            return View("Index", new LoginViewModel
            {
                ErrorString = "Could not login"
            });
        }

        public AccountController(GlobalSettings globalSettings, IMemoryCache cache, BbxpDbContext dbContext) : base(globalSettings, cache, dbContext)
        {
        }
    }
}