using novenco.Classes;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace novenco.Database
{
    public static partial class DB
    {
        public static ObservableCollection<Ventilator> GetAllVentilators()
        {
            ObservableCollection<Ventilator> ventilators = new ObservableCollection<Ventilator>();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Ventilator.*, Service_agreement_package.* FROM Ventilator INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id;", connection);

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                MessageBox.Show("GetSpareParts failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (DataRow row in table.Rows)
            {
                Service_agreement_package service_Agreement_Package = new Service_agreement_package(row);
                ventilators.Add(new Ventilator(row, service_Agreement_Package));
            }

            return ventilators;
        }

        public static int GetVentilationErrorId(int _error_type_id, int _ventilator_status_id)
        {
            int ventilatorErrorId = 0;
            SqlCommand command = new SqlCommand("SELECT MAX(Ventilator_error_id) FROM Ventilator_error WHERE FK_Error_type_id = @errorTypeId AND FK_Ventilator_status_id = @ventilatorStatusId", connection);
            command.Parameters.Add(CreateParam("@errorTypeId", _error_type_id, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@ventilatorStatusId", _ventilator_status_id, SqlDbType.Int));

            try
            {
                OpenConnection();
                ventilatorErrorId = (int)command.ExecuteScalar();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("GetVentilationErrorId failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return ventilatorErrorId;
        }
        public static int GetMaxValueFromSparePartList()
        {
            int maxValue = 0;
            // SELECT MAX(list_id) FROM Spare_part_list
            SqlCommand command = new SqlCommand("SELECT MAX(list_id) FROM Spare_part_list", connection);
            try
            {
                OpenConnection();
                maxValue = (int)command.ExecuteScalar();
                CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("GetMaxValueFromSparePartList failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return maxValue;
        }


        public static ObservableCollection<Spare_part> GetSpareParts()
        {
            ObservableCollection<Spare_part> list = new ObservableCollection<Spare_part>();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Spare_part;", connection);

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                MessageBox.Show("GetSpareParts failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Spare_part(row));
            }

            return list;

        }
        public static ObservableCollection<Error_type> GetErrorTypes()
        {
            ObservableCollection<Error_type> list = new ObservableCollection<Error_type>();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Error_type;", connection);

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                MessageBox.Show("GetErrorTypes failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Error_type(row));
            }

            return list;

        }

        public static ObservableCollection<Employee> GetServiceTechnicians()
        {
            ObservableCollection<Employee> list = new ObservableCollection<Employee>();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employee;", connection);

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                MessageBox.Show("GetServiceTechnicians failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Employee(row));
            }

            return list;
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
                MessageBox.Show("GetVentilatorStatus failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (DataRow row in table.Rows)
            {
                Company company = new Company(row);
                Service_agreement_package service_Agreement_Package = new Service_agreement_package(row);
                Ventilator ventilator = new Ventilator(row, company, service_Agreement_Package);
                status.Add(new Ventilator_status(row, ventilator));
            }
            return status;
        }

        public static Ventilator_status GetSingleVentilatorStatus(int _statusID)
        {
            ObservableCollection<Ventilator_status> status = new ObservableCollection<Ventilator_status>();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Ventilator_status.*, Company.*, Ventilator.*, Service_agreement_package.* FROM Ventilator_status INNER JOIN Ventilator ON Ventilator.Ventilator_id = Ventilator_status.FK_Ventilator_id INNER JOIN Company ON Company.Company_id = Ventilator.FK_Company_id INNER JOIN Service_agreement_package ON Ventilator.FK_Service_agreement_package_id = Service_agreement_package.Service_agreement_package_id WHERE Ventilator_status.Ventilator_status_id = @id", connection);
            adapter.SelectCommand.Parameters.Add(CreateParam("@id", _statusID, SqlDbType.Int));

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                MessageBox.Show("GetSingleVentilatorStatus failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (DataRow row in table.Rows)
            {
                Company company = new Company(row);
                Service_agreement_package service_Agreement_Package = new Service_agreement_package(row);
                Ventilator ventilator = new Ventilator(row, company, service_Agreement_Package);
                status.Add(new Ventilator_status(row, ventilator));
            }
            Ventilator_status _status = status[0];
            return _status;
        }

        public static Service_agreement_package GetServiceAgreementPackage(int _service_agreement_package_id)
        {
            Service_agreement_package sap = new Service_agreement_package();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Service_agreement_package WHERE Service_agreement_package_id = @id", connection);
            adapter.SelectCommand.Parameters.Add(CreateParam("@id", _service_agreement_package_id, SqlDbType.Int));

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                MessageBox.Show("GetServiceAgreementPackage failed", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            foreach (DataRow row in table.Rows)
            {
                sap = new Service_agreement_package(row);
            }
            return sap;
        }
    }
}
