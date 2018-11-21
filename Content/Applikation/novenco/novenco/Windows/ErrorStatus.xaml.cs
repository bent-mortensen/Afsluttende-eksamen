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

        // Fill blue       #FFDAE8FC
        string fillBlue = "#FFDAE8FC";
        // Stroke blue     #FF7999C7
        string strokeBlue = "#FF7999C7";

        // Fill green      #FFD5E8D4
        string fillGreen = "#FFD5E8D4";
        // Stroke green    #FF9CC489
        string strokeGreen = "#FF9CC489";

        // Fill yellow     #FFFFE6CC
        string fillYellow = "#FFFFE6CC";
        // Stroke yellow   #FFDCA419
        string strokeYellow = "#FFDCA419";

        // Fill red        #FFF8CECC
        string fillRed = "#FFF8CECC";
        // Stroke red      #FFC26662
        string strokeRed = "#FFC26662";

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
            lbl_max_hertz.Content = selectedObject.Ventilator.SAP.Hertz + "Hz";
            lbl_max_kwh.Content = selectedObject.Ventilator.SAP.kWh + "kWh";
            lbl_current_temperatur.Content = selectedObject.Celcius;
            lbl_current_amps.Content = selectedObject.Amps;
            lbl_current_hertz.Content = selectedObject.Hertz;
            lbl_current_kwh.Content = selectedObject.kWh;

            SetColorSeverity(rec_temperatur, selectedObject.Celcius, selectedObject.Ventilator.SAP.Celcius);
            SetColorSeverity(rec_amps, selectedObject.Amps, selectedObject.Ventilator.SAP.Amps);
            SetColorSeverity(rec_hertz, selectedObject.Hertz, selectedObject.Ventilator.SAP.Hertz);
            SetColorSeverity(rec_kwh, selectedObject.kWh, selectedObject.Ventilator.SAP.kWh);
        }

        private void SetColorSeverity(Rectangle _rec, int _currentValue, int _sapValue)
        {
            // Brush converter
            var bc = new BrushConverter();
            Rectangle rec = _rec;

            int currentValue = _currentValue;
            int sapValue = _sapValue;

            int severityState = currentValue - sapValue;
            // -value
            if (currentValue > severityState)
            {
                rec.Fill = (Brush)bc.ConvertFrom(fillGreen);
                rec.Stroke = (Brush)bc.ConvertFrom(strokeGreen);
            }
            // -+value
            if (currentValue <= sapValue - 1 && currentValue >= sapValue)
            {
                rec.Fill = (Brush)bc.ConvertFrom(fillBlue);
                rec.Stroke = (Brush)bc.ConvertFrom(strokeBlue);
            }
            // +value
            if (currentValue >= sapValue + 1 && currentValue <= sapValue + 2)
            {
                rec.Fill = (Brush)bc.ConvertFrom(fillYellow);
                rec.Stroke = (Brush)bc.ConvertFrom(strokeYellow);
            }
            // ++value
            if (currentValue > sapValue + 2)
            {
                rec.Fill = (Brush)bc.ConvertFrom(fillRed);
                rec.Stroke = (Brush)bc.ConvertFrom(strokeRed);
            }

        }

        private void btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //#region NotifyPropertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //#endregion
    }
}
