using bbxp.lib.Containers;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.Controllers {
    public class BaseController : Controller {
        private readonly GlobalSettings _globalSettings;
        private readonly IMemoryCache _memoryCache;

        protected ManagerContainer ManagerContainer => new ManagerContainer { GSetings = _globalSettings, Cache = _memoryCache };

        public BaseController(GlobalSettings globalSettings, IMemoryCache cache) {
            _globalSettings = globalSettings;
            _memoryCache = cache;
        }

        public ActionResult RedirectToError(string exceptionMessage) => RedirectToAction("Error", exceptionMessage);
    }
}