using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Views;
using Contacts.Helpers;
using Contacts.Models;
using Contacts.ViewModels;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace Contacts.Views
{
    public partial class ProspectDetailView : ContentPage
    {
        private ProspectDetailViewModel _prospectDetailViewModel;

        public ProspectDetailView()
        {
            InitializeComponent();
        }


        public ProspectDetailViewModel PDvm
        {
            get { return _prospectDetailViewModel; }
            set
            {
                _prospectDetailViewModel = value;
                BindingContext = _prospectDetailViewModel;
                this.ProspectName.FormattedText = _prospectDetailViewModel.ProspectDetail.Name;
            }

        }

        public async void OnBackButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        Contact GetContact()
        {
            var c = new Contact
            {
                Address = PDvm.ProspectDetail.Location,
                Name = PDvm.ProspectDetail.Name,
                PicUrl = PDvm.ProspectDetail.PicUrl,
                ContactID = PDvm.ProspectDetail.UserID
            };
            return c;
        }



        public async void OnAddClick(object sender, EventArgs args)
        {
            var contact = GetContact();
            await ProspectDetailViewModel.AddContact(contact);
            await API.UpdateStatus(PDvm.ProspectDetail, API.Status.Converted);
            await this.Navigation.PushModalAsync(new MainView());
        }

        

        public async void OnIgnoreClick(object sender, EventArgs args)
        {
            var task = GetContact();
            var ignore = await DisplayAlert("Alert!", "Are you sure you want to ignore this prospect?", "Ok", "Cancel");
            if ((bool)ignore)
            {
                await API.UpdateStatus(PDvm.ProspectDetail, API.Status.Ignored);
                await this.Navigation.PushModalAsync(new MainView());

            }
        }

        public async void OnCancelClick(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        public void OnLogOut(object s, EventArgs e)
        {
            Common.Logout(this);
        }
    }
}
