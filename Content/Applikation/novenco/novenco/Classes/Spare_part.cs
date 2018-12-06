using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace novenco.Classes
{
    public class Spare_part
    {
        public int Spare_part_id { get; set; }
        public string Spare_part_name { get; set; }

        public Spare_part(DataRow _row)
        {
            Spare_part_id = Convert.ToInt32(_row["Spare_part_id"].ToString());
            Spare_part_name = _row["Spare_part_name"].ToString();
        }

        public Spare_part()
        {
        }

        public string GetPathSparePartName()
        {
            string path;
            path = nameof(Spare_part_name);
            return path;
        }
    }
}
