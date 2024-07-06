using System.Security.Cryptography;
using System.Text;

namespace bbxp.lib.Common
{
    public static class Extensions
    {
        public static string ToSHA256(this string str) => BitConverter.ToString(SHA256.HashData(Encoding.ASCII.GetBytes(str))).Replace("-","").ToUpper();


        public static string ToUrlSafeTitle(this string title) =>
            title.ToLower().Replace(' ', '_').Replace(".", "").Replace(",", "").Replace("-", "_");
    }
}