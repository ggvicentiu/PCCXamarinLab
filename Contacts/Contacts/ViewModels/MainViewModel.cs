using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Helpers;
using Contacts.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Diagnostics;

namespace Contacts.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Prospect> Prospects { get; private set; }
        public ObservableCollection<Contact> Contacts { get; private set; }

        public MainViewModel()
        {
            Title = "Main";
            Prospects = new ObservableCollection<Prospect>();
            Contacts = new ObservableCollection<Contact>();
        }
    

        public async Task HydrateMainViewModel()
        {
            string url = string.Empty;
            try
            {
                url = string.Format("{0}/?UserID={1}", API.ProspectsAPI, App.Current.Properties["UserId"]);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
			
            var feeds = await API.Get(url);
            foreach (var prospect in feeds.Select(contactFeed => new Prospect
            {
                Name = contactFeed.Value<string>("Name"),
                PicUrl = contactFeed.Value<string>("PicUrl"),
                ProspectID = contactFeed.Value<int>("ProspectID"),
                Status = contactFeed.Value<byte>("Status"),
                UserID = contactFeed.Value<byte>("UserID")
            }))
            {
                Prospects.Add(prospect);
            }

        }

        public async Task HydrateContactsMainViewModel()
        {
            string url = string.Empty;
            try
            {
                url = string.Format("{0}/?UserID={1}&Completed=0", API.ContactsAPI, App.Current.Properties["UserId"]);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                
            }
			
			var contacts = await API.Get(url);

            foreach (var contact in contacts)
            {

                var c = new Contact
                {
                    Address = contact.Value<string>("Address"),
                    ContactID = contact.Value<int>("ContactID"),
                    Name = contact.Value<string>("Name"),
                    Notes = contact.Value<string>("Notes"), 
                    Phone = contact.Value<string>("Phone"),
                    PicUrl = contact.Value<string>("PicUrl"),

                };

                Contacts.Add(c);
            }

        }

       
    }
}
