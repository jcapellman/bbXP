using System.Threading.Tasks;

using bbxp.web.Handlers;
using bbxp.web.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class ContentController : BaseController {
        public ContentController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        [Route("/content/{urlSafeName}")]
        public async Task<IActionResult> Index(string urlSafeName)
            => View(await new ContentHandler(MANAGER_CONTAINER.GSetings).GetContent(urlSafeName));
    }
}