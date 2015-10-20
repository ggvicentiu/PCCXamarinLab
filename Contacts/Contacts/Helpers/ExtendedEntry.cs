using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contacts.Helpers
{
    public class ExtendedEntry : Entry
    {
        public static readonly BindableProperty FontProperty =
                BindableProperty.Create("Font", typeof(Font),
                typeof(ExtendedEntry), new Font());

        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }
    }

  
}
