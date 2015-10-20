using System;
using System.Collections.Generic;
using Contacts.Views;
using Xamarin.Forms;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Contacts
{
	public partial class ContactDetailView : ContentPage
	{
		DateTime localDateTime;

		private ContactDetailsViewModel _contactDetailsViewModel;
		public ContactDetailView ()
		{
			InitializeComponent ();
		}

		public ContactDetailsViewModel ContactDetailsVm {
			get { return _contactDetailsViewModel; }
			set {
				_contactDetailsViewModel = value;
				BindingContext = _contactDetailsViewModel;
			}

		}

		public async void GoBack(object sender, EventArgs args)
		{
			await Navigation.PopModalAsync ();
		}

		public async void OnSaveClick(object sender, EventArgs args)
		{
			var response = await ContactDetailsVm.ModifyContact (ContactDetailsVm.Contact);
			if (response) {

				DisplayAlert("Success","Your changes have been saved","OK");
			}
		}

		public async void OnDeleteClick(object sender, EventArgs args)
		{
			var response = await ContactDetailsVm.DeleteContact (ContactDetailsVm.Contact);
			if (response) {

				
				await Navigation.PushModalAsync((ContentPage)(new MainView()), false);

			} 
			else {
				await DisplayAlert ("Failure", "An error occured while performing this task. Please contact site administrator", "OK");
			}
		}


		public void OnLogOut(object s, EventArgs e)
		{
			Common.Logout (this);
		}

				
		void OnCallClick(object sender, EventArgs args) 
		{
			string phone = ContactDetailsVm.Contact.Phone;

			if (String.IsNullOrEmpty (phone) == true) 
			{
				DisplayAlert ("Opps!", "Missing Phone Number", "OK");
			} 
			else 
			{
				if(Device.OS!=TargetPlatform.WinPhone)
                Device.OpenUri (new Uri ("telprompt://" + phone));
                else
                    Device.OpenUri(new Uri("tel:" + phone));
			}
		}

		void OnLocationClick(object sender, EventArgs args) 
		{
			string location = ContactDetailsVm.Contact.Address;

			if (String.IsNullOrWhiteSpace (location) == true) 
			{
				DisplayAlert ("Opps!", "Missing Location", "OK");
			}
			else 
			{
				location = System.Net.WebUtility.UrlEncode (location);
				LaunchMapApp(location);
			}
		}

		public void LaunchMapApp(string loc) 
		{
			var request = Device.OnPlatform<string>(

				string.Format("http://maps.apple.com/?q={0}", loc),
				
				string.Format("geo:0,0?q={0}",loc),

				string.Format("bingmaps:?q={0}", loc)
			);

			Device.OpenUri(new Uri(request));
		}

	}
}

