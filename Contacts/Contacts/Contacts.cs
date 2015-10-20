using System;
using Contacts.Views;
using Xamarin.Forms;

namespace Contacts
{
    public class App : Application
    {

        public App()
        {

            var mainPage = new NavigationPage(new LoginView());
            MainPage = mainPage;


            MessagingCenter.Subscribe<App>(this, "MessageText", (sender) =>
                {
                    MainPage = mainPage;
                });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

