using System.Windows;
using System.Windows.Controls;

namespace Hypersphere
{
    interface IScreenshotAreaControl
    {
        public bool IsDoExistAndIsAnyBrushDraw();
        public bool IsDoExistAndIsPencilDraw();
        public bool IsDoExistAndIsLineDraw();
        public bool IsDoExistAndIsArrowDraw();
        public bool IsDoExistAndIsRectangleDraw();
        public bool IsDoExistAndIsMarkerDraw();
        public bool IsDoExistAndIsTextDraw();

        public void IsDoExistAndHide();

        public void CreateAndAddOrShow(FrameworkElement screenshotAreaGrid, Canvas parent, RowDefinition rdUp, RowDefinition rdDown, ColumnDefinition cdLeft, ColumnDefinition cdRight);
    }
}
