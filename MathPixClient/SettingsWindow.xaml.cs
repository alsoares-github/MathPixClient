using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MathPixClient
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            MainWindow win = Application.Current.MainWindow as MainWindow;
            Top = win.Top + 50;
            Left = win.Left;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Settings.Default.Save();
            MainWindow win = Application.Current.MainWindow as MainWindow;
            win.RefreshCredentials();
        }
    }
}
