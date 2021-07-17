using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hypersphere
{
    class DrawingText
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private SelectedColor _selectedColor;
        private TextBox _textBox;        
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public DrawingText()
        {
            _selectedColor = new SelectedColor();
            // TODO: перещение textBox
        }
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint)
        {            
            if (_textBox != null)
            {
                if (_textBox.Text == "")
                {
                    canvasForDraw.Children.Remove(_textBox);
                }
                _textBox.IsReadOnly = true;
                _textBox.BorderThickness = new Thickness(0);
                _textBox.IsHitTestVisible = false;// мышь больше не реагирует на этот элемент
            }
            _textBox = new TextBox();

            Canvas.SetTop(_textBox, startPoint.Y);
            Canvas.SetLeft(_textBox, startPoint.X);

            _textBox.Background = Brushes.Transparent;

            _textBox.Height = Double.NaN;
            _textBox.Width = Double.NaN;
            
            _textBox.MinHeight = 10;
            _textBox.MinWidth = 20;           

            _textBox.FontSize = 18;

            _textBox.Foreground = _selectedColor.GetSelectedOrDefaultSolidColorBrush();

            _textBox.BorderThickness = new Thickness(1);            

            canvasForDraw.Children.Add(_textBox);

            SetMouseFocus(_textBox);// чтобы сразу писать в нем
        }
        #endregion Public_Methods



        #region Private_Methods
        private void SetMouseFocus(TextBox textBox)
        {
            _textBox.Focus();
        }
        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
