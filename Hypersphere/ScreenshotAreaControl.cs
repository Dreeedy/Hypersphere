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
                                                           // 
        SystemUC systemUC; // всегда расположен вертикально
        PaintUC paintUC; // всегда расположен горизонтально

        Point paintUCCoordinate;
        Point systemUCCoordinate;
        


        public ScreenshotAreaControl()
        {            
            systemUC = new SystemUC();
            paintUC = new PaintUC();
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
            // TODO: исключить пересечение UC друг с другом

            // TODO: refactor
            if (!parent.Children.Contains(paintUC) && !parent.Children.Contains(systemUC))
            {
                CalculateSystemUCCoordinate(screenshotAreaGrid, rdUp, rdDown);
                parent.Children.Add(systemUC);

                CalculatePaintUCCoordinate(screenshotAreaGrid, cdLeft, cdRight);
                paintUC.elementCollection = parent.Children;// чтобы можно было стирать нарисованное
                parent.Children.Add(paintUC);
            }
            else
            {
                CalculateSystemUCCoordinate(screenshotAreaGrid, rdUp, rdDown);
                CalculatePaintUCCoordinate(screenshotAreaGrid, cdLeft, cdRight);
            }
            Show();
        }



        private void CalculateSystemUCCoordinate(FrameworkElement screenshotAreaGrid, RowDefinition rdUp, RowDefinition rdDown)
        {
            systemUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            if (rdDown.ActualHeight > PICTURE_SIZE_AND_PADDING && rdDown.ActualHeight > PICTURE_SIZE_AND_PADDING)// Есть место и снизу и сверху
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
            Canvas.SetTop(systemUC, systemUCCoordinate.Y);
            Canvas.SetLeft(systemUC, systemUCCoordinate.X);
        }

        private void CalculatePaintUCCoordinate(FrameworkElement screenshotAreaGrid, ColumnDefinition cdLeft, ColumnDefinition cdRight)
        {
            paintUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            if (cdRight.ActualWidth > PICTURE_SIZE_AND_PADDING && cdLeft.ActualWidth > PICTURE_SIZE_AND_PADDING)// Есть место и слева и справа
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
            Canvas.SetTop(paintUC, paintUCCoordinate.Y);
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
