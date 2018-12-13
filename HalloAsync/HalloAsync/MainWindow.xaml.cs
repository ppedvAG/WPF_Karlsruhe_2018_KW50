using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OhneThreading(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(300);
                pb1.Value = i;
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;

            btn.IsEnabled = false;
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(30);
                    pb1.Dispatcher.Invoke((Action)delegate { pb1.Value = i; });
                }

                btn.Dispatcher.Invoke((Action)delegate { btn.IsEnabled = true; });
            });
        }

        CancellationTokenSource cts = null;

        private void StartTaskMitScheduler(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            btn.IsEnabled = false;

            cts = new CancellationTokenSource();

            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(30);
                    Task.Factory.StartNew(() => pb1.Value = i, cts.Token, TaskCreationOptions.None, ts);
                    if (cts.IsCancellationRequested)
                        break;
                }
            }).ContinueWith(t => btn.IsEnabled = true, CancellationToken.None, TaskContinuationOptions.None, ts);

        }

        private void Abort(object sender, RoutedEventArgs e)
        {
            //if (cts != null)
            cts?.Cancel();
        }

        private async void StartAsync(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            btn.IsEnabled = false;
            cts = new CancellationTokenSource();

            for (int i = 0; i <= 100; i++)
            {
                //await Task.Delay(30, CancellationToken.None);
                await Task.Factory.StartNew(() => Thread.Sleep(30));
                pb1.Value = i;
                if (cts.IsCancellationRequested)
                    break;
            }
            btn.IsEnabled = !false;

        }


        private long GetValueByAlteUndLangsameFunktion()
        {
            Thread.Sleep(3000);
            return 74387438734;
        }

        private Task<long> GetValueByAlteUndLangsameFunktionAsync()
        {
            return Task.Factory.StartNew(() => GetValueByAlteUndLangsameFunktion());
        }

        private async void StartAlt(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(GetValueByAlteUndLangsameFunktion().ToString());
            //MessageBox.Show((await GetValueByAlteUndLangsameFunktionAsync()).ToString());
            long result = await GetValueByAlteUndLangsameFunktionAsync();
            MessageBox.Show($"Result: {result}");
        }

        private async void DbGetNames(object sender, RoutedEventArgs e)
        {
            string conString = "Server=.;Database=Northwind;Trusted_Connection=true";
            using (var con = new SqlConnection(conString))
            {
                await con.OpenAsync();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Employees;WAITFOR DELAY '00:00:02';";
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        List<string> names = new List<string>();
                        while (reader.Read())
                            names.Add($"{reader["FirstName"]} {reader["LastName"]}");
                        MessageBox.Show(string.Join(Environment.NewLine, names));
                    }
                }
            }
        }
    }
}
