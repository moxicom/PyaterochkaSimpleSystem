using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PyaterochkaSimpleSystem.ViewModels
{
    internal class StartWindowVM : ViewModelBase
    {
        public ICommand OpenMainWindowCommand => new RelayCommand(OpenMainWindow);

        public void OpenMainWindow()
        {
            var currentWindow = Application.Current.MainWindow;
            
            if (currentWindow == null)
            {
                return;
            }
                
            var mainWindow = new MainWindow()
            {
                DataContext = new MainWindowVM()
            };

            currentWindow.Dispatcher.Invoke(() =>
            {
                currentWindow.Close();
            });

            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

    }
}
