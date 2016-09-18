using bbxp.PCL.Containers;
using bbxp.PCL.Settings;
using Microsoft.AspNetCore.Mvc;

namespace bbxp.WebAPI.Controllers {
    [Route("api/[controller]")]
    public class BaseController : Controller {
        protected GlobalSettings _globalSettings;

        public ManagerContainer MANAGER_CONTAINER => new ManagerContainer { GSetings = _globalSettings };

        public BaseController(GlobalSettings globalSettings) {
            _globalSettings = globalSettings;
        }
    }
}