using System;

using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace bbxp.UWP.Views {
    public class BasePage : Page {
        public async void ShowMessage(string content) {
            var md = new MessageDialog(content, "bbxp");

            await md.ShowAsync();
        }
    }
}