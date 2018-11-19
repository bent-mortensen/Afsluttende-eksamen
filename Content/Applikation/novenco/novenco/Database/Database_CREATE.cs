﻿using System;
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
                MessageBox.Show("Connection was not made", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public static void GenerateStatus(int _Celcius, int _Hertz, int _kWh, int _Amps, int _vent_id)
        {
            // INSERT INTO Ventilator_status([Datetime], Celcius, Hertz, kWh, Amps, Validated, FK_Ventilator_id) VALUES(CURRENT_TIMESTAMP, 111, 70, 10, 10, NULL, 1);

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

                throw;
            }
        }

        public static void SetErrorType(int _ventilator_id, string _errorType)
        {
            
            int success = 0;
            SqlCommand command = new SqlCommand("INSERT INTO Ventilator_error([Type], FK_Ventilator_status_id) VALUES(@errortype, @id)", connection);
            command.Parameters.Add(CreateParam("@id", _ventilator_id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@errortype", _errorType, SqlDbType.NVarChar));

            try
            {
                OpenConnection();
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());               
            }


        }
    }
}
