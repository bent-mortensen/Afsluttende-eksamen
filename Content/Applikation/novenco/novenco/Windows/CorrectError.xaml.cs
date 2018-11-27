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
    /// Interaction logic for CorrectError.xaml
    /// </summary>
    public partial class CorrectError : Window
    {
        public CorrectError(Ventilator_status _status, Employee _employee, Error_type _errorType)
        {
            InitializeComponent();
            lbl_error_header.Content = "Ret fejl - " + _errorType.Type_name;
        }

        private void AddRemoveSparePart(object sender, RoutedEventArgs e)
        {
            // nyt vindue
        }
    }
}
