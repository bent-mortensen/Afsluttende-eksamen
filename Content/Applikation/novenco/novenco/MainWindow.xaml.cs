﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using novenco.Classes;
using novenco.Database;
using novenco.Windows;

namespace novenco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        ObservableCollection<Ventilator_status> statusList = new ObservableCollection<Ventilator_status>();
        ObservableCollection<Ventilator_status> failedVentilatorStatusList = new ObservableCollection<Ventilator_status>();
        ObservableCollection<Ventilator_status> validVentilatorStatusList = new ObservableCollection<Ventilator_status>();
        ObservableCollection<Employee> employeeList = new ObservableCollection<Employee>();

        public MainWindow()
        {
            InitializeComponent();
            FillComboboxForServiceTechnicians();
            GetStatusAndPopulateLists();

        }

        Employee emp = new Employee();
        // Set service montør
        private void FillComboboxForServiceTechnicians()
        {
            employeeList = DB.GetServiceTechnicians();
            ServiceTechnicians.DisplayMemberPath = emp.GetPathName();
            ServiceTechnicians.ItemsSource = employeeList;
        }

        private void GetStatusAndPopulateLists()
        {
            // Liste af ting programmet skal starte med at gører.
            // Hent statusser og gem i en liste. 
            statusList = DB.GetVentilatorStatus();

            // Validering af ventilatorens målte værdier mod ventilatorens Sap værdier. samt opdeling af statusser.
            ValidateSapValues();

            // Angiver hvilken error kode ventilatoren skal have.
            //DeterminVentilatorError(failedVentilatorStatus);

            // Validere gyldige statusser på databasen ud fra ventilator status id.
            ValidateStatus(validVentilatorStatusList);

            // finde error type og opdaterer databasen denne metode er ikke nødvendig hvis jeg vælger at gemme ventilator error i det øjeblik jeg opretter rapporten.
            //DeterminVentilatorError(failedVentilatorStatus);

            // viser data i datagrid
            invalidStatus.ItemsSource = failedVentilatorStatusList;
            validStatus.ItemsSource = validVentilatorStatusList;
        }

        private void DeterminVentilatorError(ObservableCollection<Ventilator_status> _failedVentilatorStatus)
        {
            //ObservableCollection<Error_type> errorTypes = DB.GetErrorTypes();

            foreach (var item in _failedVentilatorStatus)
            {
                if (item.Celcius > item.Ventilator.SAP.Celcius)
                {
                    DB.StoreVentilationError(2, item.Ventilator.Ventilator_id);
                }
                if (item.kWh > item.Ventilator.SAP.kWh)
                {
                    DB.StoreVentilationError(4, item.Ventilator.Ventilator_id);
                }
                if (item.Amps > item.Ventilator.SAP.Amps)
                {
                    DB.StoreVentilationError(3, item.Ventilator.Ventilator_id);
                }
                if (item.Hertz > item.Ventilator.SAP.Hertz)
                {
                    DB.StoreVentilationError(1, item.Ventilator.Ventilator_id);
                }
            }
        }


        // persistere statusser til databasen med "valid"
        private void ValidateStatus(ObservableCollection<Ventilator_status> _validVentilatorStatus)
        {
            foreach (var item in _validVentilatorStatus)
            {
                DB.UpdateSingleStatusToValid(item.Ventilator_status_id);
            }
        }

        // inddeler statusser i failed og valid
        private void ValidateSapValues()
        {
            if (!(statusList.Count == 0))
            {

                ObservableCollection<Ventilator_status> temp = new ObservableCollection<Ventilator_status>();

                foreach (var item in statusList)
                {
                    // Bestemmer om en status ligger over eller under Sap værdierne. hvis bare en ligger over regnes hele statussen som værende en fejl.
                    if (item.Amps > item.Ventilator.SAP.Amps ||
                        item.Celcius > item.Ventilator.SAP.Celcius ||
                        item.Hertz > item.Ventilator.SAP.Hertz ||
                        item.kWh > item.Ventilator.SAP.kWh)
                    {
                        //temp.Add(item);
                        failedVentilatorStatusList.Add(item);
                    }
                    // tilføjer alle valide statusser uanset om der er gengangere.
                    else
                    {
                        validVentilatorStatusList.Add(item);
                    }
                }

                // Get Earliest timestamp -miliseconds
                //DateTime date = new DateTime();

                //date = temp[0].Datetime;
                //foreach (var item in temp)
                //{
                //    if (date < item.Datetime)
                //    {
                //        date = item.Datetime;
                //    }
                //}



                //foreach (var item in temp)
                //{
                //    if (!(temp.Count == 0))
                //    {
                //        date = temp[0].Datetime;
                //    }



                //    foreach (var items in temp)
                //    {
                //        if (date > items.Datetime)
                //        {
                //            date = items.Datetime;
                //        }
                //    }





                //    failedVentilatorStatus.Add(item);
                //}






                //int tempwe = 1;
            }





            //foreach (var item in statusList)
            //{
            //    // Bestemmer om en status ligger over eller under Sap værdierne. hvis bare en ligger over regnes hele statussen som værende en fejl.
            //    if (item.Amps > item.Ventilator.SAP.Amps ||
            //        item.Celcius > item.Ventilator.SAP.Celcius ||
            //        item.Hertz > item.Ventilator.SAP.Hertz ||
            //        item.kWh > item.Ventilator.SAP.kWh)
            //    {
            //        // tilføjer den første status, hvis en fejlet status er den første som skal i listen.
            //        if (failedVentilatorStatus.Count == 0)
            //        {
            //            failedVentilatorStatus.Add(item);
            //        }
            //        // tilføjer kun nye ventilatorer med fejl værdier. 
            //        else
            //        {
            //            foreach (var status in failedVentilatorStatus)
            //            {
            //                if (!(status.Ventilator.Ventilator_id == item.Ventilator.Ventilator_id))
            //                {
            //                    failedVentilatorStatus.Add(status);
            //                }
            //            }
            //        }
            //    }
            //    // tilføjer alle valide statusser uanset om der er gengangere.
            //    else
            //    {
            //        validVentilatorStatus.Add(item);
            //    }
            //}
        }

        // Events, Buttons, Row click, menubar...
        MockDataGeneratorWindow window_mockDataGeneratorWindow;
        private void OpenMockDataGenerator(object sender, RoutedEventArgs e)
        {
            window_mockDataGeneratorWindow = new MockDataGeneratorWindow();
            window_mockDataGeneratorWindow.Show();
        }

        private void MenuItem_Click_CloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateStatusList(object sender, RoutedEventArgs e)
        {
            // tømmer listerne og fylder i dem igen.
            failedVentilatorStatusList = new ObservableCollection<Ventilator_status>();
            validVentilatorStatusList = new ObservableCollection<Ventilator_status>();
            GetStatusAndPopulateLists();
        }

        ErrorStatus window_ErrorStatus;
        private void Row_DoubleClick_Show_Invalid_Status(object sender, MouseButtonEventArgs e)
        {
            Employee selectedEmployee = (Employee)ServiceTechnicians.SelectedItem;
            if (!(selectedEmployee == null))
            {
                Ventilator_status selectedVentilatorStatus = (Ventilator_status)invalidStatus.SelectedItem;
                window_ErrorStatus = new ErrorStatus(selectedVentilatorStatus, selectedEmployee);
                window_ErrorStatus.Show();
            }
            else
            {
                MessageBox.Show("Vælg service montør!", "Manlgende input", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void Row_DoubleClick_Show_Valid_Status(object sender, MouseButtonEventArgs e)
        {
            Ventilator_status selectedVentilatorStatus = (Ventilator_status)validStatus.SelectedItem;
            Employee selectedEmployee = (Employee)ServiceTechnicians.SelectedItem;
            ErrorStatus window = new ErrorStatus(selectedVentilatorStatus, selectedEmployee);
            window.Show();
        }

        UpdateSAPValues window_UpdateSAPValues;
        private void MenuItem_Click_Gold(object sender, RoutedEventArgs e)
        {
            Open_UpdateSAPValues(1);
        }
        private void MenuItem_Click_Silver(object sender, RoutedEventArgs e)
        {
            Open_UpdateSAPValues(2);
        }
        private void MenuItem_Click_Kobber(object sender, RoutedEventArgs e)
        {
            Open_UpdateSAPValues(3);
        }
        private void Open_UpdateSAPValues(int _pakke)
        {
            window_UpdateSAPValues = new UpdateSAPValues(_pakke);
            window_UpdateSAPValues.Show();
        }

        private void MenuItem_Click_Remove_Error(object sender, RoutedEventArgs e)
        {
            // fjern fejlen fra listen.

            MessageBox.Show("Fjern fejlen");
        }

        UserCorrectError window_UserCorrectError;
        private void MenuItem_Click_User_Correct_Error(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (Employee)ServiceTechnicians.SelectedItem;
            if (!(selectedEmployee == null))
            {
                window_UserCorrectError = new UserCorrectError(selectedEmployee);
                window_UserCorrectError.Show();
            }
            else
            {
                MessageBox.Show("Vælg service montør!", "Manlgende input", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
    }
}