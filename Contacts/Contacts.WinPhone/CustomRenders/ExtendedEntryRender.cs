using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contacts.Helpers;
using Xamarin.Forms.Platform;
using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(Contacts.Helpers.ExtendedEntry), typeof(Contacts.WinPhone.DependencyServices.ExtendedEntryRenderer))]
namespace Contacts.WinPhone.DependencyServices
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (ExtendedEntry)Element;

            if (view == null) return;

            SetFont(view);
            ResizeHeight();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (ExtendedEntry)Element;

            if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Font")
                SetFont(view);


            ResizeHeight();
        }

        private void SetFont(ExtendedEntry view)
        {
            Font uiFont;
            if (view.Font != Font.Default && (uiFont = view.Font) != null)
            {
                ((Microsoft.Phone.Controls.PhoneTextBox)Control.Children.FirstOrDefault()).FontFamily = new System.Windows.Media.FontFamily(uiFont.FontFamily);
                ((Microsoft.Phone.Controls.PhoneTextBox)Control.Children.FirstOrDefault()).FontSize = uiFont.FontSize;
            }
            else if (view.Font == Font.Default)
            {
                ((Microsoft.Phone.Controls.PhoneTextBox)(Control.Children.FirstOrDefault())).FontSize = 18;
                ((Microsoft.Phone.Controls.PhoneTextBox)(Control.Children.FirstOrDefault())).Height = 65;
                ((Microsoft.Phone.Controls.PhoneTextBox)(Control.Children.FirstOrDefault())).VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            }
        }


        private void ResizeHeight()
        {
            if (Element.HeightRequest >= 0) return;

            Element.HeightRequest = 100;
        }
    }
}