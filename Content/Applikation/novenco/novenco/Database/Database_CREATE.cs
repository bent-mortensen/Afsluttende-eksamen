using novenco.Classes;
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
        public static void StoreErrorCorrectionReport(string _error_description, string _error_correction_description, DateTime _correction_date, int _sap_celcius, int _sap_hertz, int _sap_kwh, int _sap_amps, int _sparePartListNumber, int _employeeid, int _ventilator_error_id)
        {

            // INSERT INTO Error_correction_report(Error_description, Error_correction_description, Correction_date, Sap_celcius, Sap_amps, Sap_hertz, Sap_kwh, FK_Ventilator_error_id, FK_Employee_id, FK_Spare_part_list_id) VALUES('beskrivelse af fejlen', 'beskrivelse af tiltag for at rette fejlen', CURRENT_TIMESTAMP, 0, 0, 0, 0, 1, 1, 1);

            int success = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Error_correction_report(Error_description, Error_correction_description, Correction_date, Sap_celcius, Sap_hertz, Sap_kwh, Sap_amps, FK_Spare_part_list_id, FK_Employee_id, FK_Ventilator_error_id) VALUES(@ErrorDescription, @ErrorCorrectionDescription, @CorrectionDate, @SapCelcius, @SapHertz, @SapkWh, @SapAmps, @SparePartListNumber, @EmployeeId, @VentilatorErrorId)", connection);
            command.Parameters.Add(CreateParam("@ErrorDescription", _error_description, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@ErrorCorrectionDescription", _error_correction_description, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@CorrectionDate", _correction_date, SqlDbType.DateTime));
            command.Parameters.Add(CreateParam("@SapCelcius", _sap_celcius, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@SapHertz", _sap_hertz, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@SapkWh", _sap_kwh, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@SapAmps", _sap_amps, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@SparePartListNumber", _sparePartListNumber, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@EmployeeId", _employeeid, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@VentilatorErrorId", _ventilator_error_id, SqlDbType.Int));
                        
            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("StoreErrorCorrectionReport failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void StoreSparePartListItem(int _sparePartListNumber, int _Spare_part_id)
        {
            int success = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Spare_part_list (List_id,FK_Spare_part_id) VALUES (@sparepartlistnumber, @sparepartid)", connection);
            command.Parameters.Add(CreateParam("@sparepartlistnumber", _sparePartListNumber, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@sparepartid", _Spare_part_id, SqlDbType.Int));

            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();                
            }
            catch (Exception)
            {
                MessageBox.Show("StoreSparePartListItem failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void StoreGeneratedStatus(Ventilator_status _newVentilator_status)
        {
            int success = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Ventilator_status([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES(CURRENT_TIMESTAMP, @Celcius, @Hertz, @kWh, @Amps, NULL, @vent_id)", connection);
            command.Parameters.Add(CreateParam("@Celcius", _newVentilator_status.Celcius, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Hertz", _newVentilator_status.Hertz, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@kWh", _newVentilator_status.kWh, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Amps", _newVentilator_status.Amps, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@vent_id", _newVentilator_status.Ventilator.Ventilator_id, SqlDbType.Int));

            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("GenerateStatus failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static int StoreGeneratedStatusAndScopeID(int _Celcius, int _Hertz, int _kWh, int _Amps, int _vent_id)
        {
            int identity = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Ventilator_status([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES(CURRENT_TIMESTAMP, @Celcius, @Hertz, @kWh, @Amps, 'valid', @vent_id); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.Add(CreateParam("@Celcius", _Celcius, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Hertz", _Hertz, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@kWh", _kWh, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Amps", _Amps, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@vent_id", _vent_id, SqlDbType.Int));

            try
            {
                OpenConnection();
                identity = Convert.ToInt32(command.ExecuteScalar());
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("StoreGeneratedStatusAndScopeID failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return identity;
        }
        public static int StoreVentilationError(int _error_type_id, int _ventilator_id)
        {
            int identity = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Ventilator_error(FK_Error_type_id, FK_Ventilator_status_id) VALUES(@error_type_id, @ventilation_id); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.Add(CreateParam("@error_type_id", _error_type_id, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@ventilation_id", _ventilator_id, SqlDbType.Int));

            try
            {
                OpenConnection();
                identity = Convert.ToInt32(command.ExecuteScalar());
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("StoreVentilationError failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return identity;
        }        
        
        // Til test af forbindelse til databasen
        public static bool StoreTestConnection()
        {

            bool success;
            SqlCommand command = new SqlCommand("INSERT INTO Employee([Name], Phonenumber, Email, FK_Company_id)" +
                                                        " VALUES('Test', 'Test', 'Test@Test.Test', 1)", connection);

            try
            {
                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();

                success = true;
                MessageBox.Show("Connection was made " + success.ToString(), 
                "Warning", MessageBoxButton.OK, MessageBoxImage.Question);
                return success;
            }
            catch (Exception)
            {
                MessageBox.Show("TestConnection failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
                return success;
            }

        }
    }
}
