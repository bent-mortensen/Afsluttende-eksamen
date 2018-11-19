using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace novenco.Database
{
    public static partial class DB
    {

        // Update Status to Validated
        public static void ValidateStatus(int _number)
        {
            int success = 0;
            SqlCommand command = new SqlCommand("UPDATE Ventilator_status SET Validated = 'valid' WHERE Ventilator_status_id = @id AND Validated IS NULL;", connection);
            command.Parameters.Add(CreateParam("@id", _number, SqlDbType.NVarChar));
            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("Status did not get updated", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
