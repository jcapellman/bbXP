using System.Collections.Generic;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Posts;

using bbxp.WebAPI.BusinessLayer.Managers;

namespace bbxp.WebAPI.Controllers {
    public class PostsController : BaseController {
        public ReturnSet<List<PostResponseItem>> GET() => new PostManager(MANAGER_CONTAINER).GetHomeListing();

        public ReturnSet<PostResponseItem> GET(string urlArg) => new PostManager(MANAGER_CONTAINER).GetSinglePost(urlArg);

        public PostsController(GlobalSettings globalSettings) : base(globalSettings) { }
    }
}