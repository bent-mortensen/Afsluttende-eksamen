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
        public static void StoreSparePartListItem(int _sparePartListNumber, int _Spare_part_id)
        {
            int success = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Spare_part_list (List_id,FK_Spare_part_id) VALUES (@sparepartlistnumber, @sparepartid)", connection);
            command.Parameters.Add(CreateParam("@sparepartlistnumber", _sparePartListNumber, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@sparepartid", _Spare_part_id, SqlDbType.NVarChar));

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

        // Til test af forbindelse til database - serveren
        public static void TestConnection()
        {

            int success = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Employee([Name], Phonenumber, Email, FK_Company_id) VALUES('Test', 'Test', 'Test@Test.Test', 1)", connection);

            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
                MessageBox.Show("Connection was made " + success.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            catch (Exception)
            {
                MessageBox.Show("TestConnection failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public static void StoreGeneratedStatus(int _Celcius, int _Hertz, int _kWh, int _Amps, int _vent_id)
        {
            int success = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Ventilator_status([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES(CURRENT_TIMESTAMP, @Celcius, @Hertz, @kWh, @Amps, NULL, @vent_id)", connection);
            command.Parameters.Add(CreateParam("@Celcius", _Celcius, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@Hertz", _Hertz, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@kWh", _kWh, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@Amps", _Amps, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@vent_id", _vent_id, SqlDbType.NVarChar));

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

        public static void StoreVentilationError(int _error_type_id, int _ventilator_id)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Ventilator_error(FK_Error_type_id, FK_Ventilator_status_id) VALUES(@error_type_id, @ventilation_id)", connection);
            command.Parameters.Add(CreateParam("@error_type_id", _error_type_id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@ventilation_id", _ventilator_id, SqlDbType.NVarChar));

            try
            {
                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("StoreVentilationError failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }      
        }
    }
}
