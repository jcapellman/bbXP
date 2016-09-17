using System;

namespace bbxp.WebAPI.DataLayer.Entities.Objects.Table {
    public class DGT_MostFrequentedPagesHeader {
        public int ID { get; set; }

        public DateTime CurrentAsOf { get; set; }

        public int RequestCount { get; set; }
    }
}