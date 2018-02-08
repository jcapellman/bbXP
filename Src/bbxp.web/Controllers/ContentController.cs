using System.Threading.Tasks;

using bbxp.lib.Settings;
using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class ContentController : BaseController {
        public ContentController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        [Route("/content/{urlSafeName}")]
        public async Task<IActionResult> Index(string urlSafeName)
            => View(await new ContentManager(MANAGER_CONTAINER).GetContentAsync(urlSafeName));
    }
}