using System;
using System.Collections.Generic;

namespace bbxp.PCL.Transports.PageStats {
    public class PageStatsResponseItem {
        public List<PageRequestStatsResponseItem> TopRequests { get; set; }

        public int NumberRequests { get; set; }

        public DateTime CurrentAsOf { get; internal set; }
    }
}