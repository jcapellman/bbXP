﻿using System;

namespace bbxp.WebAPI.DataLayer.Entities.Objects.Table {
    public class Posts {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int PostedByUserID { get; set; }

        public string URLSafename { get; set; }
    }
}