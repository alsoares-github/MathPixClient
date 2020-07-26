using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            SubsGroup.ItemsSource = app.Cfg.Groups;
            SubsGroup.DisplayMemberPath = "Name";
            SubsGroup.IsSynchronizedWithCurrentItem = true;
            Binding bd1 = new Binding();
            bd1.Source = app.Cfg;
            bd1.Path = new PropertyPath("SelectedGroup");
            SubsGroup.SetBinding(ComboBox.SelectedValueProperty, bd1);

            Binding b = new Binding();
            b.Source = app.Cfg.Groups;
            b.Path = new PropertyPath("Substitutions");
            Substitutions.SetBinding(DataGrid.ItemsSourceProperty, b);   
            Substitutions.CanUserAddRows = true;
            Substitutions.CanUserDeleteRows = true;
        }
    }
}
