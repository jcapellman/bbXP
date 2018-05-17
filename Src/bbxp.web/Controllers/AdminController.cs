using bbxp.lib.DAL;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Login() => View();

        public ActionResult Index() => View();

        public ActionResult EditPost(int id) => View();

        public ActionResult NewPost() => View();

        public AdminController(GlobalSettings globalSettings, IMemoryCache cache, BbxpDbContext dbContext) : base(globalSettings, cache, dbContext)
        {
        }
    }
}