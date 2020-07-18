using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers
{
    public class ContentController : BaseController
    {
        public ContentController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        [Route("/content/{urlSafeName}")]
        public IActionResult Index(string urlSafeName)
        {
            var result = new ContentManager(ManagerContainer).GetContent(urlSafeName);

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue);
        }
    }
}