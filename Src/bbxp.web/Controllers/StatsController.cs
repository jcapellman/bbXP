using bbxp.lib.Settings;

using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class StatsController : BaseController {
        public StatsController(IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache) { }

        public ActionResult Index()
        {
            var result = new PageStatsManager(ManagerContainer).GetStatsOverview();

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue);
        }
    }
}