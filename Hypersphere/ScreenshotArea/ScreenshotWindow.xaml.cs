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
    public partial class ScreenshotWindow : Window
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        UIElement _screenshotArea;
        bool _isLeftMouseButtonPressed;
        IMouseCoordinates _mouseCoordinates;        
        IScreenshotAreaControl _screenshotAreaControl;
        IDrawingPencil _drawingPencil;
        IDrawingLine _drawingLine;
        // TODO: сделать один общий интерфейс типо IDrawing или продолжить делать отдельные
        DrawingArrow _drawingArrow;
        DrawingRectangle _drawingRectangle;
        DrawingMarker _drawingMarker;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        // TODO: запретить рисовать поверх gridSplitters. Скорее всего реализовать не получится
        public ScreenshotWindow()
        {
            InitializeComponent();

            _mouseCoordinates = new MouseCoordinates();            
            _screenshotAreaControl = new ScreenshotAreaControl();
            _drawingPencil = new DrawingPencil();
            _drawingLine = new DrawingLine();
            _drawingArrow = new DrawingArrow();
            _drawingRectangle = new DrawingRectangle();
            _drawingMarker = new DrawingMarker();
        }
        #endregion Public_Methods



        #region Private_Methods
        private void MoveScreenshotArea()
        {
            /*
             Движение ScreenshotArea происходит за счет изменения width у ColumnDefinition 
            и height RowDefinition находящихся внутри Grid blackAndScreenshotAreasGrid 
             */
            Point offset = _mouseCoordinates.GetOffsetMouseCoordinates();

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
        #endregion Private_Methods



        #region Event_handlers
        private void mainGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isLeftMouseButtonPressed = true;

            _mouseCoordinates.SetPreviousMouseCoordinates(mainGrid, e);

            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsPencilDraw())
            {
                _drawingPencil.CreatePencil(paintAndUserControlsCanvas);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsLineDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();

                _drawingLine.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsArrowDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();

                _drawingArrow.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsRectangleDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();

                _drawingRectangle.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsMarkerDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();

                _drawingMarker.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
        }
        private void mainGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
            _mouseCoordinates.СalculateOffsetMouseCoordinates();

            Point previousCoordinates = _mouseCoordinates.GetPreviousMouseCoordinates();
            Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();

            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsPencilDraw())
            {
                _drawingPencil.DrawLineGeometry(previousCoordinates, currentCoordinates);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsLineDraw())
            {
                _drawingLine.UpdateEndPoint(currentCoordinates);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsArrowDraw())
            {
                _drawingArrow.UpdateEndPoint(currentCoordinates);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsRectangleDraw())
            {
                _drawingRectangle.UpdateEndPoint(currentCoordinates);
            }
            if (_isLeftMouseButtonPressed == true && _screenshotAreaControl.IsDoExistAndIsMarkerDraw())
            {
                _drawingMarker.UpdateEndPoint(currentCoordinates);
            }

            _mouseCoordinates.UpdatePreviousMouseCoordinates();
        }
        private void mainGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isLeftMouseButtonPressed = false;
        }
        private void screenshotAreaGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_screenshotAreaControl.IsDoExistAndIsAnyBrushDraw())
            {
                GridSplittersEnabledToFalse();
                return;
            }
            GridSplittersEnabledToTrue();

            _screenshotArea = sender as UIElement;
            _screenshotArea.CaptureMouse();
        }
        private void screenshotAreaGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_screenshotAreaControl.IsDoExistAndIsAnyBrushDraw())
            {
                GridSplittersEnabledToFalse();
                return;
            }
            GridSplittersEnabledToTrue();

            if (_screenshotArea == null)
            {
                return;
            }
            MoveScreenshotArea();

            _screenshotAreaControl.IsDoExistAndHide();
        }
        private void screenshotAreaGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_screenshotAreaControl.IsDoExistAndIsAnyBrushDraw())
            {
                GridSplittersEnabledToFalse();
                return;
            }
            GridSplittersEnabledToTrue();

            _screenshotArea.ReleaseMouseCapture();

            // отображение controls происходит только тогда, когда форма не _screenshotArea
            _screenshotAreaControl.IsDoExistAndHide();
            _screenshotAreaControl.CreateAndAddOrShow(screenshotAreaGrid, paintAndUserControlsCanvas, rdUp, rdDown, cdLeft, cdRight);

            _screenshotArea = null;
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
            _screenshotAreaControl.IsDoExistAndHide();
        }
        private void GridSplittersHandler_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            _screenshotAreaControl.IsDoExistAndHide();
            _screenshotAreaControl.CreateAndAddOrShow(screenshotAreaGrid, paintAndUserControlsCanvas, rdUp, rdDown, cdLeft, cdRight);
        }
        #endregion Event_handlers
    }
}
