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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HalloWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Background = new SolidColorBrush(Colors.BurlyWood);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hallo " + tb1.Text); //uncool
            //MessageBox.Show(string.Format("Hallo {0} {1:D} {2:C}", tb1.Text, DateTime.Now, 5 + 6)); //mäßig cool

            //MessageBox.Show($"Hallo {tb1.Text} {DateTime.Now:D} {5 + 7:C}"); //cool (string interpolation)
            sp.Children.Add(new Label() { Content = "Hallo" });
        }

        private void LadeDaten(object sender, RoutedEventArgs e)
        {

        }
    }
}
