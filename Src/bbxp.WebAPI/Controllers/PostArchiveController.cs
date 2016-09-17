using System.Collections.Generic;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.PostArchive;

using bbxp.WebAPI.BusinessLayer.Managers;

using Microsoft.AspNetCore.Mvc;

namespace bbxp.WebAPI.Controllers {    
    public class PostArchiveController : BaseController {
        [HttpGet]
        public ReturnSet<List<PostArchiveListingResponseItem>> GET()
            => new PostArchiveManager(MANAGER_CONTAINER).GetArchives();

        public PostArchiveController(GlobalSettings globalSettings) : base(globalSettings) { }
    }
}