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
        public string Description { get; set; }
        public Employee Employee { get; set; }
        public Ventilator_error Ventilator_error { get; set; }
    }
}
