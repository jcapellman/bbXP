using bbxp.lib.Common;
using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;
using bbxp.lib.Transports.Posts;

using bbxp.web.blazor.Server.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : BaseController
    {
        public HomeController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        [HttpGet]
        public ReturnSet<List<PostResponseItem>> GetPosts() => new PostManager(MContainer).GetHomeListing();
    }
}