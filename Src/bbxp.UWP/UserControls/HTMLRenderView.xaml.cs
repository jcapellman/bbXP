using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace bbxp.UWP.UserControls {
    public sealed partial class HTMLRenderView : UserControl {
        public string ContentBody {
            get { return (string) GetValue(ContentBodyProperty); }
            set { SetValue(ContentBodyProperty, value); }
        }

        public static readonly DependencyProperty ContentBodyProperty = DependencyProperty.Register("ContentBody", typeof(string), typeof(HTMLRenderView), new PropertyMetadata("", PropertyChangedCallback));

        public event EventHandler<EventArgs> ContentChanged;

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            var hRenderView = (HTMLRenderView)dependencyObject;

            hRenderView.ContentChanged?.Invoke(hRenderView, EventArgs.Empty);
        }

        public HTMLRenderView() {
            this.InitializeComponent();
            
            ContentChanged += OnContentChanged;
        }

        private void OnContentChanged(object sender, EventArgs eventArgs) {
            if (string.IsNullOrEmpty(ContentBody)) {
                return;
            }
            
            wvMain.NavigateToString(ContentBody);
        }
    }
}