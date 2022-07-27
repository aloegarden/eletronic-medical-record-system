using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev_Final_Project__Medical_Records_.objects
{
    public class Patient
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public DateTime dob { get; set; }
    }
}
