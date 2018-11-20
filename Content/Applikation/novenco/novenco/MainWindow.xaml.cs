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

        public MainWindow()
        {
            InitializeComponent();
            GetStatusAndPopulateLists();
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
            DetectErrorType(failedVentilatorStatus);

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

        // detekter error type og skrive til databasen.
        private void DetectErrorType(ObservableCollection<Ventilator_status> _failedVentStatus)
        {
            //Error string
            string errorType = "";
            foreach (var item in _failedVentStatus)
            {
                if (item.Celcius > item.Ventilator.SAP.Celcius)
                {
                    errorType += "Celcius ";
                }
                if (item.Hertz > item.Ventilator.SAP.Hertz)
                {
                    errorType += "Hertz ";
                }
                if (item.kWh > item.Ventilator.SAP.kWh)
                {
                    errorType += "kWh ";
                }
                if (item.Amps > item.Ventilator.SAP.Amps)
                {
                    errorType += "Amps ";
                }

                // INSERT Error type
                DB.SetErrorType(item.Ventilator_status_id, errorType);
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

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Ventilator_status selectedObject = (Ventilator_status)invalidStatus.SelectedItem;
            //Ventilator_status status = DB.GetSingleVentilatorStatus(selectedObject.Ventilator_status_id);
            ErrorStatus window = new ErrorStatus(selectedObject);
            window.Show();





            
        }
    }
}
