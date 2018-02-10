using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class StatsController : BaseController {
        public StatsController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        public ActionResult Index()
        {
            var result = new PageStatsManager(ManagerContainer).GetStatsOverview();

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue);
        }
    }
}