using System;

namespace bbXP.dataLayer.Models {
    public class Post : IEditModel {		
		public int ID { get; set; }

		public DateTime Created { get; set; }

	    public bool Active { get; set; }

	    public DateTime Modified { get; set; }

		public string Title { get; set; }

		public string URLSafeTitle { get; set; }

		public string Body { get; set; }

		public int PostedByUserID { get; set; }		
    }
}