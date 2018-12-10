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
            service_agreement_package_id = _service_agreement_package_id;

            sap = DB.GetServiceAgreementPackage(service_agreement_package_id);

            lbl_package.Content = "Service aggrement package - " + sap.Name;
            lbl_package_description.Content = sap.Description;
            lbl_celcius.Content = sap.Celcius + " C";
            lbl_hertz.Content = sap.Hertz + " Hz";
            lbl_kwh.Content = sap.kWh + " kWh";
            lbl_amps.Content = sap.Amps + " A";
        }

        // Dette event tjekker at det kun er numeriske værdier der bliver intastet 
        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var text = s.Text.Insert(s.SelectionStart, e.Text);
            double d;
            // If parsing is successful, set Handled to false
            e.Handled = !double.TryParse(text, out d);
        }
        // 
        private void btn_SetNewSAPValues(object sender, RoutedEventArgs e)
        {
            ReturnOldValueIfInputIsZero();

            if (sap.NewCelcius(newCelcius) && sap.NewHertz(newHertz) && sap.NewKWH(newkWh) && sap.NewAmps(newAmps))
            {

                if (MessageBox.Show("Dette vil overskrive service agreement package " + sap.Name + ", med nye værdier", "Advarsel", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    DB.UpdateServiceAgreementPackage(service_agreement_package_id, newCelcius, newHertz, newkWh, newAmps);
                    MessageBox.Show("Service Agreement pakke opdateret!");
                    Close();
                }
                else
                {
                    Close();
                }

            }
            else
            {
                MessageBox.Show("Nye værdier er for høje/lave! Celcius: 1-300 - Hertz: 1-2000 - kWh: 1-200 - Amps: 1-50");
            }
        }

        private void ReturnOldValueIfInputIsZero()
        {
            if (txt_box_celcius.Text.Length == 0)
            {
                newCelcius = sap.Celcius;
            }
            else
            {
                newCelcius = Convert.ToInt32(txt_box_celcius.Text);
            }
            if (txt_box_hertz.Text.Length == 0)
            {
                newHertz = sap.Hertz;
            }
            else
            {
                newHertz = Convert.ToInt32(txt_box_hertz.Text);
            }
            if (txt_box_kwh.Text.Length == 0)
            {
                newkWh = sap.kWh;
            }
            else
            {
                newkWh = Convert.ToInt32(txt_box_kwh.Text);
            }
            if (txt_box_amps.Text.Length == 0)
            {
                newAmps = sap.Amps;
            }
            else
            {
                newAmps = Convert.ToInt32(txt_box_amps.Text);
            }
        }
    }
}
