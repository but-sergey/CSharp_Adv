using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HelloWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            //foreach(var arg in e.Args)
            //    MessageBox.Show($"Argument = {arg}");
            //for (var i = 0; i < e.Args.Length; i++)
            //    MessageBox.Show($"Argument #{i} = {e.Args[i]}");

            wnd.Title = "Hello, WPF!";
            wnd.Show();
        }
    }
}
