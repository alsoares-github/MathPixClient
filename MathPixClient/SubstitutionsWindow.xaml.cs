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

            SubsGroup.ItemsSource = app.Cfg.GroupNames;
            SubsGroup.SelectedIndex = app.Cfg.SelectedGroup;
            

            Substitutions.ItemsSource = app.Cfg.ActiveSubstitutions;
            Substitutions.CanUserAddRows = true;
            Substitutions.CanUserDeleteRows = true;
        }

        private void OnSubsGroupSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App app = Application.Current as App;

            app.Cfg.SetActiveGroup(SubsGroup.SelectedIndex);
            Substitutions.ItemsSource = app.Cfg.ActiveSubstitutions;
        }
    }
}
