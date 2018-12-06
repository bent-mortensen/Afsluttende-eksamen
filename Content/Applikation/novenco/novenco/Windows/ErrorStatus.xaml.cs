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
        private Ventilator_status ventilatorStatus;
        private Employee employee;
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

        public ErrorStatus(Ventilator_status _selectedVentilatorStatus, Employee _selectedEmployee)
        {
            InitializeComponent();
            GetErrorType();
            ventilatorStatus = _selectedVentilatorStatus;
            employee = _selectedEmployee;

            lbl_ventilator_id.Content = "#" + _selectedVentilatorStatus.Ventilator.Ventilator_id;
            lbl_address.Content = _selectedVentilatorStatus.Ventilator.Address + ".";
            lbl_max_temperatur.Content = _selectedVentilatorStatus.Ventilator.SAP.Celcius + "C";
            lbl_max_amps.Content = _selectedVentilatorStatus.Ventilator.SAP.Amps + "A";
            lbl_max_hertz.Content = _selectedVentilatorStatus.Ventilator.SAP.Hertz + "Hz";
            lbl_max_kwh.Content = _selectedVentilatorStatus.Ventilator.SAP.kWh + "kWh";
            lbl_current_temperatur.Content = _selectedVentilatorStatus.Celcius;
            lbl_current_amps.Content = _selectedVentilatorStatus.Amps;
            lbl_current_hertz.Content = _selectedVentilatorStatus.Hertz;
            lbl_current_kwh.Content = _selectedVentilatorStatus.kWh;

            SetColorSeverity(rec_temperatur, _selectedVentilatorStatus.Celcius, _selectedVentilatorStatus.Ventilator.SAP.Celcius);
            SetColorSeverity(rec_amps, _selectedVentilatorStatus.Amps, _selectedVentilatorStatus.Ventilator.SAP.Amps);
            SetColorSeverity(rec_hertz, _selectedVentilatorStatus.Hertz, _selectedVentilatorStatus.Ventilator.SAP.Hertz);
            SetColorSeverity(rec_kwh, _selectedVentilatorStatus.kWh, _selectedVentilatorStatus.Ventilator.SAP.kWh);
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

        // knapper
        private void Correct_Temperature_Click(object sender, EventArgs e)
        {
            OpenCorrectError(ventilatorStatus.Celcius, ventilatorStatus.Ventilator.SAP.Celcius, errorTypeList[1]);
        }

        private void Correct_Amps_Click(object sender, EventArgs e)
        {
            OpenCorrectError(ventilatorStatus.Amps, ventilatorStatus.Ventilator.SAP.Amps, errorTypeList[2]);
        }

        private void Correct_Hertz_Click(object sender, EventArgs e)
        {
            OpenCorrectError(ventilatorStatus.Hertz, ventilatorStatus.Ventilator.SAP.Hertz, errorTypeList[0]);
        }

        private void Correct_kWh_Click(object sender, EventArgs e)
        {
            OpenCorrectError(ventilatorStatus.kWh, ventilatorStatus.Ventilator.SAP.kWh, errorTypeList[3]);
        }

        CorrectError window_CorrectError;
        private void OpenCorrectError(int _currentValue, int _sapValue, Error_type _errorType)
        {
            window_CorrectError = new CorrectError(ventilatorStatus, employee, _errorType);
            
            // Bestemmer om en status ligger over eller under Sap værdierne.
            if (_currentValue > _sapValue)
            {
                window_CorrectError.Show();
            }
        }


        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
