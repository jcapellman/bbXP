using System.Collections.Generic;
using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.Posts;
using bbxp.WebAPI.BusinessLayer.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.WebAPI.Controllers {    
    public class SearchController : BaseController {
        [HttpGet]
        public ReturnSet<List<PostResponseItem>> GET(string query)
            => new PostManager(MANAGER_CONTAINER).SearchPosts(query);

        public SearchController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
    }
}