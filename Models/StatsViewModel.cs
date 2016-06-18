using System;
using System.Collections.Generic;

using bbxp.MVC.Entities.Objects.Table;

namespace bbxp.MVC.Models {
    public class StatsViewModel {
        public List<DGT_MostFrequentedPages> TopRequests { get; set; }

        public int NumberRequests { get; set; }

        public DateTime CurrentAsOf { get; internal set; }
    }
}