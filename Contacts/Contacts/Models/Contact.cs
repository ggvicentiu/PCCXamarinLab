using System;

namespace Contacts.Models
{
    public partial class Contact
    {
        public int ContactID { get; set; }

       
        public string Name { get; set; }

        public string PicUrl { get; set; }

        
        public string Phone { get; set; }

        
        public string Address { get; set; }

        
        public string Notes { get; set; }
    }
}
