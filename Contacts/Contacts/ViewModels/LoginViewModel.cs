using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Helpers;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Xamarin.Forms;
using System.Diagnostics;

namespace Contacts.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
        }

        public async Task<bool> Login(string username, string password)
        {
            //this is where the authenticain calls should go. Upon successful authentication, the UserId will be set
            App.Current.Properties["UserId"] = 1;

			return true; 
        }

        public async Task<bool> ResetProspects()
        {
            var client = new HttpClient();
            var url = string.Format("{0}/{1}", API.ContactsAPIBaseAddress, "ResetProspects");
            var response = await client.PostAsync(url,null);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

            return true;
        }

    }
}
