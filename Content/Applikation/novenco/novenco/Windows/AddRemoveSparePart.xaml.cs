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
    /// Interaction logic for AddRemoveSparePart.xaml
    /// </summary>
    public partial class AddRemoveSparePart : Window
    {
        ObservableCollection<Spare_part> spareParts = new ObservableCollection<Spare_part>();
        public ObservableCollection<Spare_part> ChoosenSpareParts = new ObservableCollection<Spare_part>();
        Spare_part sparepart = new Spare_part();


        public AddRemoveSparePart(ObservableCollection<Spare_part> _choosenSparePartList)
        {
            InitializeComponent();

            ChoosenSpareParts = _choosenSparePartList;

            // Henter alle reservedele fra databasen.
            spareParts = DB.GetSpareParts();

            // reservedele man kan vælge imellem.
            lb_all_spare_parts.ItemsSource = spareParts;
            lb_all_spare_parts.DisplayMemberPath = sparepart.GetPathSparePartName();
            
            // valgte reservedele.
            lb_choosen_spare_parts.ItemsSource = ChoosenSpareParts;
            lb_choosen_spare_parts.DisplayMemberPath = sparepart.GetPathSparePartName();
        }

        private void Btn_Click_Ok(object sender, RoutedEventArgs e)
        {
            // Acceptere valg og lukker vinduet! Close(); ikke nødvendig.            
            DialogResult = true;
        }

        private void Btn_RemoveSparePartFromChoosenList(object sender, RoutedEventArgs e)
        {
            if (lb_choosen_spare_parts.SelectedItem != null)
            {
                ChoosenSpareParts.Remove((Spare_part)lb_choosen_spare_parts.SelectedItem);               
            }
        }

        private void Btn_AddSparePartToList(object sender, RoutedEventArgs e)
        {
            if (lb_all_spare_parts.SelectedItem != null)
            {
                ChoosenSpareParts.Add((Spare_part)lb_all_spare_parts.SelectedItem);
            }             
        }

    }
}
