using System;
using System.Threading.Tasks;

using Windows.Storage;

using bbxp.PCL.Common;
using bbxp.PCL.PlatformAbstractions;

using Microsoft.Toolkit.Uwp;

namespace bbxp.UWP.PlatformImplementations {
    public class LocalStorage : BaseLocalStorage {
        readonly Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        
        public override async Task<dynamic> ReadFile<T>(string fileName) {
            try
            {
                var file = await storageFolder.GetFileAsync(fileName);

                if (!file.IsAvailable)
                {
                    return default(T);
                }

                var result = await storageFolder.ReadTextFromFileAsync(fileName);

                if (typeof (T) == typeof (string))
                {
                    return result;
                }

                return result.Length == 0 ? default(T) : ConvertJSONtoT<T>(result);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public override async Task<ReturnSet<bool>> WriteFile<T>(string fileName, T objectValue) {
            var jsonStr = (typeof (T) == typeof (string) ? objectValue.ToString() : ConvertToJSON(objectValue));

            var result = await storageFolder.WriteTextToFileAsync(jsonStr, fileName, CreationCollisionOption.ReplaceExisting);

            return new ReturnSet<bool>(true);
        }
    }
}