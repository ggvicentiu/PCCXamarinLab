using System;
using Contacts.Views;
using UIKit;
using EventKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Contacts.iOS;

[assembly:ExportRenderer(typeof(MainView), typeof(MainViewCustomRender))]

namespace Contacts.iOS
{
	public class MainViewCustomRender : PageRenderer
	{
		MainView mainView;
		public MainViewCustomRender ()
		{
		}

		public override bool CanBecomeFirstResponder {
			get {
				return true;
			}
		}
		public override void ViewDidAppear (bool animated)
		{
			BecomeFirstResponder ();

		}
		public override void ViewWillAppear (bool animated)
		{

		}

		public override void MotionEnded (UIEventSubtype motion, UIEvent evt)
		{
			if (motion == UIEventSubtype.MotionShake)
			{
				if (mainView != null) {
					mainView.ShakeReceived ();
				}
			}
			base.MotionEnded (motion, evt);
		} 
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			mainView = e.NewElement as MainView;
			if (mainView != null) {
				
			}
		}
	}
}

