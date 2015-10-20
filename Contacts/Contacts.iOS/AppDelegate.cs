using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Contacts.Helpers;
using Contacts.ViewModels;
using Foundation;
using UIKit;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace Contacts.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public App Contacts {
			get;
			private set;
		}
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
            global::ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();

			Contacts = new App ();

			LoadApplication (Contacts);

			UIApplication.SharedApplication.ApplicationSupportsShakeToEdit = true;

			return base.FinishedLaunching (app, options);
		}

	}
}

