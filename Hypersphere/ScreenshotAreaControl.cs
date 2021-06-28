using System;
using System.Collections.Generic;
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
        PaintUC paintUC;
        SystemUC systemUC;

        public ScreenshotAreaControl()
        {
            paintUC = new PaintUC();
            systemUC = new SystemUC();
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

        public void CreateAndAddOrShow(FrameworkElement screenshotAreaGrid, Canvas parent)
        {
            // TODO: если UC выходит за границу экрана, помещать его внутрь области
            // TODO: динамическое перемещение UC в зависемости от доступного места
            // TODO: исключить пересечение UC друг с другом

            Point paintUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));
            Point systemUCCoordinate = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            // TODO: refactor
            if (!parent.Children.Contains(paintUC))
            {
                paintUCCoordinate.X += screenshotAreaGrid.ActualWidth + 6;// 3px gridSplitter, и еще 3px
                //paintUC = new PaintUC();// vertical
                Canvas.SetTop(paintUC, paintUCCoordinate.Y);
                Canvas.SetLeft(paintUC, paintUCCoordinate.X);
                paintUC.elementCollection = parent.Children;// чтобы можно было стирать нарисованное
                parent.Children.Add(paintUC);
            }
            else
            {
                paintUCCoordinate.X += screenshotAreaGrid.ActualWidth + 6;// 3px gridSplitter, и еще 3px
                Canvas.SetTop(paintUC, paintUCCoordinate.Y);
                Canvas.SetLeft(paintUC, paintUCCoordinate.X);
                paintUC.Visibility = Visibility.Visible;
            }
            if (!parent.Children.Contains(systemUC))
            {
                systemUCCoordinate.Y += screenshotAreaGrid.ActualHeight + 6;// 3px gridSplitter, и еще 3px
                //systemUC = new SystemUC();// horizontal
                Canvas.SetTop(systemUC, systemUCCoordinate.Y);
                Canvas.SetLeft(systemUC, systemUCCoordinate.X);
                parent.Children.Add(systemUC);
            }
            else
            {
                systemUCCoordinate.Y += screenshotAreaGrid.ActualHeight + 6;// 3px gridSplitter, и еще 3px
                Canvas.SetTop(systemUC, systemUCCoordinate.Y);
                Canvas.SetLeft(systemUC, systemUCCoordinate.X);
                systemUC.Visibility = Visibility.Visible;
            }
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
