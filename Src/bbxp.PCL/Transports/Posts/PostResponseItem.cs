using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace bbxp.PCL.Transports.Posts {
    [DataContract]
    public class PostResponseItem {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public List<TagResponseItem> Tags { get; set; }

        [DataMember]
        public DateTime PostDate { get; set; } 
    }
}