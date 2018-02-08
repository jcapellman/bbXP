using bbxp.lib.Containers;
using bbxp.web.DAL;

namespace bbxp.web.Managers {
    public class BaseManager {
        protected ManagerContainer mContainer;

        protected RedisFactory rFactory;

        public BaseManager(ManagerContainer container) {
            mContainer = container;

            rFactory = new RedisFactory(container.GSetings.RedisDatabaseConnection);
        }
    }
}