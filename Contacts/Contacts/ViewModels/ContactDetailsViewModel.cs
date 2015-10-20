using System;
using System.Net.Http;
using Contacts.Helpers;
using Contacts.Models;
using Contacts.ViewModels;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Contacts
{
	public class ContactDetailsViewModel:BaseViewModel
	{
		private Contact _Contact;
		public ContactDetailsViewModel (Contact contact)
		{
			_Contact = contact;	
		}

		public async Task<bool> ModifyContact(Contact contact)
		{
			var client = new HttpClient();
			var data = Newtonsoft.Json.JsonConvert.SerializeObject(contact);
			var content = new System.Net.Http.StringContent(data, System.Text.Encoding.UTF8, "application/json");

			// Send a request asynchronously and continue when complete
			var response = await client.PutAsync(string.Format("{0}/{1}", API.ContactsAPI,contact.ContactID) , content);

			// Check that response was successful or throw exception
			try{
			response.EnsureSuccessStatusCode();
			}
			catch(Exception e) {
                Debug.WriteLine(e);
                return false;
			}

			return true;

		}

		public async Task<bool> DeleteContact(Contact contact)
		{
			var client = new HttpClient();
			var data = string.Format ("{0}/{1}", API.ContactsAPI, contact.ContactID);
			// Send a request asynchronously and continue when complete
			var response = await client.DeleteAsync(data);

			// Check that response was successful or throw exception
			try{
				response.EnsureSuccessStatusCode();
			}
			catch(Exception e) {
                Debug.WriteLine(e);
				return false;
			}

			return true;
		}



		public Contact Contact
		{
			get { return _Contact; }
			set
			{
				_Contact = value;
				OnPropertyChanged("Contact");
			}
		}

	}
}

