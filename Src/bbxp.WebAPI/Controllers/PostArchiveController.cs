using System.Collections.Generic;
using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.PostArchive;
using bbxp.PCL.Transports.Posts;
using bbxp.WebAPI.BusinessLayer.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.WebAPI.Controllers {    
    public class PostArchiveController : BaseController {
        [HttpGet]
        public ReturnSet<List<PostArchiveListingResponseItem>> GET()
            => new PostArchiveManager(MANAGER_CONTAINER).GetArchives();

        [Route("{year}/{month}")]
        [HttpGet]
        public ReturnSet<List<PostResponseItem>> GET(int year, int month) => new PostManager(MANAGER_CONTAINER).GetMonthPosts(year, month);

        public PostArchiveController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
    }
}