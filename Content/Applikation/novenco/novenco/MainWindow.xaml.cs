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

        //List<int> failedVentilatorStatus = new List<int>();
        //List<int> validVentilatorStatus = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            
            // Liste af ting programmet skal starte med at gører.
            // Hent status
            status = DB.GetVentilatorStatus();

            // Validering af IS NULL statusser fra Databasen.
            CheckCurrentISNULLStatus();

            // Validere statusser på databasen ud fra et id.
            ValidateStatus(validVentilatorStatus);

            // finde error type og opdaterer databasen 
            DetectErrorType(failedVentilatorStatus);

            // Sorteret liste med fejl statusser øverst
            SortedListOfStatus();

            // viser data i datagrid
            dataGrid.ItemsSource = sortedStatus;
        }

        #region Event timer


        // Timer til event
        System.Timers.Timer timer = new System.Timers.Timer();
        private void SetupEvent()
        {
            timer.Interval = 10000;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(UpdateItemsSource);
        }

        private void UpdateItemsSource(object sender, ElapsedEventArgs e)
        {
            // Hent ventilator status
            status = DB.GetVentilatorStatus();

            // Validering af IS NULL statusser fra Databasen.
            CheckCurrentISNULLStatus();

            // Validere statusser på databasen ud fra et id.
            ValidateStatus(validVentilatorStatus);

            // finde error type og opdaterer databasen 
            DetectErrorType(failedVentilatorStatus);

            // Sorteret liste med fejl statusser øverst
            SortedListOfStatus();
        }
        #endregion

        public void SortedListOfStatus()
        {
            foreach (var item in failedVentilatorStatus)
            {
                sortedStatus.Add(item);
            }

            foreach (var item in validVentilatorStatus)
            {
                // virker ikke, men skal hente mindst 10 valid statusser ud.
                sortedStatus.Add(item);
            }
        }

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




        // Buttons
        private void OpenMockDataGenerator(object sender, RoutedEventArgs e)
        {
            MockDataGeneratorWindow window = new MockDataGeneratorWindow();
            window.Show();
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // kig her https://stackoverflow.com/questions/3120616/wpf-datagrid-selected-row-clicked-event


        }
    }
}
