namespace bbxp.desktop.windows.Objects.JSON
{
    public class Settings
    {
        public string RESTServiceURL { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Settings()
        {
            RESTServiceURL = string.Empty;

            Username = string.Empty;

            Password = string.Empty;
        }
    }
}