using System;
using System.Collections.Generic;
using Contacts.Helpers;
using Contacts.Models;
using Contacts.ViewModels;
using Xamarin.Forms;
using System.Diagnostics;

namespace Contacts.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            try
            {

                InitializeComponent();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            BindingContext = new MainViewModel() { IsBusy = true };
            PopulateProspects();
            PopulateContacts();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void OnLogOut(object s, EventArgs e)
        {
            Common.Logout(this);
        }


        private async void PopulateContacts()
        {
            var vm = (MainViewModel)this.BindingContext;
            await vm.HydrateContactsMainViewModel();

            this.Contacts.HasUnevenRows = true;
            this.Contacts.ItemsSource = vm.Contacts;
            this.Contacts.WidthRequest = ((Page)this).Width * 7 / 8;
            this.Contacts.HeightRequest = ((Page)this).Height * 2 / 3;
            this.ContactsBG.WidthRequest = ((Page)this).Width * 7 / 8;
            this.ContactsBG.HeightRequest = ((Page)this).Height * 10 / 25;
            this.Contacts.HorizontalOptions = LayoutOptions.Center;
            this.Contacts.ItemTapped += (s, e) => Navigation.PushModalAsync(
                new ContactDetailView
                {
                    ContactDetailsVm = new ContactDetailsViewModel(
                        ((ListView)
                            s).SelectedItem as Contact)
                }, false);
            this.Contacts.ItemTemplate = new DataTemplate(() =>
            {
                var what = new ImageCell();
                what.SetBinding(ImageCell.TextProperty, "Name");
                what.SetBinding(ImageCell.ImageSourceProperty, "PicUrl");
                what.SetBinding(ImageCell.DetailProperty, "Phone");
                vm.IsBusy = false;
                return what;
            });
        }

        private async void PopulateProspects()
        {

            var vm = (MainViewModel)this.BindingContext;
            await vm.HydrateMainViewModel();

            var i = 0;
            foreach (var p in vm.Prospects)
            {
                CreateCustomCell(p, i, vm);
                i++;
            }
        }

        private void CreateCustomCell(Prospect prospect, int i, MainViewModel vm)
        {

            this.Prospects.Children.Add(
                   
                    new TableView
                    {
                        HasUnevenRows = true,
                        BackgroundColor = Color.Transparent, // Xamarin.Forms.Color.FromHex("8fc1d0"),
                        Opacity = 70.0,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HeightRequest = (Device.OS == TargetPlatform.iOS) ? this.Height * 1.25 / 10 : this.Height * 2.5 / 10,
                        Root = new TableRoot
                        {
                            new TableSection
                            {
                                new ProspectCustomCell(
                                    new Label {Text = prospect.Name}, i, vm.Prospects.Count, this,prospect)
                            }
                        }
                    }
                );
        }

        public void ShakeReceived()

        {

            int contentSize = (int)ProspectScrollView.ContentSize.Width;

            if (ProspectScrollView.ScrollX < contentSize)
            {

                ProspectScrollView.ScrollToAsync(ProspectScrollView.ScrollX + ProspectScrollView.Width + 6, 0, true);

            }
            else

            {

                ProspectScrollView.ScrollToAsync(0, 0, true);

            }

        }
    }
}
