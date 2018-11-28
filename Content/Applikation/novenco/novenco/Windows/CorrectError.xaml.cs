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
    /// Interaction logic for CorrectError.xaml
    /// </summary>
    public partial class CorrectError : Window
    {
        List<int> Spareparts = new List<int>();
        Ventilator_status status;
        Employee employee;
        Error_type errorType;
        ObservableCollection<Spare_part> sparePartList = new ObservableCollection<Spare_part>();

        public CorrectError(Ventilator_status _status, Employee _employee, Error_type _errorType)
        {
            InitializeComponent();
            status = _status;
            employee = _employee;
            errorType = _errorType;

            // Vis fejl type der skal rette nu.
            lbl_error_header.Content = "Ret fejl - " + _errorType.Type_name;
        }

        private void Btn_AddRemoveSparePart(object sender, RoutedEventArgs e)
        {
            AddRemoveSparePart window = new AddRemoveSparePart(sparePartList);
            window.ShowDialog();
            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
                // gem en liste med spare parts.
                sparePartList = window.ChoosenSpareParts;              
                //MessageBox.Show("User clicked OK");
            }
            else
            {
                MessageBox.Show("User clicked Cancel");
            }            
        }

        private void Btn_CorrectError(object sender, RoutedEventArgs e)
        {
            string error_description = tb_error_description.Text;
            string error_correction_description = tb_error_correction_description.Text;
            DateTime correction_date = DateTime.Now;
            int sap_celcius = status.Ventilator.SAP.Celcius;
            int sap_hertz = status.Ventilator.SAP.Hertz;
            int sap_kwh = status.Ventilator.SAP.kWh;
            int sap_amps = status.Ventilator.SAP.Amps;
            int sparePartListNumber = SaveSparePartList();
            int employeeid = employee.Employee_id;
            DB.StoreVentilationError(errorType.Error_type_id, status.Ventilator_status_id);
            int ventilator_error_id = DB.GetVentilationErrorId(errorType.Error_type_id, status.Ventilator_status_id);

            // gem error report
            // INSERT INTO Error_correction_report(Error_description, Error_correction_description, Correction_date, Sap_celcius, Sap_amps, Sap_hertz, Sap_kwh, FK_Ventilator_error_id, FK_Employee_id, FK_Spare_part_list_id) VALUES('beskrivelse af fejlen', 'beskrivelse af tiltag for at rette fejlen', CURRENT_TIMESTAMP, 0, 0, 0, 0, 1, 1, 1);
            
            // tjek status ved close(); om der er oprettet fejlrettelser for resten af fejlstatussen med dette tidsstempel og valider den efterfølgende så den forsvinder fra skærmen alle skal valideres.
            

            string errorDescription = tb_error_description.Text;
            string errorCorrectionDescription = tb_error_correction_description.Text;
            string correctionDate = DateTime.Now.ToString();
            MessageBox.Show(correctionDate);
            MessageBox.Show(errorDescription);
            MessageBox.Show(errorCorrectionDescription);
            MessageBox.Show("Employee id: " + employee.Employee_id);
            MessageBox.Show("Ventilator id: " + status.Ventilator.Ventilator_id);
            MessageBox.Show("Error type id: " + errorType.Error_type_id);
        }

        private int SaveSparePartList()
        {
            int sparePartListNumber = DB.GetMaxValueFromSparePartList();
            sparePartListNumber++;

            foreach (var item in sparePartList)
            {
                DB.StoreSparePartListItem(sparePartListNumber, item.Spare_part_id);
            }
            return sparePartListNumber;
        }
    }
}
