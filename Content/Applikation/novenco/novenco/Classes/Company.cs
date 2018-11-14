using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novenco.Classes
{
    public class Company
    {
        public int Company_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Cvr_number { get; set; }

        public Company(DataRow _row)
        {
            Company_id = Convert.ToInt32(_row["Company_id"].ToString());
            Name = _row["Name"].ToString();
            Description = _row["Description"].ToString();
            Email = _row["Email"].ToString();
            Phonenumber = _row["Phonenumber"].ToString();
            Cvr_number = _row["Cvr_number"].ToString();

        }
    }
}
