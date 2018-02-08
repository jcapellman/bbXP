using System.Threading.Tasks;

using bbxp.lib.Settings;
using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class StatsController : BaseController {
        public StatsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public async Task<ActionResult> Index() => View(await new PageStatsManager(MANAGER_CONTAINER).GetStatsOverviewAsync());
    }
}