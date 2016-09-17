using bbxp.CommonLibrary.Containers;

namespace bbxp.WebAPI.BusinessLayer.Managers {
    public class BaseManager {
        protected ManagerContainer mContainer;

        public BaseManager(ManagerContainer container) {
            mContainer = container;
        }
    }
}