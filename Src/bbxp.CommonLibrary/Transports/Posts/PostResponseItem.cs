using System;
using System.Collections.Generic;

namespace bbxp.CommonLibrary.Transports.Posts {    
    public class PostResponseItem {        
        public string Title { get; set; }
        
        public string Body { get; set; }

        public List<TagResponseItem> Tags { get; set; }

        public DateTime PostDate { get; set; } 
    }
}