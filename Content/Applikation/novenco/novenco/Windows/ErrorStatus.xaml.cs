using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using novenco.Database;

namespace novenco.Windows
{
    /// <summary>
    /// Interaction logic for ErrorStatus.xaml
    /// </summary>
    public partial class ErrorStatus : Window
    {
        private Ventilator_status selectedVentilatorStatus;
        private Employee selectedEmployee;
        private ObservableCollection<Error_type> errorTypeList;

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
            GetErrorType();
        }

        public ErrorStatus(Ventilator_status selectedVentilatorStatus, Employee selectedEmployee)
        {
            InitializeComponent();
            GetErrorType();
            this.selectedVentilatorStatus = selectedVentilatorStatus;

            lbl_ventilator_id.Content = "#" + selectedVentilatorStatus.Ventilator.Ventilator_id;
            lbl_address.Content = selectedVentilatorStatus.Ventilator.Address + ".";
            lbl_max_temperatur.Content = selectedVentilatorStatus.Ventilator.SAP.Celcius + "C";
            lbl_max_amps.Content = selectedVentilatorStatus.Ventilator.SAP.Amps + "A";
            lbl_max_hertz.Content = selectedVentilatorStatus.Ventilator.SAP.Hertz + "Hz";
            lbl_max_kwh.Content = selectedVentilatorStatus.Ventilator.SAP.kWh + "kWh";
            lbl_current_temperatur.Content = selectedVentilatorStatus.Celcius;
            lbl_current_amps.Content = selectedVentilatorStatus.Amps;
            lbl_current_hertz.Content = selectedVentilatorStatus.Hertz;
            lbl_current_kwh.Content = selectedVentilatorStatus.kWh;

            SetColorSeverity(rec_temperatur, selectedVentilatorStatus.Celcius, selectedVentilatorStatus.Ventilator.SAP.Celcius);
            SetColorSeverity(rec_amps, selectedVentilatorStatus.Amps, selectedVentilatorStatus.Ventilator.SAP.Amps);
            SetColorSeverity(rec_hertz, selectedVentilatorStatus.Hertz, selectedVentilatorStatus.Ventilator.SAP.Hertz);
            SetColorSeverity(rec_kwh, selectedVentilatorStatus.kWh, selectedVentilatorStatus.Ventilator.SAP.kWh);
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

        // Hent error typer.
        private void GetErrorType()
        {
            errorTypeList = DB.GetErrorTypes();
        }

        private void Correct_Temperature_Click(object sender, EventArgs e)
        {
            OpenCorrectError(errorTypeList[1]);
        }

        private void Correct_Amps_Click(object sender, EventArgs e)
        {
            OpenCorrectError(errorTypeList[2]);
        }

        private void Correct_Hertz_Click(object sender, EventArgs e)
        {
            OpenCorrectError(errorTypeList[0]);
        }

        private void Correct_kWh_Click(object sender, EventArgs e)
        {
            OpenCorrectError(errorTypeList[3]);
        }

        private void OpenCorrectError(Error_type _errorType)
        {
            CorrectError window = new CorrectError(selectedVentilatorStatus, selectedEmployee, _errorType);
            window.Show();
        }


        private void Btn_Close(object sender, RoutedEventArgs e)
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


        
            //foreach (var item in _failedVentStatus)
            //{
            //    if (item.Celcius > item.Ventilator.SAP.Celcius)
            //    {

            //    }
            //    if (item.Hertz > item.Ventilator.SAP.Hertz)
            //    {

            //    }
            //    if (item.kWh > item.Ventilator.SAP.kWh)
            //    {

            //    }
            //    if (item.Amps > item.Ventilator.SAP.Amps)
            //    {

            //    }
            //}
                // INSERT Error type
                //int error_celcius = 2;
                //int error_hertz = 1;
                //int error_kwh = 4;
                //int error_amps = 3;
                //DB.SetVentilationError(error_celcius, item.Ventilator_status_id);



        //#endregion
    }
}
