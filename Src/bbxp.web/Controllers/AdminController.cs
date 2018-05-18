using bbxp.lib.DAL;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        public ActionResult Index() => View();

        public ActionResult EditPost(int id) => View();

        public ActionResult NewPost() => View();

        public AdminController(GlobalSettings globalSettings, IMemoryCache cache, BbxpDbContext dbContext) : base(globalSettings, cache, dbContext)
        {
        }
    }
}