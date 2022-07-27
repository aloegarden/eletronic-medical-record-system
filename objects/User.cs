using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev_Final_Project__Medical_Records_.objects
{
    public class User
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public int age { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public DateTime dob { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
    }
}
