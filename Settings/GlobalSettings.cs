using Microsoft.Extensions.Configuration;

namespace bbxp.MVC.Settings {
    public class GlobalSettings {
        public string SiteName { get; set; }

        public int NumPostsToList { get; set; }

        public string DatabaseConnection { get; set; }
    }
}