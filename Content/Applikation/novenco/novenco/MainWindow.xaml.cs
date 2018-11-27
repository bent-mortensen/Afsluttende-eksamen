using System;
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
        ObservableCollection<Ventilator_status> status = new ObservableCollection<Ventilator_status>();
        ObservableCollection<Ventilator_status> failedVentilatorStatus = new ObservableCollection<Ventilator_status>();
        ObservableCollection<Ventilator_status> validVentilatorStatus = new ObservableCollection<Ventilator_status>();
        ObservableCollection<Ventilator_status> sortedStatus = new ObservableCollection<Ventilator_status>();
        ObservableCollection<Employee> employee = new ObservableCollection<Employee>();

        public MainWindow()
        {
            InitializeComponent();
            FillComboboxForServiceTechnicians();
            GetStatusAndPopulateLists();
        }

        // Set service montør
        private void FillComboboxForServiceTechnicians()
        {
            employee = DB.GetServiceTechnicians();
            ServiceTechnicians.DisplayMemberPath = "Name";
            ServiceTechnicians.ItemsSource = employee;
        }

        private void GetStatusAndPopulateLists()
        {
            // Liste af ting programmet skal starte med at gører.
            // Hent status
            status = DB.GetVentilatorStatus();

            // Validering af IS NULL statusser fra Databasen.
            CheckCurrentISNULLStatus();

            // Validere statusser på databasen ud fra et id.
            ValidateStatus(validVentilatorStatus);

            // finde error type og opdaterer databasen 
            //DetectErrorType(failedVentilatorStatus);

            // viser data i datagrid
            invalidStatus.ItemsSource = failedVentilatorStatus;
            validStatus.ItemsSource = validVentilatorStatus;
        }


        // persistere statusser til databasen med "valid"
        private void ValidateStatus(ObservableCollection<Ventilator_status> _validVentilatorStatus)
        {
            foreach (var item in _validVentilatorStatus)
            {
                DB.ValidateStatus(item.Ventilator_status_id);
            }
        }

        // inddeler statusser i failed og valid
        private void CheckCurrentISNULLStatus()
        {
            foreach (var item in status)
            {
                if (item.Amps > item.Ventilator.SAP.Amps ||
                    item.Celcius > item.Ventilator.SAP.Celcius ||
                    item.Hertz > item.Ventilator.SAP.Hertz ||
                    item.kWh > item.Ventilator.SAP.kWh)
                {
                    failedVentilatorStatus.Add(item);
                }
                else
                {
                    validVentilatorStatus.Add(item);
                }
            }
        }

        // Events, Buttons, Row click, menubar...
        private void OpenMockDataGenerator(object sender, RoutedEventArgs e)
        {
            MockDataGeneratorWindow window = new MockDataGeneratorWindow();
            window.Show();
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateStatusList(object sender, RoutedEventArgs e)
        {
            // tømmer listerne og fylder i dem igen.
            sortedStatus = new ObservableCollection<Ventilator_status>();
            failedVentilatorStatus = new ObservableCollection<Ventilator_status>();
            validVentilatorStatus = new ObservableCollection<Ventilator_status>();
            GetStatusAndPopulateLists();
        }

        private void Row_DoubleClick_Show_Invalid_Status(object sender, MouseButtonEventArgs e)
        {
            Employee selectedEmployee = (Employee)ServiceTechnicians.SelectedItem;
            if (!(selectedEmployee == null))
            {
                Ventilator_status selectedVentilatorStatus = (Ventilator_status)invalidStatus.SelectedItem;
                ErrorStatus window = new ErrorStatus(selectedVentilatorStatus, selectedEmployee);
                window.Show();
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

        private void MenuItem_Click_Gold(object sender, RoutedEventArgs e)
        {
            UpdateSAPValues window = new UpdateSAPValues(1);
            window.Show();
        }

        private void MenuItem_Click_Silver(object sender, RoutedEventArgs e)
        {
            UpdateSAPValues window = new UpdateSAPValues(2);
            window.Show();
        }

        private void MenuItem_Click_Kobber(object sender, RoutedEventArgs e)
        {
            UpdateSAPValues window = new UpdateSAPValues(3);
            window.Show();
        }

        private void MenuItem_Click_Remove_Error(object sender, RoutedEventArgs e)
        {
            // fjern fejlen fra listen.

            MessageBox.Show("Fjern fejlen");
        }
    }
}
