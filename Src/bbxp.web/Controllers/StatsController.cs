using System.Threading.Tasks;

using bbxp.lib.Settings;
using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class StatsController : BaseController {
        public StatsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public ActionResult Index() => View(new PageStatsManager(MANAGER_CONTAINER).GetStatsOverview());
    }
}