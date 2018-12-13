using HalloDaten.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HalloDaten
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = Settings.Default.MeinFarbe;

            this.Left = Settings.Default.PosX;
            this.Top = Settings.Default.PosY;

            this.Closed += (s, e) =>
            {
                Settings.Default.PosX = (int)this.Left;
                Settings.Default.PosY = (int)this.Top;
                Settings.Default.Save();
            };
        }

        private void DemoDatenLaden(object sender, RoutedEventArgs e)
        {
            var h1 = new Hersteller() { Name = "Baudi", Land = "Bayern" };
            var h2 = new Hersteller() { Name = "Afro Romeo", Land = "Takatukaland" };

            List<Auto> autos = new List<Auto>();
            for (int i = 0; i < 50; i++)
            {
                var a = new Auto()
                {
                    Id = i,
                    Baujahr = DateTime.Now.AddYears(10).AddDays(i * 47)
                };
                if (i % 2 == 0)
                {
                    a.Hersteller = h1;
                    a.Modell = $"B{i:00}";
                }
                else
                {
                    a.Hersteller = h2;
                    a.Modell = $"Brummm {i:0000}";
                }
                autos.Add(a);
            }
            lb1.ItemsSource = autos;
            cb1.ItemsSource = new[] { h1, h2 };
        }

        private void Speichern(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog() { Filter = "Autodatei|*.xml|Der ganze Mist|*.*" };

            if (dlg.ShowDialog().Value)
            {
                using (var writer = new StreamWriter(dlg.FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Auto>));
                    serial.Serialize(writer, lb1.ItemsSource);
                }
            }
        }
    }
}
