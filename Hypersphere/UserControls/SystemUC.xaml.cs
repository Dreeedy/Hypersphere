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
        private Image image;

        public SystemUC()
        {
            InitializeComponent();
        }

        private void closeImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void copyImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/copy_32x32_enabled.png"));
        }

        private void copyImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/copy_32x32_disabled.png"));
        }

        private void saveImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/save_32x32_enabled.png"));
        }

        private void saveImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/save_32x32_disabled.png"));
        }

        private void closeImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/close_32x32_enabled.png"));
        }

        private void closeImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/close_32x32_disabled.png"));
        }        
    }
}
