using System.Linq;

using bbxp.lib.Containers;
using bbxp.web.DAL;

namespace bbxp.web.Managers {
    public class AdminManager : BaseManager {
        public AdminManager(ManagerContainer container) : base(container) { }

        public bool AttemptLogin(string username, string password) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                return eFactory.Users.Any(a => a.Username == username && a.Password == password && a.Active);
            }
        }
    }
}