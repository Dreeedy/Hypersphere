using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hypersphere.UserControls;

namespace Hypersphere
{
    class ScreenshotAreaControl : IScreenshotAreaControl
    {
        const int PICTURE_SIZE_AND_PADDING = 34; // размер image у screenshotArea controls и padding
        const int GRID_SPLITTER_THICKNESS_AND_PADDING = 6; // толшина gridSplitter и padding

        // TODO: refactor
        const int SYSTEMUC_HEIGHT = PICTURE_SIZE_AND_PADDING;
        const int SYSTEMUC_WIDTH = 102;

        const int PAINTUC_HEIGHT = 272;
        const int PAINTUC_WIDTH = PICTURE_SIZE_AND_PADDING;

        SystemUC systemUC; // всегда расположен вертикально
        PaintUC paintUC; // всегда расположен горизонтально

        Point systemUCCoordinate;
        Point paintUCCoordinate;

        bool isSystemUCNotFit;
        bool isPaintUCNotFit;

        Point systemUCOffset;
        Point paintUCOffset;

        public ScreenshotAreaControl()
        {
            systemUC = new SystemUC();
            Panel.SetZIndex(systemUC, 1000);// нельзя рисовать поверх
            paintUC = new PaintUC();
            Panel.SetZIndex(paintUC, 1000);// нельзя рисовать поверх
        }

        public bool IsDoExistAndIsPencilDraw()
        {
            bool exist = IsDoExist();
            bool isPencilDraw = paintUC.IsPencilDraw();

            if (exist && isPencilDraw)
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
            if (!parent.Children.Contains(paintUC) && !parent.Children.Contains(systemUC))
            {
                CalculateSystemUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);              
                CalculatePaintUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);
                if (isSystemUCNotFit & isPaintUCNotFit)
                {
                    // не рисуем
                }
                else
                {
                    SetOnCanvas(screenshotAreaGrid);
                }               

                parent.Children.Add(systemUC);

                paintUC.elementCollection = parent.Children;// чтобы можно было стирать нарисованное
                parent.Children.Add(paintUC);
            }
            else
            {
                CalculateSystemUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);
                CalculatePaintUCCoordinate(screenshotAreaGrid, rdUp, rdDown, cdLeft, cdRight);

                if (isSystemUCNotFit & isPaintUCNotFit)
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



        private void CalculateSystemUCCoordinate(FrameworkElement screenshotAreaGrid, RowDefinition rdUp, RowDefinition rdDown, ColumnDefinition cdLeft, ColumnDefinition cdRight)
        {
            systemUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            if (rdDown.ActualHeight > PICTURE_SIZE_AND_PADDING)// Есть место снизу
            {
                systemUCCoordinate.Y += screenshotAreaGrid.ActualHeight + GRID_SPLITTER_THICKNESS_AND_PADDING;
            }
            if (rdDown.ActualHeight < PICTURE_SIZE_AND_PADDING && rdUp.ActualHeight > PICTURE_SIZE_AND_PADDING)// Нет места снизу
            {
                systemUCCoordinate.Y -= (PICTURE_SIZE_AND_PADDING + GRID_SPLITTER_THICKNESS_AND_PADDING);
            }
            if (rdDown.ActualHeight < PICTURE_SIZE_AND_PADDING && rdUp.ActualHeight < PICTURE_SIZE_AND_PADDING)// Нет места снизу и сверху
            {
                systemUCCoordinate.Y += screenshotAreaGrid.ActualHeight - (GRID_SPLITTER_THICKNESS_AND_PADDING + PICTURE_SIZE_AND_PADDING);
            }
            // offset чтобы systemUC не выходил за левую границу экрана
            systemUCOffset.Y = 0;
            systemUCOffset.X = 0;
            isSystemUCNotFit = false;
            if (cdLeft.ActualWidth <= SYSTEMUC_WIDTH && screenshotAreaGrid.ActualWidth <= SYSTEMUC_WIDTH)// left
            {
                systemUCOffset.X = SYSTEMUC_WIDTH - screenshotAreaGrid.ActualWidth;
                isSystemUCNotFit = true;
            }
            if (cdRight.ActualWidth <= SYSTEMUC_WIDTH && screenshotAreaGrid.ActualWidth <= SYSTEMUC_WIDTH)// right
            {
                systemUCOffset.X = (screenshotAreaGrid.ActualWidth) * -1;// чтобы смещать влево 
                isSystemUCNotFit = true;
            }
            Debug.WriteLine(systemUCOffset.X);
        }        

        private void CalculatePaintUCCoordinate(FrameworkElement screenshotAreaGrid, RowDefinition rdUp, RowDefinition rdDown,  ColumnDefinition cdLeft, ColumnDefinition cdRight)
        {
            paintUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            if (cdRight.ActualWidth > PICTURE_SIZE_AND_PADDING)// Есть место справа
            {
                paintUCCoordinate.X += screenshotAreaGrid.ActualWidth + GRID_SPLITTER_THICKNESS_AND_PADDING;
            }
            if (cdRight.ActualWidth < PICTURE_SIZE_AND_PADDING && cdLeft.ActualWidth > PICTURE_SIZE_AND_PADDING)// Нет места справа
            {
                paintUCCoordinate.X -= (PICTURE_SIZE_AND_PADDING + GRID_SPLITTER_THICKNESS_AND_PADDING);
            }
            if (cdRight.ActualWidth < PICTURE_SIZE_AND_PADDING && cdLeft.ActualWidth < PICTURE_SIZE_AND_PADDING)// Нет места справа и слева
            {
                paintUCCoordinate.X += screenshotAreaGrid.ActualWidth - (GRID_SPLITTER_THICKNESS_AND_PADDING + PICTURE_SIZE_AND_PADDING);
            }
            // offset чтобы paintUC не выходил за верхнюю границу экрана
            paintUCOffset.Y = 0;
            paintUCOffset.X = 0;
            isPaintUCNotFit = false;
            if (rdUp.ActualHeight <= PAINTUC_HEIGHT && screenshotAreaGrid.ActualHeight <= PAINTUC_HEIGHT)// top
            {
                paintUCOffset.Y = PAINTUC_HEIGHT - screenshotAreaGrid.ActualHeight;
                isPaintUCNotFit = true;
            }

            if (rdDown.ActualHeight <= PAINTUC_HEIGHT && screenshotAreaGrid.ActualHeight <= PAINTUC_HEIGHT)// bottom
            {
                paintUCOffset.Y = (screenshotAreaGrid.ActualHeight) * -1;
                isPaintUCNotFit = true;
            }
        }

        private void SetOnCanvas(FrameworkElement screenshotAreaGrid)
        {
            Canvas.SetTop(systemUC, systemUCCoordinate.Y);
            Canvas.SetLeft(systemUC, systemUCCoordinate.X + screenshotAreaGrid.ActualWidth - SYSTEMUC_WIDTH + systemUCOffset.X);// ориентация от правого нижнего угла

            Canvas.SetTop(paintUC, paintUCCoordinate.Y + screenshotAreaGrid.ActualHeight - PAINTUC_HEIGHT + paintUCOffset.Y);// ориентация от правого нижнего угла
            Canvas.SetLeft(paintUC, paintUCCoordinate.X);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// true, если элементы существуют
        /// </returns>
        private bool IsDoExist()
        {
            if (paintUC != null && systemUC != null)
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
            paintUC.Visibility = Visibility.Hidden;
            systemUC.Visibility = Visibility.Hidden;
        }

        private void Show()
        {
            paintUC.Visibility = Visibility.Visible;
            systemUC.Visibility = Visibility.Visible;
        }
    }
}