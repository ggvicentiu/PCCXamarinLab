using Contacts.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Helpers
{
    public static class API
    {

        public const string ContactsAPIBaseAddress = "http://contactsapi.azurewebsites.net/api";

        public const string ProspectsAPI = "http://contactsapi.azurewebsites.net/api/prospects";

        public static string Authentication = "TODO:// add auth service";

        public const string ContactsAPI = "http://contactsapi.azurewebsites.net/api/contacts";

        public enum Status : short
        {
            Sent = 0, Ignored = 1, Converted = 2
        }

        public static async Task<JArray> Get(string url)
        {
            // Create an HttpClient instance
            var client = new HttpClient();

            // Send a request asynchronously and continue when complete
            var response = await client.GetAsync(url);

            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();

            // Read response asynchronously as JToken

            var root = await response.Content.ReadAsAsync<JArray>();
            return root;


        }

        public static async Task<JObject> PostContact(Contact contact)
        {
            var modifiedtask = ModifyContact(contact);
            var client = new HttpClient();
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(contact);
            var content = new System.Net.Http.StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(API.ContactsAPI, content);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<JObject>().Result;
        }

        private static Contact ModifyContact(Contact contact)
        {
            var modifiedContact = new Contact
            {
                Address = contact.Address,
                Name = contact.Name,
                Notes = contact.Notes,
                Phone = contact.Phone,
                PicUrl = contact.PicUrl,
                ContactID = 1
            };
            return contact;
        }

        public static async Task<bool> UpdateStatus(Prospect p, API.Status status)
        {
            var client = new HttpClient();
            p.Status = (int)status;
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(p);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var url = string.Format("{0}/{1}", API.ProspectsAPI, p.ProspectID);
            var response = await client.PutAsync( url, content);
            //PUT returns empty content, so we only care if the update was succesfull or not. In case of success server return 204
            return response.IsSuccessStatusCode;
        }
    }
}
