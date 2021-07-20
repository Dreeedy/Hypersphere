using System.Windows.Media;

namespace Hypersphere
{
    interface ISelectedColor
    {
        public void ShowColorDialogAndSetBrushColor();
        public SolidColorBrush GetSelectedOrDefaultSolidColorBrush();
    }
}
