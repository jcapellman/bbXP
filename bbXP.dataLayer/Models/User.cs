using System;

namespace bbXP.dataLayer.Models {
    public class User : IEditModel {
	    public DateTime Modified { get; set; }

	    public DateTime Created { get; set; }

	    public bool Active { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }

		public Guid Password { get; set; }

		public bool IsConfirmed { get; set; }
    }
}