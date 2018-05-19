using bbxp.lib.DAL;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        public ActionResult Index() => View();

        public ActionResult EditPost(int id) => View();

        public ActionResult NewPost() => View();

        public AdminController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }
    }
}