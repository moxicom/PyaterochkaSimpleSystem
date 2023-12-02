using dotenv.net;
using PyaterochkaSimpleSystem.ViewModels;
using PyaterochkaSimpleSystem.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace PyaterochkaSimpleSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            var window = new StartWindow()
            {
                DataContext = new StartWindowVM()
            };
            Current.MainWindow = window;
            Run(window);
        }
    }
}
