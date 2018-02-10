using System.Linq;

using bbxp.lib.Containers;

namespace bbxp.lib.Managers {
    public class AdminManager : BaseManager {
        public AdminManager(ManagerContainer container) : base(container) { }

        public bool AttemptLogin(string username, string password) => DbContext.Users.Any(a => a.Username == username && a.Password == password && a.Active);        
    }
}