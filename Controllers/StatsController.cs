using bbxp.MVC.Managers;
using bbxp.MVC.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class StatsController : BaseController {
        public StatsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public ActionResult Index() => View(new StatsManager(MANAGER_CONTAINER).GetStatsOverview());
    }
}