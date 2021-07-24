using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hypersphere.ScreenshotArea;
using Hypersphere.UserControls;

namespace Hypersphere
{
    class ScreenshotAreaControl : IScreenshotAreaControl
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields
        private const int _PICTURE_SIZE_AND_PADDING = 34; // размер image у screenshotArea controls и padding
        private const int _GRID_SPLITTER_THICKNESS_AND_PADDING = 6; // толшина gridSplitter и padding

        // TODO: refactor
        private const int _SYSTEMUC_HEIGHT = _PICTURE_SIZE_AND_PADDING;
        private const int _SYSTEMUC_WIDTH = 102;

        private const int _PAINTUC_HEIGHT = 272;
        private const int _PAINTUC_WIDTH = _PICTURE_SIZE_AND_PADDING;
        #endregion Private_Static_Fields     



        #region Private_Fields
        private SystemUC _systemUC; // всегда расположен вертикально
        private PaintUC _paintUC; // всегда расположен горизонтально

        private Point _systemUCCoordinate;
        private Point _paintUCCoordinate;

        private bool _isSystemUCNotFit;
        private bool _isPaintUCNotFit;

        private Point _systemUCOffset;
        private Point _paintUCOffset;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public ScreenshotAreaControl(ScreenshotWindow sw)
        {
            _systemUC = new SystemUC(sw);
            Panel.SetZIndex(_systemUC, 1000);// нельзя рисовать поверх
            _paintUC = new PaintUC();
            Panel.SetZIndex(_paintUC, 1000);// нельзя рисовать поверх
        }
        public bool IsDoExistAndIsAnyBrushDraw()
        {
            bool exist = IsDoExist();
            bool isAnyBrushDraw = _paintUC.IsAnyBrushDraw();

            if (exist && isAnyBrushDraw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDoExistAndIsPencilDraw()
        {
            bool exist = IsDoExist();
            bool isPencilDraw = _paintUC.IsPencilDraw();

            if (exist && isPencilDraw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDoExistAndIsLineDraw()
        {
            bool exist = IsDoExist();
            bool isLineDraw = _paintUC.IsLineDraw();

            if (exist && isLineDraw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDoExistAndIsArrowDraw()
        {
            bool exist = IsDoExist();
            bool isArrowDraw = _paintUC.IsArrowDraw();

            if (exist && isArrowDraw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDoExistAndIsRectangleDraw()
        {
            bool exist = IsDoExist();
            bool isRectangleDraw = _paintUC.IsRectangleDraw();

            if (exist && isRectangleDraw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDoExistAndIsMarkerDraw()
        {
            bool exist = IsDoExist();
            bool isMarkerDraw = _paintUC.IsMarkerDraw();

            if (exist && isMarkerDraw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDoExistAndIsTextDraw()
        {
            bool exist = IsDoExist();
            bool isTextDraw = _paintUC.IsTextDraw();

            if (exist && isTextDraw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void IsDoExistAndHide()
        {
            bool isExist = IsDoExist();
            if (isExist)
            {
                Hide();
            }
        }
        public void CreateAndAddOrShow(FrameworkElement screenshotAreaGrid, Canvas parent,
            RowDefinition rdUp, RowDefinition rdDown,
            ColumnDefinition cdLeft, ColumnDefinition cdRight)
        {
            // TODO: refactor
            if (!parent.Children.Contains(_paintUC) && !parent.Children.Contains(_systemUC))
            {
                CalculateSystemUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);
                CalculatePaintUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);
                if (_isSystemUCNotFit & _isPaintUCNotFit)
                {
                    // не рисуем
                }
                else
                {
                    SetOnCanvas(screenshotAreaGrid);
                }

                parent.Children.Add(_systemUC);

                _paintUC.elementCollection = parent.Children;// чтобы можно было стирать нарисованное
                parent.Children.Add(_paintUC);
            }
            else
            {
                CalculateSystemUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);
                CalculatePaintUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);

                if (_isSystemUCNotFit & _isPaintUCNotFit)
                {
                    // не рисуем
                }
                else
                {
                    SetOnCanvas(screenshotAreaGrid);
                }
            }
            Show();
        }
        #endregion Public_Methods



        #region Private_Methods
        private void CalculateSystemUCCoordinate(FrameworkElement screenshotAreaGrid, RowDefinition rdUp, RowDefinition rdDown, ColumnDefinition cdLeft, ColumnDefinition cdRight)
        {
            _systemUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            if (rdDown.ActualHeight > _PICTURE_SIZE_AND_PADDING)// Есть место снизу
            {
                _systemUCCoordinate.Y += screenshotAreaGrid.ActualHeight + _GRID_SPLITTER_THICKNESS_AND_PADDING;
            }
            if (rdDown.ActualHeight < _PICTURE_SIZE_AND_PADDING && rdUp.ActualHeight > _PICTURE_SIZE_AND_PADDING)// Нет места снизу
            {
                _systemUCCoordinate.Y -= (_PICTURE_SIZE_AND_PADDING + _GRID_SPLITTER_THICKNESS_AND_PADDING);
            }
            if (rdDown.ActualHeight < _PICTURE_SIZE_AND_PADDING && rdUp.ActualHeight < _PICTURE_SIZE_AND_PADDING)// Нет места снизу и сверху
            {
                _systemUCCoordinate.Y += screenshotAreaGrid.ActualHeight - (_GRID_SPLITTER_THICKNESS_AND_PADDING + _PICTURE_SIZE_AND_PADDING);
            }
            // offset чтобы systemUC не выходил за левую границу экрана
            _systemUCOffset.Y = 0;
            _systemUCOffset.X = 0;
            _isSystemUCNotFit = false;
            if (cdLeft.ActualWidth <= _SYSTEMUC_WIDTH && screenshotAreaGrid.ActualWidth <= _SYSTEMUC_WIDTH)// left
            {
                _systemUCOffset.X = _SYSTEMUC_WIDTH - screenshotAreaGrid.ActualWidth;
                _isSystemUCNotFit = true;
            }
            if (cdRight.ActualWidth <= _SYSTEMUC_WIDTH && screenshotAreaGrid.ActualWidth <= _SYSTEMUC_WIDTH)// right
            {
                _systemUCOffset.X = (screenshotAreaGrid.ActualWidth) * -1;// чтобы смещать влево 
                _isSystemUCNotFit = true;
            }
        }
        private void CalculatePaintUCCoordinate(FrameworkElement screenshotAreaGrid, RowDefinition rdUp, RowDefinition rdDown, ColumnDefinition cdLeft, ColumnDefinition cdRight)
        {
            _paintUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            if (cdRight.ActualWidth > _PICTURE_SIZE_AND_PADDING)// Есть место справа
            {
                _paintUCCoordinate.X += screenshotAreaGrid.ActualWidth + _GRID_SPLITTER_THICKNESS_AND_PADDING;
            }
            if (cdRight.ActualWidth < _PICTURE_SIZE_AND_PADDING && cdLeft.ActualWidth > _PICTURE_SIZE_AND_PADDING)// Нет места справа
            {
                _paintUCCoordinate.X -= (_PICTURE_SIZE_AND_PADDING + _GRID_SPLITTER_THICKNESS_AND_PADDING);
            }
            if (cdRight.ActualWidth < _PICTURE_SIZE_AND_PADDING && cdLeft.ActualWidth < _PICTURE_SIZE_AND_PADDING)// Нет места справа и слева
            {
                _paintUCCoordinate.X += screenshotAreaGrid.ActualWidth - (_GRID_SPLITTER_THICKNESS_AND_PADDING + _PICTURE_SIZE_AND_PADDING);
            }
            // offset чтобы paintUC не выходил за верхнюю границу экрана
            _paintUCOffset.Y = 0;
            _paintUCOffset.X = 0;
            _isPaintUCNotFit = false;
            if (rdUp.ActualHeight <= _PAINTUC_HEIGHT && screenshotAreaGrid.ActualHeight <= _PAINTUC_HEIGHT)// top
            {
                _paintUCOffset.Y = _PAINTUC_HEIGHT - screenshotAreaGrid.ActualHeight;
                _isPaintUCNotFit = true;
            }

            if (rdDown.ActualHeight <= _PAINTUC_HEIGHT && screenshotAreaGrid.ActualHeight <= _PAINTUC_HEIGHT)// bottom
            {
                _paintUCOffset.Y = (screenshotAreaGrid.ActualHeight) * -1;
                _isPaintUCNotFit = true;
            }
        }
        private void SetOnCanvas(FrameworkElement screenshotAreaGrid)
        {
            Canvas.SetTop(_systemUC, _systemUCCoordinate.Y);
            Canvas.SetLeft(_systemUC, _systemUCCoordinate.X + screenshotAreaGrid.ActualWidth - _SYSTEMUC_WIDTH + _systemUCOffset.X);// ориентация от правого нижнего угла

            Canvas.SetTop(_paintUC, _paintUCCoordinate.Y + screenshotAreaGrid.ActualHeight - _PAINTUC_HEIGHT + _paintUCOffset.Y);// ориентация от правого нижнего угла
            Canvas.SetLeft(_paintUC, _paintUCCoordinate.X);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// true, если элементы существуют
        /// </returns>
        private bool IsDoExist()
        {
            if (_paintUC != null && _systemUC != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Hide()
        {
            _paintUC.Visibility = Visibility.Hidden;
            _systemUC.Visibility = Visibility.Hidden;
        }
        private void Show()
        {
            _paintUC.Visibility = Visibility.Visible;
            _systemUC.Visibility = Visibility.Visible;
        }
        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}