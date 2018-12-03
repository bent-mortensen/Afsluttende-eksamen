using System;
using System.Collections;
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
    // Database_UPDATE
    public static partial class DB
    {
        // Opdater alle statusser for en ventilator ud fra ventilator id'et
        public static void UpdateAllStatusesForOneVentilator(int _ventilator_id)
        {
            int success = 0;
            SqlCommand command = new SqlCommand("UPDATE Ventilator_status SET Validated = 'valid' WHERE FK_Ventilator_id = @id AND Validated IS NULL;", connection);
            command.Parameters.Add(CreateParam("@id", _ventilator_id, SqlDbType.Int));

            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("UpdateAllStatusesForOneVentilator failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Update Status to Validated
        public static void UpdateSingleStatusToValid(int _number)
        {
            int success = 0;
            SqlCommand command = new SqlCommand("UPDATE Ventilator_status SET Validated = 'valid' WHERE Ventilator_status_id = @id AND Validated IS NULL;", connection);
            command.Parameters.Add(CreateParam("@id", _number, SqlDbType.Int));

            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("UpdateSingleStatusToValid failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void UpdateServiceAgreementPackage(int _service_agreement_package_id, int _newCelcius, int _newHertz, int _newkWh, int _newAmps)
        {
            //UPDATE Service_agreement_package SET Sap_Celcius = 12, Sap_Hertz = 12, Sap_kWh = 12, Sap_Amps = 12;
            int success = 0;
            SqlCommand command = new SqlCommand("UPDATE Service_agreement_package SET Sap_Celcius = @celcius, Sap_Hertz = @hertz, Sap_kWh = @kwh, Sap_Amps = @amps WHERE Service_agreement_package_id = @id", connection);
            command.Parameters.Add(CreateParam("@id", _service_agreement_package_id, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@celcius", _newCelcius, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@hertz", _newHertz, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@kwh", _newkWh, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@amps", _newAmps, SqlDbType.Int));

            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("UpdateServiceAgreementPackage failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
