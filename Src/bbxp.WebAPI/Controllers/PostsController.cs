using System.Collections.Generic;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Posts;

using bbxp.WebAPI.BusinessLayer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.WebAPI.Controllers {
    public class PostsController : BaseController {
        [HttpGet]
        public ReturnSet<List<PostResponseItem>> GET() => new PostManager(MANAGER_CONTAINER).GetHomeListing();

        [HttpGet]
        [Route("{urlArg}")]
        public ReturnSet<PostResponseItem> GET(string urlArg) => new PostManager(MANAGER_CONTAINER).GetSinglePost(urlArg.Replace("_", "/"));

        public PostsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
    }
}