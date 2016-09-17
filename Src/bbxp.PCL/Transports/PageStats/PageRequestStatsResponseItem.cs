using System.Runtime.Serialization;

namespace bbxp.PCL.Transports.PageStats {
    [DataContract]
    public class PageRequestStatsResponseItem {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public string Request { get; set; }
    }
}