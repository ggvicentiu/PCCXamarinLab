using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Models
{
    public class Prospect
    {
        internal string Phone;

        public string Name { get; set; }
        public string PicUrl { get; set; }
        public int ProspectID { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public string Location { get; set; }
        public string Notes { get; internal set; }
    }
}
