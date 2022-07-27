using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev_Final_Project__Medical_Records_.objects
{
    public class Release_Medicine : Medicine
    {
        public int id{ get; set; }
        public int patient_id {get; set; }
        public string reason { get; set; }
        public DateTime date_released { get; set; }

    }
}
