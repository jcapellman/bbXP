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
        [Route("{year}/{month}/{day}/{postURL}")]
        public ReturnSet<PostResponseItem> GET(int year, int month, int day, string postURL) => new PostManager(MANAGER_CONTAINER).GetSinglePost($"{year}/{month}/{day}/{postURL}");

        public PostsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
    }
}