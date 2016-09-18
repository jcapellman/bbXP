﻿using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.Content;

using bbxp.WebAPI.BusinessLayer.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.WebAPI.Controllers {    
    public class ContentController : BaseController {
        [HttpGet]
        public ReturnSet<ContentResponseItem> GET(string urlArg) => new ContentManager(MANAGER_CONTAINER).GetContent(urlArg);

        public ContentController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
    }
}