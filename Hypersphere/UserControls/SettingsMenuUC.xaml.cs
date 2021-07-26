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
using System.Windows.Shapes;

namespace Hypersphere.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsMenuUC.xaml
    /// </summary>
    public partial class SettingsMenuUC : Window
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        // AutomaticProgramStart
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public SettingsMenuUC()
        {
            InitializeComponent();
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers
        private void Window_Closed(object sender, EventArgs e)
        {

        }
        #endregion Event_handlers

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (AutomaticProgramStart.GetAutomaticProgramStartStatus() == true)
            {
                automaticProgramStartCheckBox.IsChecked = true;
            }
            else
            {
                automaticProgramStartCheckBox.IsChecked = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (automaticProgramStartCheckBox.IsChecked == true)
            {
                AutomaticProgramStart.EnableAutomaticProgramStartValue();
                AutomaticProgramStart.SwithAutomaticProgramStart();
            }
            else
            {
                AutomaticProgramStart.DisableAutomaticProgramStartValue();
                AutomaticProgramStart.SwithAutomaticProgramStart();
            }
        }
    }
}
