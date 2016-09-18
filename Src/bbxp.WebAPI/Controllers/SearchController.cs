using System.Collections.Generic;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Posts;

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