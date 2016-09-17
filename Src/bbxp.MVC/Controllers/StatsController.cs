using System.Threading.Tasks;

using bbxp.CommonLibrary.Handlers;
using bbxp.CommonLibrary.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class StatsController : BaseController {
        public StatsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public async Task<ActionResult> Index() => View(await new PageStatsHandler(MANAGER_CONTAINER.GSetings).GetPageStats());
    }
}