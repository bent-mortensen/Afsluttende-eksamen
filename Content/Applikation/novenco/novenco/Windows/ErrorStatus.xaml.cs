using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using novenco.Classes;

namespace novenco.Windows
{
    /// <summary>
    /// Interaction logic for ErrorStatus.xaml
    /// </summary>
    public partial class ErrorStatus : Window
    {
        private Ventilator_status selectedObject;

        public ErrorStatus()
        {
            InitializeComponent();
        }

        public ErrorStatus(Ventilator_status selectedObject)
        {
            InitializeComponent();
            this.selectedObject = selectedObject;
            lbl_ventilator_id.Content = "#" + selectedObject.Ventilator.Ventilator_id;
            lbl_address.Content = selectedObject.Ventilator.Address + ".";
            lbl_max_temperatur.Content = selectedObject.Ventilator.SAP.Celcius + "C";
            lbl_max_amps.Content = selectedObject.Ventilator.SAP.Amps + "A";
            lbl_max_hertz.Content = selectedObject.Ventilator.SAP.Hertz+ "Hz";
            lbl_max_kwh.Content = selectedObject.Ventilator.SAP.kWh + "kWh";
            lbl_current_temperatur.Content = selectedObject.Celcius;
            lbl_current_amps.Content = selectedObject.Amps;
            lbl_current_hertz.Content = selectedObject.Hertz;
            lbl_current_kwh.Content = selectedObject.kWh;
        }

        private void btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
