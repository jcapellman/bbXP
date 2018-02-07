using System.Linq;
using bbxp.PCL.Containers;
using bbxp.WebAPI.DataLayer.Entities;

namespace bbxp.web.Managers {
    public class AdminManager : BaseManager {
        public AdminManager(ManagerContainer container) : base(container) { }

        public bool AttemptLogin(string username, string password) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var user = eFactory.Users.FirstOrDefault(a => a.Username == username && a.Password == password && a.Active);

                if (user == null) {
                    return false;
                }

                return true;
            }
        }
    }
}