using novenco.Classes;
using novenco.Database;
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

namespace novenco.Windows
{
    /// <summary>
    /// Interaction logic for UpdateSAPValues.xaml
    /// </summary>
    public partial class UpdateSAPValues : Window
    {
        private int service_agreement_package_id;
        private Service_agreement_package sap;

        int newCelcius;
        int newHertz;
        int newkWh;
        int newAmps;

        public UpdateSAPValues()
        {
            InitializeComponent();
        }

        public UpdateSAPValues(int _service_agreement_package_id)
        {
            InitializeComponent();
            this.service_agreement_package_id = _service_agreement_package_id;

            sap = DB.GetServiceAgreementPackage(service_agreement_package_id);
            lbl_celcius.Content = sap.Celcius + " C";
            lbl_hertz.Content = sap.Hertz + " Hz";
            lbl_kwh.Content = sap.kWh + " kWh";
            lbl_amps.Content = sap.Amps + " A";
        }

        private void btn_SetNewSAPValues(object sender, RoutedEventArgs e)
        {
            newCelcius = Convert.ToInt32(txt_box_celcius.Text);
            newHertz = Convert.ToInt32(txt_box_hertz.Text);
            newkWh = Convert.ToInt32(txt_box_kwh.Text);
            newAmps = Convert.ToInt32(txt_box_amps.Text);

            // kun numerisk 

            if (MessageBox.Show("Dette vil overskrive service agreement package " + sap.Name + ", med nye værdier", "Advarsel", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                DB.UpdateServiceAgreementPackage(service_agreement_package_id, newCelcius, newHertz, newkWh, newAmps);
                MessageBox.Show("test");
                Close();
            }
            else
            {
                Close();
            }
        }
    }
}
