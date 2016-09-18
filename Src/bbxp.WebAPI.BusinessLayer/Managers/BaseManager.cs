using bbxp.PCL.Containers;
using bbxp.WebAPI.DataLayer;

namespace bbxp.WebAPI.BusinessLayer.Managers {
    public class BaseManager {
        protected ManagerContainer mContainer;

        protected RedisFactory rFactory;

        public BaseManager(ManagerContainer container) {
            mContainer = container;

            rFactory = new RedisFactory(container.GSetings.RedisDatabaseConnection);
        }
    }
}