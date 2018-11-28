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

namespace novenco
{
    /// <summary>
    /// Interaction logic for MockDataGeneratorWindow.xaml
    /// </summary>
    public partial class MockDataGeneratorWindow : Window
    {
        Random rnd = new Random();
        int id = 1;
        int Celcius = 0;
        int Hertz = 0;
        int kWh = 0;
        int Amps = 0;
        public MockDataGeneratorWindow()
        {
            InitializeComponent();
            SetupEvent();            
        }


        private void GenerateValidStatus(object sender, RoutedEventArgs e)
        {
            Celcius = rnd.Next(59);
            Hertz = rnd.Next(4);
            kWh = rnd.Next(4);
            Amps = rnd.Next(2);

            GenerateStatus(Celcius, Hertz, kWh, Amps, id);
        }

        private void GenerateErrorStatus(object sender, RoutedEventArgs e)
        {
            Celcius = rnd.Next(61, 100);
            Hertz = rnd.Next(6, 20);
            kWh = rnd.Next(6, 20);
            Amps = rnd.Next(4, 7);
            GenerateStatus(Celcius, Hertz, kWh, Amps, id);
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
            Celcius = rnd.Next(0, 100);
            Hertz = rnd.Next(0, 20);
            kWh = rnd.Next(0, 20);
            Amps = rnd.Next(0, 7);
            GenerateStatus(Celcius, Hertz, kWh, Amps, id);
        }

        private void GenerateStatus(int _Celcius, int _Hertz, int _kWh, int _Amps, int _id)
        {
            DB.StoreGeneratedStatus(_Celcius, _Hertz, _kWh, _Amps, _id);
        }

    }
}
