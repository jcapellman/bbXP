namespace bbxp.CommonLibrary.Settings {
    public class GlobalSettings {
        public string SiteName { get; set; }

        public int NumPostsToList { get; set; }

        public string DatabaseConnection { get; set; }

        public string WebAPIAddress { get; set; }

        public string CachingWebAPIAddress { get; set; }

        public string RedisDatabaseConnection { get; set; }
    }
}