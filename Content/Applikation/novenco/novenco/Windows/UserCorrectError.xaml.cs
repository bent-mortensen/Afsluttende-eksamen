using novenco.Classes;
using novenco.Database;
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

namespace novenco.Windows
{
    /// <summary>
    /// Interaction logic for UserCorrectError.xaml
    /// </summary>
    public partial class UserCorrectError : Window
    {
        List<int> Spareparts = new List<int>();
        Ventilator ventilator;
        Employee employee;
        Error_type errorType;
        ObservableCollection<Spare_part> sparePartList = new ObservableCollection<Spare_part>();

        ObservableCollection<Ventilator> ventilators = new ObservableCollection<Ventilator>();
        ObservableCollection<Error_type> errorTypes = new ObservableCollection<Error_type>();

        public UserCorrectError(Employee _selectedEmployee)
        {
            InitializeComponent();
            employee = _selectedEmployee;

            // Fill lists


            // comboboxes dropdown 
            ventilators = DB.GetAllVentilators();
            cb_Ventilator_id.ItemsSource = ventilators;
            cb_Ventilator_id.DisplayMemberPath = "Ventilator_id";

            errorTypes = DB.GetErrorTypes();
            cb_Error_type.ItemsSource = errorTypes;
            cb_Error_type.DisplayMemberPath = "Type_name";

            // labels
            lbl_Sap_value.Content = "";

        }

        private void Btn_AddRemoveSparePart(object sender, RoutedEventArgs e)
        {
            AddRemoveSparePart window = new AddRemoveSparePart(sparePartList);
            window.ShowDialog();
            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
                //MessageBox.Show("User clicked OK");

                // gem en liste med spare parts.
                sparePartList = window.ChoosenSpareParts;
            }
            else
            {
                // Gør ingen ting
                //MessageBox.Show("User clicked Cancel");
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
                int errorType_id = errorType.Error_type_id;

                int sap_celcius = ventilator.SAP.Celcius;
                int sap_hertz = ventilator.SAP.Hertz;
                int sap_kwh = ventilator.SAP.kWh;
                int sap_amps = ventilator.SAP.Amps;

                int Celcius = GetMeasuredValue(errorType_id, "Celcius");
                int Hertz = GetMeasuredValue(errorType_id, "hertz");
                int kWh = GetMeasuredValue(errorType_id, "kwh");
                int Amps = GetMeasuredValue(errorType_id, "amps");

                // Gem Status og hent ID for status 
                int StatusID = DB.StoreGeneratedStatusAndScopeID(Celcius, Hertz, kWh, Amps, ventilator.Ventilator_id);

                int sparePartListNumber = SaveSparePartList();
                int employeeid = employee.Employee_id;

                // gem ventilator error og retuner identity
                int ventilator_error_id = DB.StoreVentilationError(errorType.Error_type_id, StatusID);

                // Obsolete. Erstattet med SELECT SCOPE_IDENTITY(); 
                //int ventilator_error_id = DB.GetVentilationErrorId(errorType.Error_type_id, status.Ventilator_status_id);

                // gem error correction report
                DB.StoreErrorCorrectionReport(error_description, error_correction_description, correction_date, sap_celcius, sap_hertz, sap_kwh, sap_amps, sparePartListNumber, employeeid, ventilator_error_id);
                Close();
            }
            else
            {

            }
        }

        private int GetMeasuredValue(int _errorType_id, string _type)
        {
            if (_type == "celcius" && _errorType_id == 2)
            {
                return Convert.ToInt32(tb_Measured_value.Text);
            }
            else if (_type == "amps" && _errorType_id == 3)
            {
                return Convert.ToInt32(tb_Measured_value.Text);
            }
            else if (_type == "kwh" && _errorType_id == 4)
            {
                return Convert.ToInt32(tb_Measured_value.Text);
            }
            else if (_type == "hertz" && _errorType_id == 1)
            {
                return Convert.ToInt32(tb_Measured_value.Text);
            }
            else
            {
                return 0;
            }

        }

        // Reservedels list redigering
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

        // Advarsel
        private void cb_Error_type_MouseOver(object sender, MouseEventArgs e)
        {
            Ventilator ventilator = (Ventilator)cb_Ventilator_id.SelectedItem;
            if (!(ventilator == null))
            {

            }
            else
            {
                MessageBox.Show("Vælg ventilator først!");
            }
        }

        // Combobox logik
        private void cb_Ventilator_id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ventilator = (Ventilator)cb_Ventilator_id.SelectedItem;

            cb_Error_type.ItemsSource = errorTypes;

            cb_Error_type_SelectionChanged(sender, e, true, 1);
        }
        private void cb_Error_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errorType = (Error_type)cb_Error_type.SelectedItem;

            SetlblValue();

        }
        private void cb_Error_type_SelectionChanged(object sender, SelectionChangedEventArgs e, bool test = false, int Index = 0)
        {
            if (test)
            {
                cb_Error_type.SelectedIndex = Index;
            }

            errorType = (Error_type)cb_Error_type.SelectedItem;

            SetlblValue();

        }
        private void SetlblValue()
        {
            if (errorType.Error_type_id == 1)
            {
                lbl_Sap_value.Content = ventilator.SAP.Hertz + " hz";

            }
            if (errorType.Error_type_id == 2)
            {
                lbl_Sap_value.Content = ventilator.SAP.Celcius + " C";

            }
            if (errorType.Error_type_id == 3)
            {
                lbl_Sap_value.Content = ventilator.SAP.Amps + " A";

            }
            if (errorType.Error_type_id == 4)
            {
                lbl_Sap_value.Content = ventilator.SAP.kWh + " kWh";

            }
        }
    }
}
