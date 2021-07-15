using System.Windows;
using System.Windows.Controls;
using Hypersphere.UserControls;

namespace Hypersphere
{
    interface IScreenshotAreaControl
    {
        public bool IsDoExistAndIsAnyBrushDraw();
        public bool IsDoExistAndIsPencilDraw();
        public bool IsDoExistAndIsLineDraw();
        public bool IsDoExistAndIsArrowDraw();
        public bool IsDoExistAndIsRectangleDraw();
        public void IsDoExistAndHide();
        public void CreateAndAddOrShow(FrameworkElement screenshotAreaGrid, Canvas parent, RowDefinition rdUp, RowDefinition rdDown, ColumnDefinition cdLeft, ColumnDefinition cdRight);
    }
}
