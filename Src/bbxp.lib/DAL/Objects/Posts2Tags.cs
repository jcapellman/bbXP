using System;

namespace bbxp.lib.DAL.Objects
{
    public class Posts2Tags
    {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public int PostID { get; set; }

        public int TagID { get; set; }
    }
}