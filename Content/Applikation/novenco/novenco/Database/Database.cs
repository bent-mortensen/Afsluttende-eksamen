using novenco.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
        static private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);

        private static void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.ToString(), "Please try again later!", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    //do no stuff
                }
            }
        }

        private static void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.ToString(), "Please try again later!", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    //do no stuff
                }
            }
        }


        public static Ventilator_status GetSingleVentilatorStatus()
        {
            Ventilator_status vent_status = new Ventilator_status();



            return vent_status;
        }
        public static Service_agreement_package GetSAPValues(int ventilator_status_id)
        {
            Service_agreement_package SAP = new Service_agreement_package();



            return SAP;
        }

        public static ObservableCollection<Ventilator_status> GetVentilatorStatus()
        {
            ObservableCollection<Ventilator_status> status = new ObservableCollection<Ventilator_status>();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Ventilator_status.*, Company.*, Ventilator.*, Service_agreement_package.* FROM Ventilator_status INNER JOIN Ventilator ON Ventilator.Ventilator_id = Ventilator_status.FK_Ventilator_id INNER JOIN Company ON Company.Company_id = Ventilator.FK_Company_id INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id WHERE Ventilator_status.Validated IS NULL; ", connection);

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                
            }
            
            foreach (DataRow row in table.Rows)
            {
                Company company = new Company(row);
                Service_agreement_package service_Agreement_Package = new Service_agreement_package(row);
                Ventilator ventilator = new Ventilator(row, company, service_Agreement_Package);
                status.Add(new Ventilator_status(row, ventilator));
            }
            //MessageBox.Show(test);

            return status;
        }

        internal static void SetErrorType(int ventilator_id, string errorType)
        {
            
        }

        public static void TestConnection()
        {


            int success = 0;
            try
            {
                OpenConnection();

                SqlCommand command = new SqlCommand("INSERT INTO Employee([Name], Phonenumber, Email, FK_Company_id) VALUES('Test', 'Test', 'Test@Test.Test', 1)", connection);
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                
            }

        }

        private static SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
    }
}
