﻿using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.PageStats;
using bbxp.WebAPI.BusinessLayer.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.WebAPI.Controllers {
    public class PageStatsController : BaseController {
        [HttpGet]
        public ReturnSet<PageStatsResponseItem> GET()
            => new PageStatsManager(MANAGER_CONTAINER).GetStatsOverview();

        public PageStatsController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }
    }
}