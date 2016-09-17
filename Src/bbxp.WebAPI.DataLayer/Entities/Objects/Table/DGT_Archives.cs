using System;

namespace bbxp.WebAPI.DataLayer.Entities.Objects.Table {
    public class DGT_Archives {
        public int ID { get; set; }

        public string DateString { get; set; }

        public string RelativeURL { get; set; }

        public int Count { get; set; }
        
        public DateTime PostDate { get; set; }
    }
}