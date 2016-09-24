using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using bbxp.PCL.Handlers;

namespace bbxp.UWP.UserControls {
    public sealed partial class HTMLRenderView : UserControl {
        public string URL {
            get { return (string)GetValue(URLProperty); }
            set { SetValue(URLProperty, value); }
        }

        public static readonly DependencyProperty URLProperty = DependencyProperty.Register("URL", typeof(string), typeof(HTMLRenderView), new PropertyMetadata("", PropertyChangedCallback));
        
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

        private async void OnContentChanged(object sender, EventArgs eventArgs) {
            if (string.IsNullOrEmpty(URL) || string.IsNullOrEmpty(ContentBody)) {
                return;
            }

            var cssContentHandler = new CSSContentHandler();

            var cssStr = await cssContentHandler.GetCSSContent(URL);

            var finalContent = $"<head><meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\" /><style type='text/css'>{cssStr}</style></head><body class=\"bodyMobile\"><div id=\"PostContainer\"><div id=\"ContentContainer\">{ContentBody}</div></div></body>";
            
            wvMain.NavigateToString(finalContent);
        }
    }
}