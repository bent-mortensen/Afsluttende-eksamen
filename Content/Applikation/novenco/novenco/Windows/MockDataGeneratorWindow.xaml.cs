using novenco.Database;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace novenco
{
    /// <summary>
    /// Interaction logic for MockDataGeneratorWindow.xaml
    /// </summary>
    public partial class MockDataGeneratorWindow : Window
    {
        Random rnd = new Random();
        Ventilator_status newVentilator_status;
        Ventilator newVentilator;

        public MockDataGeneratorWindow()
        {
            InitializeComponent();
            SetupEvent();            
        }

        private void GenerateValidStatus(object sender, RoutedEventArgs e)
        {
            CreateNewVentilatorAndNewVentilatorStatus();

            newVentilator.Ventilator_id = rnd.Next(1, 3);
            newVentilator_status.Ventilator = newVentilator;
            newVentilator_status.Celcius = rnd.Next(59);
            newVentilator_status.Hertz = rnd.Next(4);
            newVentilator_status.kWh = rnd.Next(4);
            newVentilator_status.Amps = rnd.Next(2);

            GenerateStatus(newVentilator_status);
        }

        private void CreateNewVentilatorAndNewVentilatorStatus()
        {
            newVentilator_status = new Ventilator_status();
            newVentilator = new Ventilator();
        }

        private void GenerateErrorStatus(object sender, RoutedEventArgs e)
        {
            CreateNewVentilatorAndNewVentilatorStatus();

            newVentilator.Ventilator_id = rnd.Next(1, 3);
            newVentilator_status.Ventilator = newVentilator;
            newVentilator_status.Celcius = rnd.Next(61, 100);
            newVentilator_status.Hertz = rnd.Next(6, 20);
            newVentilator_status.kWh = rnd.Next(4, 7);
            newVentilator_status.Amps = rnd.Next(2);

            GenerateStatus(newVentilator_status);
        }

        // Timer til event
        System.Timers.Timer timer = new System.Timers.Timer();
        private void SetupEvent()
        {
            timer.Interval = 10000; //In milliseconds here
            timer.AutoReset = true; //Stops it from repeating
            timer.Elapsed += new ElapsedEventHandler(GenerateStatusAndSend);
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void GenerateStatusAndSend(object sender, ElapsedEventArgs e)
        {
            CreateNewVentilatorAndNewVentilatorStatus();

            newVentilator.Ventilator_id = rnd.Next(1, 3);
            newVentilator_status.Ventilator = newVentilator;
            newVentilator_status.Celcius = rnd.Next(0, 100);
            newVentilator_status.Hertz = rnd.Next(0, 20);
            newVentilator_status.kWh = rnd.Next(0, 20);
            newVentilator_status.Amps = rnd.Next(0, 7);

            GenerateStatus(newVentilator_status);
        }

        private void GenerateStatus(Ventilator_status _newVentilator_status)
        {
            DB.StoreGeneratedStatus(_newVentilator_status);
        }
    }
}
