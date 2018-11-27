using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes
{
    public class Employee
    {
              
        public int Employee_id { get; set; }
        public string Name { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public Company Company { get; set; }

        public Employee(DataRow _row)
        {
            Employee_id = Convert.ToInt32(_row["Employee_id"].ToString());
            Name = _row["Name"].ToString();
            Phonenumber = _row["Phonenumber"].ToString();
            Email = _row["Email"].ToString();            
        }
    }
}
