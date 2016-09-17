using System;

namespace bbxp.WebAPI.DataLayer.Entities.Objects.Table {
    public class Requests {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public string RequestStr { get; set; }
    }
}