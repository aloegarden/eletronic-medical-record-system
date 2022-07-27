using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev_Final_Project__Medical_Records_.objects
{
    public class UserLogin : User
    {
        public DateTime loginTime { get; set; }
        public DateTime logoutTime { get; set; }
    }
}
