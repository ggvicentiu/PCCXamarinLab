using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contacts.Views
{
    public class WebsiteView : BaseView
    {
        public WebsiteView(string site, string title)
        {
            this.Title = title;
            var webView = new WebView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Source = new UrlWebViewSource
                {
                    Url = site
                }
            };
            Content = webView;
        }
    }
}
