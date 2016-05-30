using System;
using System.Collections.Generic;

namespace bbxp.MVC.Models {
    public class Tag {
        public string DisplayString { get; set; }

        public string URLString { get; set; }
    }

    public class PostViewModel {
        public string Title { get; set; }

        public string Body { get; set; }
        
        public List<Tag> Tags { get; set; }

        public DateTime PostDate { get; set; } 
    }
}