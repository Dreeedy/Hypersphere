using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    /// <summary>
    /// Класс используется для хранения и передачи выбранного цвета для рисования
    /// </summary>
    public class SelectedColor
    {

        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields
        private static SolidColorBrush _selectedSolidColorBrushColor;// т.к одновременно можно использовать только один цвет
        #endregion Private_Static_Fields     



        #region Private_Fields
        private SolidColorBrush _defaultSolidColorBrushColor;
        #endregion Private_Fields



        #region Properties        
        private SolidColorBrush GetSolidColorBrushColor()
        {
            return _selectedSolidColorBrushColor;
        }
        private void SetSolidColorBrushColor(SolidColorBrush value)
        {
            _selectedSolidColorBrushColor = value;
        }
        private SolidColorBrush GetDefaultSolidBrushColor()
        {
            return _defaultSolidColorBrushColor;
        }
        #endregion Properties



        #region Public_Functions
        public SelectedColor()
        {
            _defaultSolidColorBrushColor = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }
        public void ShowColorDialogAndSetBrushColor()
        {
            System.Windows.Forms.ColorDialog colorPicker = new System.Windows.Forms.ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SolidColorBrush color 
                    = new SolidColorBrush(Color.FromArgb(colorPicker.Color.A, colorPicker.Color.R, colorPicker.Color.G, colorPicker.Color.B));
                SetSolidColorBrushColor(color);
            }
        }
        public SolidColorBrush GetSelectedOrDefaultSolidColorBrush()
        {
            if (_selectedSolidColorBrushColor == null)
            {
                return GetDefaultSolidBrushColor();
            }
            return GetSolidColorBrushColor();
        }
        #endregion Public_Functions



        #region Private_Functions

        #endregion Private_Functions



        #region Event_handlers

        #endregion Event_handlers
    }
}
