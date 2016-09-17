using System.Collections.Generic;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Posts;

using bbxp.WebAPI.BusinessLayer.Managers;

using Microsoft.AspNetCore.Mvc;

namespace bbxp.WebAPI.Controllers {    
    public class PostTagController : BaseController {
        [HttpGet]
        public ReturnSet<List<PostResponseItem>> GET(string tag)
            => new PostManager(MANAGER_CONTAINER).GetPostsFromTag(tag);

        public PostTagController(GlobalSettings globalSettings) : base(globalSettings) { }
    }
}