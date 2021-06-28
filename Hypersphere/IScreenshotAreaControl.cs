using System.Windows;
using System.Windows.Controls;
using Hypersphere.UserControls;

namespace Hypersphere
{
    interface IScreenshotAreaControl
    {
        public bool IsDoExistAndIsPencilDraw();
        public void IsDoExistAndHide();
        public void CreateAndAddOrShow(FrameworkElement screenshotAreaGrid, Canvas parent);
    }
}
