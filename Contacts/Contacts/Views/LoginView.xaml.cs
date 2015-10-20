using System;
using System.Collections.Generic;
using System.Diagnostics;
using Contacts.ViewModels;
using Xamarin.Forms;

namespace Contacts.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            LoginViewModel vm = new LoginViewModel();
            BindingContext = vm;
            var signIn = new TapGestureRecognizer();
            signIn.Tapped += async (s, e) =>
            {
                
                Debug.Assert(vm != null, "vm != null");
                var go = await vm.Login(this.Username.Text, this.Password.Text);

                if (go)
                    await this.Navigation.PushModalAsync((ContentPage)(new MainView()), false);
                else
                    await DisplayAlert("Authentication failed", "verify username and password", "Try again");

            };
            this.SignInLabel.GestureRecognizers.Add(signIn);

            var newAcct = new TapGestureRecognizer();
            newAcct.Tapped += OnClick;
            this.NewAccount.GestureRecognizers.Add(newAcct);

            var resetProspects = new TapGestureRecognizer();
            resetProspects.Tapped += async (sender, e) =>
            {
                var go = await vm.ResetProspects();

                if (go)
                    await this.Navigation.PushModalAsync((ContentPage)(new MainView()), false);
                else
                    await DisplayAlert("Failure", "Reset Prospects has failed", "Try again");
            };
            this.ResetProspects.GestureRecognizers.Add(resetProspects);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnClick(object sender, EventArgs args)
        {
            this.Navigation.PushAsync(new WebsiteView("http://google.com", "Placeholder"));
        }


    }
}

