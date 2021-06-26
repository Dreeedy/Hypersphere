﻿using System;
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

        Path path;
        GeometryGroup geometryGroup;

        public ScreenshotWindow()
        {
            InitializeComponent();
        }

        #region Event_Handlers      
        private void mainGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isLeftMouseButtonPressed = true;
            previousMouseCoordinates = e.GetPosition(mainGrid);

            if (isLeftMouseButtonPressed == true && screenshotArea == null && paintUC != null && paintUC.GetisPencilDraw())
            { 
                // TODO: нормальная инициализация карандаша
                path = new Path();
                path.Stroke = Brushes.Black;
                path.StrokeThickness = 1;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 204, 204, 255);
                path.Fill = mySolidColorBrush;

                geometryGroup = new GeometryGroup();
                path.Data = geometryGroup;

                paintAndUserControlsCanvas.Children.Add(path); 
            }
        }

        private void mainGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            currentMouseCoordinates = e.GetPosition(mainGrid);
            offsetMouseCoordinates.Y = previousMouseCoordinates.Y - currentMouseCoordinates.Y;
            offsetMouseCoordinates.X = previousMouseCoordinates.X - currentMouseCoordinates.X;

            // TODO: рисование
            // TODO: refactor/рисование
            if (isLeftMouseButtonPressed == true && screenshotArea == null && paintUC != null && paintUC.GetisPencilDraw())
            { 
                LineGeometry line = new LineGeometry();

                line.StartPoint = previousMouseCoordinates;
                line.EndPoint = currentMouseCoordinates;

                geometryGroup.Children.Add(line);
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

            CheckUserControlAndHide();
        }

        private void screenshotAreaGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            screenshotArea.ReleaseMouseCapture();

            // отображение controls происходит только тогда, когда форма не screenshotArea
            CheckUserControlAndHide();
            CreateAndAddUserControlOrShow();

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
            CheckUserControlAndHide();
        }

        private void GridSplittersHandler_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            CheckUserControlAndHide();
            CreateAndAddUserControlOrShow();
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

        private void CheckUserControlAndHide()
        {
            if (paintUC != null)
            {
                paintUC.Visibility = Visibility.Hidden;
            }
            if (systemUC != null)
            {
                systemUC.Visibility = Visibility.Hidden;
            }
        }

        private void CreateAndAddUserControlOrShow()
        {
            // TODO: если UC выходит за границу экрана, помещать его внутрь области
            // TODO: динамическое перемещение UC в зависемости от доступного места
            // TODO: исключить пересечение UC друг с другом

            Point paintUCPoint = screenshotAreaGrid.PointToScreen(new Point(0, 0));
            Point systemUCPoint = screenshotAreaGrid.PointToScreen(new Point(0, 0));

            // TODO: refactor
            if (!paintAndUserControlsCanvas.Children.Contains(paintUC))
            {
                paintUCPoint.X += screenshotAreaGrid.ActualWidth + 6;// 3px gridSplitter, и еще 3px
                paintUC = new PaintUC();// vertical
                Canvas.SetTop(paintUC, paintUCPoint.Y);
                Canvas.SetLeft(paintUC, paintUCPoint.X);
                paintUC.elementCollection = paintAndUserControlsCanvas.Children;// чтобы можно было стирать нарисованное
                paintAndUserControlsCanvas.Children.Add(paintUC);
            }
            else
            {
                paintUCPoint.X += screenshotAreaGrid.ActualWidth + 6;// 3px gridSplitter, и еще 3px
                Canvas.SetTop(paintUC, paintUCPoint.Y);
                Canvas.SetLeft(paintUC, paintUCPoint.X);
                paintUC.Visibility = Visibility.Visible;
            }
            if (!paintAndUserControlsCanvas.Children.Contains(systemUC))
            {
                systemUCPoint.Y += screenshotAreaGrid.ActualHeight + 6;// 3px gridSplitter, и еще 3px
                systemUC = new SystemUC();// horizontal
                Canvas.SetTop(systemUC, systemUCPoint.Y);
                Canvas.SetLeft(systemUC, systemUCPoint.X);
                paintAndUserControlsCanvas.Children.Add(systemUC);
            }
            else
            {
                systemUCPoint.Y += screenshotAreaGrid.ActualHeight + 6;// 3px gridSplitter, и еще 3px
                Canvas.SetTop(systemUC, systemUCPoint.Y);
                Canvas.SetLeft(systemUC, systemUCPoint.X);
                systemUC.Visibility = Visibility.Visible;
            }          
        }

        
    }
}
