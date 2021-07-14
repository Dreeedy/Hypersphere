using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    public static class SelectedColor
    {
        private static SolidColorBrush _selectedSolidColorBrush;
        public static SolidColorBrush SelectedSolidColorBrush
        {
            get
            {
                if (_selectedSolidColorBrush == null)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));// red - цвет по умолчанию
                }
                return _selectedSolidColorBrush;
            }
            set
            {
                _selectedSolidColorBrush = value;
            }
        }

        public static void ColorDialogAndSetBrushColor(Shape selectColorShape)
        {
            System.Windows.Forms.ColorDialog colorPicker = new System.Windows.Forms.ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SolidColorBrush solidColorBrush
                    = new SolidColorBrush(Color.FromArgb(colorPicker.Color.A, colorPicker.Color.R, colorPicker.Color.G, colorPicker.Color.B));
                selectColorShape.Fill = solidColorBrush;
                SelectedColor.SelectedSolidColorBrush = solidColorBrush;
            }
        }
    }
}
