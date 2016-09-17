using System.Runtime.Serialization;

namespace bbxp.PCL.Transports.Posts {
    [DataContract]
    public class TagResponseItem {
        [DataMember]
        public string DisplayString { get; set; }

        [DataMember]
        public string URLString { get; set; }    
    }
}