using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes
{
    public class Error_correction_report
    {
        public int Error_correction_report_id { get; set; }
        public string Error_description { get; set; }
        public string Error_correction_description { get; set; }
        public DateTime Correction_date { get; set; }
        public int Sap_celcius { get; set; }
        public int Sap_hertz { get; set; }
        public int Sap_kwh { get; set; }
        public int Sap_amps { get; set; }
        public Spare_parts Spare_part_list { get; set; }
        public Employee Employee { get; set; }
        public Ventilator_error Ventilator_error { get; set; }
    }
}
