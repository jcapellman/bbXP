using System;

namespace bbxp.web.DAL.Objects
{
    public class Requests
    {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public string RequestStr { get; set; }
    }
}