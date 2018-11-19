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

        private static SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
    }
}
