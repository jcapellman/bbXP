using System;

namespace bbxp.WebAPI.DataLayer.Entities.Objects.SP {
    public class GetPostListingSP {
        public int ID { get; set; }

        public DateTime Created { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Username { get; set; }

        public string URLSafename { get; set; }

        public string TagList { get; set; }

        public string SafeTagList { get; set; }

        public int NumComments { get; set; }
    }
}