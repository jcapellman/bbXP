using bbxp.PCL.Common;

using System.Threading.Tasks;

namespace bbxp.PCL.PlatformAbstractions {
    public interface ILocalStorage {
        Task<ReturnSet<bool>> WriteFile<T>(string fileName, T objectValue);

        Task<T> ReadFile<T>(string fileName);
    }
}
