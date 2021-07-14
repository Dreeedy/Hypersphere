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
        UIElement screenshotArea;

        bool isLeftMouseButtonPressed;

        IMouseCoordinates mouseCoordinates;
        IDrawingPencil drawingPencil;
        IScreenshotAreaControl screenshotAreaControl;



        // TODO: запретить рисовать поверх gridSplitters. Скорее всего реализовать не получится
        public ScreenshotWindow()
        {
            InitializeComponent();

            mouseCoordinates = new MouseCoordinates();
            drawingPencil = new DrawingPencil();
            screenshotAreaControl = new ScreenshotAreaControl();
        }



        #region Event_Handlers      
        private void mainGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isLeftMouseButtonPressed = true;

            mouseCoordinates.SetPreviousMouseCoordinates(mainGrid, e);
            
            if (isLeftMouseButtonPressed == true && screenshotAreaControl.IsDoExistAndIsPencilDraw())
            {
                drawingPencil.CreatePencil(paintAndUserControlsCanvas);
            }
        }

        private void mainGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
            mouseCoordinates.СalculateOffsetMouseCoordinates();

            if (isLeftMouseButtonPressed == true && screenshotAreaControl.IsDoExistAndIsPencilDraw())
            {
                Point previousCoordinates = mouseCoordinates.GetPreviousMouseCoordinates();
                Point currentCoordinates = mouseCoordinates.GetCurrentMouseCoordinates();
                drawingPencil.DrawLineGeometry(previousCoordinates, currentCoordinates);
            }
            mouseCoordinates.UpdatePreviousMouseCoordinates();
        }

        private void mainGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {           
            isLeftMouseButtonPressed = false;
        }

        private void screenshotAreaGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (screenshotAreaControl.IsDoExistAndIsAnyBrushDraw())
            {
                GridSplittersEnabledToFalse();
                return;
            }
            GridSplittersEnabledToTrue();

            screenshotArea = sender as UIElement;
            screenshotArea.CaptureMouse();
        }
        
        private void screenshotAreaGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (screenshotAreaControl.IsDoExistAndIsAnyBrushDraw())
            {
                GridSplittersEnabledToFalse();
                return;
            }
            GridSplittersEnabledToTrue();

            if (screenshotArea == null)
            {
                return;
            }            
            MoveScreenshotArea();

            screenshotAreaControl.IsDoExistAndHide();
        }

        private void screenshotAreaGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (screenshotAreaControl.IsDoExistAndIsAnyBrushDraw())
            {
                GridSplittersEnabledToFalse();
                return;
            }
            GridSplittersEnabledToTrue();

            screenshotArea.ReleaseMouseCapture();

            // отображение controls происходит только тогда, когда форма не screenshotArea
            screenshotAreaControl.IsDoExistAndHide();
            screenshotAreaControl.CreateAndAddOrShow(screenshotAreaGrid, paintAndUserControlsCanvas, rdUp, rdDown, cdLeft, cdRight);

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
            screenshotAreaControl.IsDoExistAndHide();
        }

        private void GridSplittersHandler_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            screenshotAreaControl.IsDoExistAndHide();
            screenshotAreaControl.CreateAndAddOrShow(screenshotAreaGrid, paintAndUserControlsCanvas, rdUp, rdDown, cdLeft, cdRight);
        }        
        #endregion



        private void MoveScreenshotArea()
        {
            /*
             Движение ScreenshotArea происходит за счет изменения width у ColumnDefinition 
            и height RowDefinition находящихся внутри Grid blackAndScreenshotAreasGrid 
             */
            Point offset = mouseCoordinates.GetOffsetMouseCoordinates();

            if (offset.Y > 0)
            {
                MoveScreenshotAreaUp(offset);
            }
            if (offset.Y < 0)
            {
                MoveScreenshotAreaDown(offset);
            }
            if (offset.X > 0)
            {
                MoveScreenshotAreaLeft(offset);
            }
            if (offset.X < 0)
            {
                MoveScreenshotAreaRight(offset);
            }
        }

        private void MoveScreenshotAreaUp(Point offset)
        {
            if (rdUp.ActualHeight - offset.Y >= 0)
            {
                rdUp.Height = new GridLength(rdUp.ActualHeight - offset.Y);
            }
            rdDown.Height = new GridLength(rdDown.ActualHeight + offset.Y);
        }

        private void MoveScreenshotAreaDown(Point offset)
        {
            if (rdDown.ActualHeight + offset.Y >= 0)
            {
                rdDown.Height = new GridLength(rdDown.ActualHeight + offset.Y);
            }
            rdUp.Height = new GridLength(rdUp.ActualHeight - offset.Y);
        }

        private void MoveScreenshotAreaLeft(Point offset)
        {
            if (cdLeft.ActualWidth - offset.X >= 0)
            {
                cdLeft.Width = new GridLength(cdLeft.ActualWidth - offset.X);
            }
            cdRight.Width = new GridLength(cdRight.ActualWidth + offset.X);
        }

        private void MoveScreenshotAreaRight(Point offset)
        {
            if (cdRight.ActualWidth + offset.X >= 0)
            {
                cdRight.Width = new GridLength(cdRight.ActualWidth + offset.X);
            }
            cdLeft.Width = new GridLength(cdLeft.ActualWidth - offset.X);
        }

        private void GridSplittersEnabledToTrue()
        {
            if (!gsTop.IsEnabled)
            {
                gsTop.IsEnabled = true;
            }
            if (!gsBottom.IsEnabled)
            {
                gsBottom.IsEnabled = true;
            }
            if (!gsLeft.IsEnabled)
            {
                gsLeft.IsEnabled = true;
            }
            if (!gsRight.IsEnabled)
            {
                gsRight.IsEnabled = true;
            }
        }

        private void GridSplittersEnabledToFalse()
        {
            if (gsTop.IsEnabled)
            {
                gsTop.IsEnabled = false;
            }
            if (gsBottom.IsEnabled)
            {
                gsBottom.IsEnabled = false;
            }
            if (gsLeft.IsEnabled)
            {
                gsLeft.IsEnabled = false;
            }
            if (gsRight.IsEnabled)
            {
                gsRight.IsEnabled = false;
            }
        }
    }
}
