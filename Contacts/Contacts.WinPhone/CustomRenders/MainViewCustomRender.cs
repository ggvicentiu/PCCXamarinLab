using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using ShakeGestures;
using System.Windows;

[assembly: ExportRenderer(typeof(Contacts.Views.MainView), typeof(Contacts.WinPhone.DependencyServices.MainViewCustomRender))]
namespace Contacts.WinPhone.DependencyServices
{
    public class MainViewCustomRender : PageRenderer
    {
        MainView mainView;

        public MainViewCustomRender()
        {

            ShakeGesturesHelper.Instance.ShakeGesture += Instance_ShakeGesture;
            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 3;
            ShakeGesturesHelper.Instance.Active = true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            mainView = e.NewElement as MainView;
        }

        private void Instance_ShakeGesture(object sender, ShakeGestureEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (mainView != null)
                {
                    mainView.ShakeReceived();
                }
            });
        }
    }
}
