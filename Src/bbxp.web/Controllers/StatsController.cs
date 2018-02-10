using bbxp.lib.Settings;
using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class StatsController : BaseController {
        public StatsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public ActionResult Index()
        {
            var result = new PageStatsManager(MANAGER_CONTAINER).GetStatsOverview();

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue);
        }
    }
}