using System;
using System.Collections.Generic;

using Windows.UI.Xaml.Controls;

namespace bbxp.UWP.Views {
    public class MenuItem {
        public Symbol Icon { get; set; }

        public string Name { get; set; }

        public Type PageType { get; set; }

        public string Parameter { get; set; }

        public static List<MenuItem> GetMainItems() {
            var items = new List<MenuItem> {
                    new MenuItem() {Icon = Symbol.Home, Name = "Home", PageType = typeof (Views.MainListingPage)},
                    new MenuItem {Icon = Symbol.Folder, Name = "Archives", PageType = typeof(Views.MainPage)},
                    new MenuItem() {Icon = Symbol.Find, Name = "Search", PageType = typeof(Views.SearchPage)},
                    new MenuItem {Icon = Symbol.Globe, Name = "GitHub", PageType = typeof(Views.ExternalPage), Parameter = "http://www.github.com/jcapellman" },
                    new MenuItem {Icon = Symbol.Globe, Name = "Twitter", PageType = typeof(Views.ExternalPage), Parameter = "http://twitter.com/jcapellman" },
                    new MenuItem {Icon = Symbol.Globe, Name = "LinkedIn", PageType = typeof(Views.ExternalPage), Parameter = "http://linkedin.com/in/jarredcapellman"},
                    new MenuItem() {Icon = Symbol.People, Name = "About Me", PageType = typeof (Views.ContentPage), Parameter = "About-Me"},
                };

            return items;
        }
    }

    public sealed partial class MainPage : Page {        
        public MainPage() {
            this.InitializeComponent();

            hmMain.ItemsSource = MenuItem.GetMainItems();

            fMain.Navigate(typeof(MainListingPage));
        }

        private void hmMain_ItemClick(object sender, ItemClickEventArgs e) {
            var clickedItem = (e.ClickedItem as MenuItem);

            if (clickedItem == null) {
                return;
            }

            fMain.Navigate(clickedItem.PageType, clickedItem.Parameter);
        }
    }
}