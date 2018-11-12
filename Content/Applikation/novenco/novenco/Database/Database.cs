using System;
using System.Collections.Generic;
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
                else
                {
                    //do yes stuff
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
                else
                {
                    //do yes stuff
                }
            }
        }

        public static void TestConnection()
        {

            //DataTable table = new DataTable();
            int success = 0;
            try
            {
                OpenConnection();
                //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employee", connection);
                //SqlCommand command = new SqlCommand("SELECT * FROM Employee", connection);
                SqlCommand command = new SqlCommand("INSERT INTO Employee([Name], Phonenumber, Email, FK_Company_id) VALUES('Test', 'Test', 'Test@Test.Test', 1)", connection);
                //adapter.Fill(table);
                success = command.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
            //string test = "";
            //foreach (DataRow row in table.Rows)
            //{
            //    test += row.ToString();
            //}
            //MessageBox.Show(test);
        }

        private static SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
    }
}
