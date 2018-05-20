using System;

namespace bbxp.lib.DAL.Objects
{
    public class Tags
    {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public string Description { get; set; }

        public string URLSafeDescription { get; set; }
    }
}