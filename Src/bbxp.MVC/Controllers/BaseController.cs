using bbxp.MVC.Containers;
using bbxp.MVC.Settings;

using Microsoft.AspNetCore.Mvc;

namespace bbxp.MVC.Controllers {
    public class BaseController : Controller {
        protected GlobalSettings _globalSettings;

        public ManagerContainer MANAGER_CONTAINER => new ManagerContainer { GSetings = _globalSettings };

        public BaseController(GlobalSettings globalSettings) {
            _globalSettings = globalSettings;
        }
    }
}