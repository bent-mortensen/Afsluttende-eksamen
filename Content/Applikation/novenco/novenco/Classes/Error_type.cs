using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes
{
    public class Error_type
    {
        public int Error_type_id { get; set; }
        public string Type_name { get; set; }

        public Error_type(DataRow _row)
        {
            Error_type_id = Convert.ToInt32(_row["Error_type_id"].ToString());
            Type_name = _row["Type_name"].ToString();

        }
    }
}
