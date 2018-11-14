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

        public MainWindow()
        {
            InitializeComponent();

            //ObservableCollection<Ventilator_status> status = DB.GetVentilatorStatus();
            // display data in datagrid
            //dataGrid.ItemsSource = status;

            ValidateStatus();
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


        private void ValidateStatus()
        {

            if ()
            {

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
