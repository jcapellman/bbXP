﻿using System;

namespace bbxp.WebAPI.DataLayer.Entities.Objects.Table {
    public class DGT_Posts {
        public int ID { get; set; }

        public DateTime PostDate { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string RelativeURL { get; set; }

        public string TagList { get; set; }

        public string SafeTagList { get; set; }
    }
}