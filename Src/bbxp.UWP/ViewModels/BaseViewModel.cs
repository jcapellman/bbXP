using System.ComponentModel;
using System.Runtime.CompilerServices;

using bbxp.PCL.Settings;

namespace bbxp.UWP.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        protected GlobalSettings gSettings = new GlobalSettings {
            WebAPIAddress = "http://localhost:1337/api/",
            CachingWebAPIAddress = "http://localhost:1338/node/"
        };

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}