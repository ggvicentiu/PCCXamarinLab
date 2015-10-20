using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Contacts.Helpers;
using Contacts.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.PM;
using System.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Contacts.Droid
{
    [Activity(Label = "Contacts",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}


