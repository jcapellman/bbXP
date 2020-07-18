using bbxp.lib.Common;
using bbxp.lib.Containers;
using System;
using System.Linq;

namespace bbxp.lib.Managers
{
    public class AdminManager : BaseManager
    {
        public AdminManager(ManagerContainer container) : base(container) { }

        public ReturnSet<int> AttemptLogin(string username, string password)
        {
            try
            {
                var hashedPassword = HashString(password);

                var result = DbContext.Users.FirstOrDefault(a => a.Username == username && a.Password == hashedPassword && a.Active);

                if (result == null)
                {
                    throw new Exception($"No user matching {username} and {password}");
                }

                return new ReturnSet<int>(result.ID);
            }
            catch (Exception ex)
            {
                return new ReturnSet<int>(ex);
            }
        }
    }
}