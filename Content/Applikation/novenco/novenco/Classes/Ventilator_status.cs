using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes
{
    public class Ventilator_status
    {
        public int Ventilator_status_id { get; set; }
        public DateTime Datetime { get; set; }
        public int Celcius { get; set; }
        public int Hertz { get; set; }
        public int kWh { get; set; }
        public int Amps { get; set; }
        public string Validated { get; set; }
        public Ventilator Ventilator { get; set; }

        public Ventilator_status(DataRow _row, Ventilator _Ventilator)
        {
            Ventilator_status_id = Convert.ToInt32(_row["Ventilator_status_id"].ToString());
            Datetime = DateTime.Parse(_row["Datetime"].ToString());
            Celcius = Convert.ToInt32(_row["Celcius"].ToString());
            Hertz = Convert.ToInt32(_row["Hertz"].ToString());
            kWh = Convert.ToInt32(_row["kWh"].ToString());
            Amps = Convert.ToInt32(_row["Amps"].ToString());
            Validated = _row["Validated"].ToString();
            Ventilator = _Ventilator;
        }

        public Ventilator_status()
        {
        }
    }
}
