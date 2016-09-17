using bbxp.MVC.Managers;
using bbxp.MVC.Models;
using bbxp.MVC.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    [Route("Admin")]
    public class AdminController : BaseController {
        public AdminController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
        
        public IActionResult Index() => View();

        public IActionResult Login() => View();

        public IActionResult AttemptLogin(LoginViewModel model) {
            var result = new AdminManager(MANAGER_CONTAINER).AttemptLogin(model.Username, model.Password);

            return (result ? View("Index") : View("Login"));
        }
    }
}