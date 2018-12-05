using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes
{
    public class Ventilator
    {
        public int Ventilator_id { get; set; }
        public string Address { get; set; }
        public Company Company { get; set; }
        public Service_agreement_package SAP { get; set; }


        public Ventilator(DataRow _row, Service_agreement_package _service_Agreement_Package)
        {
            Ventilator_id = Convert.ToInt32(_row["Ventilator_id"].ToString());
            Address = _row["Address"].ToString();
            SAP = _service_Agreement_Package;
        }

        public Ventilator(DataRow _row, Company _company, Service_agreement_package _service_Agreement_Package)
        {
            Ventilator_id = Convert.ToInt32(_row["Ventilator_id"].ToString());
            Address = _row["Address"].ToString();
            Company = _company;
            SAP = _service_Agreement_Package;

        }

        public Ventilator()
        {
        }
    }
}
