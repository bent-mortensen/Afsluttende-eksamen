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
            adapter.SelectCommand.Parameters.Add(CreateParam("@id", _statusID, SqlDbType.NVarChar));

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
            adapter.SelectCommand.Parameters.Add(CreateParam("@id", _service_agreement_package_id, SqlDbType.NVarChar));

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
