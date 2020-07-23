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
    /// Interaction logic for SubstitutionsWindow.xaml
    /// </summary>
    public partial class SubstitutionsWindow : Window
    {
        public SubstitutionsWindow()
        {
            InitializeComponent();

            App app = Application.Current as App;

            SubsGroup.ItemsSource = app.GroupNames;
        }
    }
}
