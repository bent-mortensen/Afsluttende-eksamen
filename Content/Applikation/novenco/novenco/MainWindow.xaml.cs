using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        List<int> failedVentilatorStatus = new List<int>();
        List<int> validVentilatorStatus = new List<int>();

        public MainWindow()
        {
            InitializeComponent();

            // dette kan vise og fylde en liste på forsiden af appen.
            status = DB.GetVentilatorStatus();
            // display data in datagrid
            //dataGrid.ItemsSource = status;


            // validating IS NULL statusser from Database.
            CheckCurrentISNULLStatus();

            //
            ValidateStatus(validVentilatorStatus);

            int temp = 2 + 2;

        }

        private void ValidateStatus(List<int> _validVentilatorStatus)
        {
            foreach (var item in _validVentilatorStatus)
            {
                DB.ValidateStatus(item);
            }
        }

        private void DetectErrorType()
        {
            //Error string
            string errorType = "";

            Ventilator_status vent_status = DB.GetSingleVentilatorStatus();
            Service_agreement_package SAP = DB.GetSAPValues(vent_status.Ventilator_status_id);

            if (vent_status.Celcius > SAP.Celcius || vent_status.Hertz > SAP.Hertz || vent_status.kWh > SAP.kWh || vent_status.Amps > SAP.Amps)
            {
                //Error type
                if (vent_status.Celcius > SAP.Celcius)
                {
                    errorType = "Celcius";
                }
                if (vent_status.Hertz > SAP.Hertz)
                {
                    errorType = "Hertz";
                }
                if (vent_status.kWh > SAP.kWh)
                {
                    errorType = "kWh";
                }
                if (vent_status.Amps > SAP.Amps)
                {
                    errorType = "Amps";
                }
            }

            DB.SetErrorType(vent_status.Ventilator.Ventilator_id, errorType);
        }


        private void CheckCurrentISNULLStatus()
        {
            foreach (var item in status)
            {
                if (item.Amps > item.Ventilator.SAP.Amps ||
                    item.Celcius > item.Ventilator.SAP.Celcius ||
                    item.Hertz > item.Ventilator.SAP.Hertz ||
                    item.kWh > item.Ventilator.SAP.kWh)
                {
                    failedVentilatorStatus.Add(item.Ventilator_status_id);
                }
                else
                {
                    validVentilatorStatus.Add(item.Ventilator_status_id);
                }
            }
        }

        // Hent status
        // Sammenlign status med SAP værdier
        // Fejl skal i toppen af listen
        // Resten af ventilatorer skal ligge under listen

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DB.TestConnection();
        }
    }
}
