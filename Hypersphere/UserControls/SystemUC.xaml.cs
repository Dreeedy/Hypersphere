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

namespace Hypersphere.UserControls
{
    /// <summary>
    /// Interaction logic for SystemUC.xaml
    /// </summary>
    public partial class SystemUC : UserControl
    {
        public SystemUC()
        {
            InitializeComponent();
        }

        private void closeImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
