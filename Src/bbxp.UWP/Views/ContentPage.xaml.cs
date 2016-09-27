﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using bbxp.UWP.ViewModels;

namespace bbxp.UWP.Views {
    public sealed partial class ContentPage : Page {
        private ContentViewModel viewModel => (ContentViewModel)DataContext;

        public ContentPage() {
            this.InitializeComponent();

            DataContext = new ContentViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            var urlArg = (MenuItem)e.Parameter;
            
            var result = await viewModel.LoadData(urlArg.Parameter);
        }
    }
}