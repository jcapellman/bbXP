using bbxp.MVC.Managers;
using bbxp.MVC.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class ContentController : BaseController {
        public ContentController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
        
        [Route("/content/{urlSafeName}")]
        public IActionResult Index(string urlSafeName) => View(new ContentManager(MANAGER_CONTAINER).GetContent(urlSafeName));
    }
}