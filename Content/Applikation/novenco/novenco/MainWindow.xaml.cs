using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
            SetItemSource();
        }

        // Viser data i datagrid.
        private void SetItemSource()
        {
            invalidStatus.ItemsSource = failedVentilatorStatusList;
            validStatus.ItemsSource = validVentilatorStatusList;
        }

        // fyld service montør dropdown.
        Employee emp = new Employee();
        private void FillComboboxForServiceTechnicians()
        {
            employeeList = DB.GetServiceTechnicians();
            ServiceTechnicians.DisplayMemberPath = emp.GetPathName();
            ServiceTechnicians.ItemsSource = employeeList;
        }

        // Henter statusser og gemmer i en liste. 
        private void GetStatusAndPopulateLists()
        {            
            statusList = DB.GetVentilatorStatus();
            SortStatusList();                       
            ValidateStatus(validVentilatorStatusList);
            SetItemSource();
        }
                
        // Validere alle statusser i validVentilatorStatusList.
        private void ValidateStatus(ObservableCollection<Ventilator_status> _validVentilatorStatus)
        {
            foreach (var item in _validVentilatorStatus)
            {
                DB.UpdateSingleStatusToValid(item.Ventilator_status_id);
            }
        }

        // Validering af ventilatorens målte værdier mod ventilatorens Sap værdier. samt opdeling af statusser.
        private void SortStatusList()
        {
            if (!(statusList.Count == 0))
            {
                foreach (var item in statusList)
                {
                    // Bestemmer om en status ligger over eller under Sap værdierne. hvis bare en ligger over regnes hele statussen som værende en fejl.
                    if (item.Amps > item.Ventilator.SAP.Amps ||
                        item.Celcius > item.Ventilator.SAP.Celcius ||
                        item.Hertz > item.Ventilator.SAP.Hertz ||
                        item.kWh > item.Ventilator.SAP.kWh)
                    {
                        failedVentilatorStatusList.Add(item);
                    }
                    // tilføjer alle valide statusser uanset om der er gengangere.
                    else
                    {
                        validVentilatorStatusList.Add(item);
                    }
                }
            }
        }

        // Events, Buttons, Row click, menubar...
        MockDataGeneratorWindow window_mockDataGeneratorWindow;
        ErrorStatus window_ErrorStatus;
        UpdateSAPValues window_UpdateSAPValues;
        UserCorrectError window_UserCorrectError;
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
            SetItemSource();
        }
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
                MessageBox.Show("Vælg service tekniker!", "Manlgende input", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void Row_DoubleClick_Show_Valid_Status(object sender, MouseButtonEventArgs e)
        {
            Ventilator_status selectedVentilatorStatus = (Ventilator_status)validStatus.SelectedItem;
            Employee selectedEmployee = (Employee)ServiceTechnicians.SelectedItem;
            ErrorStatus window = new ErrorStatus(selectedVentilatorStatus, selectedEmployee);
            window.Show();
        }
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
                MessageBox.Show("Vælg service tekniker!", "Manlgende input", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }       
    }
}