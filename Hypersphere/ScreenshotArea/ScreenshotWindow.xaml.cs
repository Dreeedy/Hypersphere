using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hypersphere.UserControls;

namespace Hypersphere.ScreenshotArea
{
    /// <summary>
    /// Interaction logic for ScreenshotWindow.xaml
    /// </summary>
    public partial class ScreenshotWindow : Window
    {
        Point previousMouseCoordinates = new Point();
        Point currentMouseCoordinates = new Point();
        Point offsetMouseCoordinates = new Point();

        UIElement screenshotArea;

        bool isLeftMouseButtonPressed;

        PaintUC paintUC;
        SystemUC systemUC;

        public ScreenshotWindow()
        {
            InitializeComponent();
        }

        #region Event_Handlers      
        private void mainGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isLeftMouseButtonPressed = true;
            previousMouseCoordinates = e.GetPosition(mainGrid);
        }

        private void mainGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            currentMouseCoordinates = e.GetPosition(mainGrid);
            offsetMouseCoordinates.Y = previousMouseCoordinates.Y - currentMouseCoordinates.Y;
            offsetMouseCoordinates.X = previousMouseCoordinates.X - currentMouseCoordinates.X;

            // TODO: рисование
            if (isLeftMouseButtonPressed == true && screenshotArea == null && paintUC != null && paintUC.GetisPencilDraw())
            {
                Line line = new Line();
                paintAndUserControlsCanvas.Children.Add(line);

                line.Stroke = new SolidColorBrush(Colors.Aqua);
                line.StrokeThickness = 2;
                line.Y1 = previousMouseCoordinates.Y;
                line.X1 = previousMouseCoordinates.X;
                line.Y2 = currentMouseCoordinates.Y;
                line.X2 = currentMouseCoordinates.X;
            }         

            previousMouseCoordinates = currentMouseCoordinates;
        }

        private void mainGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isLeftMouseButtonPressed = false;
        }

        private void screenshotAreaGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            screenshotArea = sender as UIElement;
            screenshotArea.CaptureMouse();
        }

        private void screenshotAreaGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (screenshotArea == null)
            {
                return;
            }            
            MoveScreenshotArea();

            CheckUserControlAndRemove();
        }

        private void screenshotAreaGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            screenshotArea.ReleaseMouseCapture();

            // отображение controls происходит только тогда, когда форма не screenshotArea
            CheckUserControlAndRemove();
            CreateAndAddUserControl();

            screenshotArea = null;            
        }

        private void blackAndScreenshotAreasGrid_Loaded(object sender, RoutedEventArgs e)
        {
            gsTop.DragStarted += GridSplittersHandler_DragStarted;
            gsBottom.DragStarted += GridSplittersHandler_DragStarted;
            gsLeft.DragStarted += GridSplittersHandler_DragStarted;
            gsRight.DragStarted += GridSplittersHandler_DragStarted;

            gsTop.DragDelta += GridSplittersHandler_DragDelta;
            gsBottom.DragDelta += GridSplittersHandler_DragDelta;
            gsLeft.DragDelta += GridSplittersHandler_DragDelta;
            gsRight.DragDelta += GridSplittersHandler_DragDelta;

            gsTop.DragCompleted += GridSplittersHandler_DragCompleted;
            gsBottom.DragCompleted += GridSplittersHandler_DragCompleted;
            gsLeft.DragCompleted += GridSplittersHandler_DragCompleted;
            gsRight.DragCompleted += GridSplittersHandler_DragCompleted;
        }

        private void GridSplittersHandler_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {

        }

        private void GridSplittersHandler_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            CheckUserControlAndRemove();
        }

        private void GridSplittersHandler_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            CheckUserControlAndRemove();
            CreateAndAddUserControl();
        }        
        #endregion

        private void MoveScreenshotArea()
        {
            /*
             Движение ScreenshotArea происходит за счет изменения width у ColumnDefinition 
            и height RowDefinition находящихся внутри Grid blackAndScreenshotAreasGrid 
             */
            if (offsetMouseCoordinates.Y > 0)
            {
                MoveScreenshotAreaUp();
            }
            if (offsetMouseCoordinates.Y < 0)
            {
                MoveScreenshotAreaDown();
            }
            if (offsetMouseCoordinates.X > 0)
            {
                MoveScreenshotAreaLeft();
            }
            if (offsetMouseCoordinates.X < 0)
            {
                MoveScreenshotAreaRight();
            }
        }

        private void MoveScreenshotAreaUp()
        {
            if (rdUp.ActualHeight - offsetMouseCoordinates.Y >= 0)
            {
                rdUp.Height = new GridLength(rdUp.ActualHeight - offsetMouseCoordinates.Y);
            }
            rdDown.Height = new GridLength(rdDown.ActualHeight + offsetMouseCoordinates.Y);
        }

        private void MoveScreenshotAreaDown()
        {
            if (rdDown.ActualHeight + offsetMouseCoordinates.Y >= 0)
            {
                rdDown.Height = new GridLength(rdDown.ActualHeight + offsetMouseCoordinates.Y);
            }
            rdUp.Height = new GridLength(rdUp.ActualHeight - offsetMouseCoordinates.Y);
        }

        private void MoveScreenshotAreaLeft()
        {
            if (cdLeft.ActualWidth - offsetMouseCoordinates.X >= 0)
            {
                cdLeft.Width = new GridLength(cdLeft.ActualWidth - offsetMouseCoordinates.X);
            }
            cdRight.Width = new GridLength(cdRight.ActualWidth + offsetMouseCoordinates.X);
        }

        private void MoveScreenshotAreaRight()
        {
            if (cdRight.ActualWidth + offsetMouseCoordinates.X >= 0)
            {
                cdRight.Width = new GridLength(cdRight.ActualWidth + offsetMouseCoordinates.X);
            }
            cdLeft.Width = new GridLength(cdLeft.ActualWidth - offsetMouseCoordinates.X);
        }

        private void CheckUserControlAndRemove()
        {
            if (paintUC != null)
            {
                paintAndUserControlsCanvas.Children.Remove(paintUC);
                paintUC = null;
            }
            if (systemUC != null)
            {
                paintAndUserControlsCanvas.Children.Remove(systemUC);
                systemUC = null;
            }
        }

        private void CreateAndAddUserControl()
        {
            // TODO: если UC выходит за границу экрана, помещать его внутрь области
            // TODO: динамическое перемещение UC в зависемости от доступного места
            // TODO: исключить пересечение UC друг с другом
            // TODO: не отображать UC когда происходит изменения размеров screenshotArea
            Point paintUCPoint = screenshotAreaGrid.PointToScreen(new Point(0, 0));
            Point systemUCPoint = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            paintUCPoint.X += screenshotAreaGrid.ActualWidth + 6;// 3px gridSplitter, и еще 3px
            paintUC = new PaintUC();// vertical
            Canvas.SetTop(paintUC, paintUCPoint.Y);
            Canvas.SetLeft(paintUC, paintUCPoint.X);
            paintAndUserControlsCanvas.Children.Add(paintUC);

            systemUCPoint.Y += screenshotAreaGrid.ActualHeight + 6;// 3px gridSplitter, и еще 3px
            systemUC = new SystemUC();// horizontal
            Canvas.SetTop(systemUC, systemUCPoint.Y);
            Canvas.SetLeft(systemUC, systemUCPoint.X);
            paintAndUserControlsCanvas.Children.Add(systemUC);
        }


    }
}
