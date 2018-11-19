using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes
{
    public class Service_agreement_package
    {
        public int Service_agreement_package_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Celcius { get; set; }
        public int Hertz { get; set; }
        public int kWh { get; set; }
        public int Amps { get; set; }

        public Service_agreement_package(DataRow _row)
        {
            Service_agreement_package_id = Convert.ToInt32(_row["Service_agreement_package_id"].ToString());
            Name = _row["Sap_Name"].ToString();
            Description = _row["Sap_Description"].ToString();
            Celcius = Convert.ToInt32(_row["Sap_Celcius"].ToString());
            Hertz = Convert.ToInt32(_row["Sap_Hertz"].ToString());
            kWh = Convert.ToInt32(_row["Sap_kWh"].ToString());
            Amps = Convert.ToInt32(_row["Sap_Amps"].ToString());
        }
    }
}
