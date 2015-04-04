using System;

namespace bbXP.dataLayer.Models {
    public interface IEditModel {
		DateTime Modified { get; set; }

		DateTime Created { get; set; }

		bool Active { get; set; }
    }
}