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
    /// Interaction logic for PaintUC.xaml
    /// </summary>
    public partial class PaintUC : UserControl
    {
        bool isPencilDraw;

        public PaintUC()
        {
            InitializeComponent();
        }

        public bool GetisPencilDraw()
        {
            return isPencilDraw;
        }

        private void pencilImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isPencilDraw)
            {
                // "/Resource/Plugs/plug_1_32x32.png" pack://application:,,,/AssemblyName;component/Resources/logo.png"
                Image image = sender as Image;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Plugs/plug_1_32x32.png"));
                isPencilDraw = false;
            }
            else
            {
                // "/Resource/Plugs/plug_active_32x32.png"
                Image image = sender as Image;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Plugs/plug_active_32x32.png"));
                isPencilDraw = true;
            }
        }
    }
}
