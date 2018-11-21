using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using novenco.Classes;

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

        public static void UpdateServiceAgreementPackage(int _service_agreement_package_id, int _newCelcius, int _newHertz, int _newkWh, int _newAmps)
        {
            //UPDATE Service_agreement_package SET Sap_Celcius = 12, Sap_Hertz = 12, Sap_kWh = 12, Sap_Amps = 12;

        }
    }
}
