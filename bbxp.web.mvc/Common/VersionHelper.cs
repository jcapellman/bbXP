using System.Reflection;

namespace bbxp.web.mvc.Common
{
    public static class VersionHelper
    {
        public static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown Version";
        }
    }
}