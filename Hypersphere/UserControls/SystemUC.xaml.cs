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
using Hypersphere.ScreenshotArea;

namespace Hypersphere.UserControls
{
    public partial class SystemUC : UserControl
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private ScreenshotWindow _screenshotWindow;
        private ImageSaveFileDialog _selectedFolder;

        private Image _image;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public SystemUC(ScreenshotWindow sw)
        {
            InitializeComponent();

            _screenshotWindow = sw;
            _selectedFolder = new ImageSaveFileDialog();
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers
        private void closeImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_screenshotWindow != null)
            {
                _screenshotWindow.Close();
            }            
        }
        private void copyImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/copy_32x32_enabled.png"));
        }
        private void copyImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/copy_32x32_disabled.png"));
        }
        private void saveImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/save_32x32_enabled.png"));
        }
        private void saveImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/save_32x32_disabled.png"));
        }
        private void closeImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/close_32x32_enabled.png"));
        }
        private void closeImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/close_32x32_disabled.png"));
        }
        #endregion Event_handlers

        private void copyImage_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void saveImage_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //_selectedFolder.SaveImageToFolder();
        }
    }
}
