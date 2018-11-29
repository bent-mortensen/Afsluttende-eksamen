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
            // tjek status ved close(); om der er oprettet fejlrettelser for resten af fejlstatussen med dette tidsstempel og valider den efterfølgende så den forsvinder fra skærmen alle skal valideres.
            var result = MessageBox.Show("Rapporten vil nu blive gemt", "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
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
                // gem ventilator error og retuner identity
                int ventilator_error_id = DB.StoreVentilationError(errorType.Error_type_id, status.Ventilator_status_id);

                // Obsolete. Erstattet med SELECT SCOPE_IDENTITY(); 
                // int ventilator_error_id = DB.GetVentilationErrorId(errorType.Error_type_id, status.Ventilator_status_id);
                
                // gem error correction report
                DB.StoreErrorCorrectionReport(error_description, error_correction_description, correction_date, sap_celcius, sap_hertz, sap_kwh, sap_amps, sparePartListNumber, employeeid, ventilator_error_id);
                Close();
            }
            else
            {

            }


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
