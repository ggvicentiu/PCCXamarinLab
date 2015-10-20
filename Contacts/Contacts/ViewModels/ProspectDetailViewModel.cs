using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contacts.Helpers;
using Contacts.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Contacts.Views;

namespace Contacts.ViewModels
{
    public class ProspectDetailViewModel:BaseViewModel
    {
        private Prospect _prospect;
        public ProspectDetailViewModel(Prospect prospect)
        {
            Title = "Prospect Detail";
            _prospect = prospect;
            
        }

		public async static Task<JObject> AddContact(Contact contact)
        {
			var result =  await API.PostContact (contact);
            return result;
        }

		
        public Prospect ProspectDetail
        {
            get { return _prospect; }
            set
            {
                _prospect = value;
                OnPropertyChanged("ProspectDetail");
            }
        }

        
    }
}
