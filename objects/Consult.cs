using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev_Final_Project__Medical_Records_.objects
{
    public class Consult
    {
        public int id { get; set; }
        public int patient_id { get; set; }
        public decimal weight { get; set; }
        public decimal height { get; set; }
        public int bpFirst { get; set; }
        public int bpSecond { get; set; }
        public string bp { get; set; }
        private string bp_remarks { get; set; }
        public string Bp_remarks 
        {
            get 
            {
                if ((bpFirst > 180 && bpSecond > 120) || (bpFirst > 180 || bpSecond > 120))
                {
                    return "Hypertensive Crisis";
                }
                else if ((bpFirst >= 140 && bpFirst <= 180) || (bpSecond >= 90 && bpSecond <= 120))
                {
                    return "High Blood Pressure (Stage 2)";
                }
                else if ((bpFirst >= 130 && bpFirst <= 139) || (bpSecond >= 80 && bpSecond <= 89))
                {
                    return "High Blood Pressure (Stage 1)";
                }
                else if ((bpFirst >= 120 && bpFirst <= 129) && bpSecond < 80)
                {
                    return "Elevated";
                }
                else if (bpFirst < 120 && bpSecond < 80)
                {
                    return "Normal";
                }
                else
                {
                    return "Out of Range";
                }
            }
            set
            {
                if ((bpFirst > 180 && bpSecond > 120) || (bpFirst > 180 || bpSecond > 120))
                {
                    bp_remarks = "Hypertensive Crisis";
                }
                else if ((bpFirst >= 140 && bpFirst <= 180) || (bpSecond >= 90 && bpSecond <= 120))
                {
                    bp_remarks = "High Blood Pressure (Stage 2)";
                }
                else if ((bpFirst >= 130 && bpFirst <= 139) || (bpSecond >= 80 && bpSecond <= 89))
                {
                    bp_remarks = "High Blood Pressure (Stage 1)";
                }
                else if ((bpFirst >= 120 && bpFirst <= 129) && bpSecond < 80)
                {
                    bp_remarks = "Elevated";
                }
                else if (bpFirst < 120 && bpSecond < 80)
                {
                    bp_remarks = "Normal";
                }
                else
                {
                    bp_remarks = "Out of Range";
                }
            }
        }
        public decimal temperature { get; set; }
        private string _temperature_remarks { get; set; }
        public string temperature_remarks { get; set; }
        public DateTime consultation_date { get; set; }
    }
}
