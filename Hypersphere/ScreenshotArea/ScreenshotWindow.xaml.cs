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
        private UIElement _screenshotArea;

        private bool _isLeftMouseButtonPressedOnMainGrid;        

        private IMouseCoordinates _mouseCoordinates;   
        
        private IScreenshotAreaControl _screenshotAreaControl;

        private IDrawingPencilBrush _drawingPencil;
        private IDrawingTextBrush _drawingText;
        private ITwoPointDrawingBrush _drawingLine;
        private ITwoPointDrawingBrush _drawingArrow;
        private ITwoPointDrawingBrush _drawingRectangle;
        private ITwoPointDrawingBrush _drawingMarker;

        private double _primaryScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
        private double _primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

        private bool _isScreenshotAreaResizing;

        private Point _resizingPoint;
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
            _drawingPencil = new DrawingPencilBrush();
            _drawingLine = new DrawingLineBrush();
            _drawingArrow = new DrawingArrowBrush();
            _drawingRectangle = new DrawingRectangleBrush();
            _drawingMarker = new DrawingMarkerBrush();
            _drawingText = new DrawingTextBrush();

            _primaryScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            _primaryScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

            _isScreenshotAreaResizing = true;



            MinimizeScreenshotArea();
        }
        #endregion Public_Methods



        #region Private_Methods
        /// <summary>
        /// Затемняет экран
        /// </summary>
        private void MinimizeScreenshotArea()
        {
            rdUp.Height = new GridLength((_primaryScreenHeight / 2));
            rdDown.Height = new GridLength((_primaryScreenHeight / 2));
            
            cdLeft.Width = new GridLength((_primaryScreenWidth / 2));
            cdRight.Width = new GridLength((_primaryScreenWidth / 2));
        }
        private void ResizeScreenshotArea()
        {
            Point currentMouseCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();

            // resize TOP-LEFT
            if (currentMouseCoordinates.Y < _resizingPoint.Y && currentMouseCoordinates.X < _resizingPoint.X)
            {
                ResizeScreenshotAreaUp(currentMouseCoordinates);
                ResizeScreenshotAreaLeft(currentMouseCoordinates);

                return;
            }
            // resize TOP-RIGHT
            if (currentMouseCoordinates.Y < _resizingPoint.Y && currentMouseCoordinates.X > _resizingPoint.X)
            {
                ResizeScreenshotAreaUp(currentMouseCoordinates);
                ResizeScreenshotAreaRight(currentMouseCoordinates);

                return;
            }
            // resize BOTTOM-RIGHT
            if (currentMouseCoordinates.Y > _resizingPoint.Y && currentMouseCoordinates.X > _resizingPoint.X)
            {
                ResizeScreenshotAreaDown(currentMouseCoordinates);
                ResizeScreenshotAreaRight(currentMouseCoordinates);

                return;
            }
            // resize BOTTOM-LEFT
            if (currentMouseCoordinates.Y > _resizingPoint.Y && currentMouseCoordinates.X < _resizingPoint.X)
            {
                ResizeScreenshotAreaDown(currentMouseCoordinates);
                ResizeScreenshotAreaLeft(currentMouseCoordinates);

                return;
            }
            
            return;
        }
        private void ResizeScreenshotAreaUp(Point currentMouseCoordinates)
        {
            rdUp.Height = new GridLength((currentMouseCoordinates.Y));
        }
        private void ResizeScreenshotAreaDown(Point currentMouseCoordinates)
        {
            rdDown.Height = new GridLength((_primaryScreenHeight - currentMouseCoordinates.Y));
        }
        private void ResizeScreenshotAreaLeft(Point currentMouseCoordinates)
        {
            cdLeft.Width = new GridLength((currentMouseCoordinates.X));
        }
        private void ResizeScreenshotAreaRight(Point currentMouseCoordinates)
        {
            cdRight.Width = new GridLength((_primaryScreenWidth - currentMouseCoordinates.X));
        }
        private void MoveScreenshotArea()
        {
            /*
             * Движение ScreenshotArea происходит за счет изменения width у ColumnDefinition             
             * и height RowDefinition находящихся внутри Grid blackAndScreenshotAreasGrid
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
            _isLeftMouseButtonPressedOnMainGrid = true;

            _mouseCoordinates.SetPreviousMouseCoordinates(mainGrid, e);

            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsPencilDraw())
            {
                _drawingPencil.CreatePencil(paintAndUserControlsCanvas);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsLineDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();
                _drawingLine.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsArrowDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();
                _drawingArrow.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsRectangleDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();
                _drawingRectangle.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsMarkerDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();
                _drawingMarker.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsTextDraw())
            {
                _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
                Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();
                _drawingText.CreateAndSetPoints(paintAndUserControlsCanvas, currentCoordinates);
            }

            if (_isLeftMouseButtonPressedOnMainGrid == true && screenshotAreaGrid.ActualWidth <= 0 && screenshotAreaGrid.ActualHeight <= 0)
            {               
                Point previousMouseCoordinates = _mouseCoordinates.GetPreviousMouseCoordinates();
                _resizingPoint = previousMouseCoordinates;

                rdUp.Height = new GridLength((previousMouseCoordinates.Y));
                rdDown.Height = new GridLength((_primaryScreenHeight - previousMouseCoordinates.Y));

                cdLeft.Width = new GridLength((previousMouseCoordinates.X));
                cdRight.Width = new GridLength((_primaryScreenWidth - previousMouseCoordinates.X));       
            }
        }
        private void mainGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            _mouseCoordinates.SetCurrentMouseCoordinates(mainGrid, e);
            _mouseCoordinates.СalculateOffsetMouseCoordinates();

            Point previousCoordinates = _mouseCoordinates.GetPreviousMouseCoordinates();
            Point currentCoordinates = _mouseCoordinates.GetCurrentMouseCoordinates();

            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsPencilDraw())
            {
                _drawingPencil.DrawLineGeometry(previousCoordinates, currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsLineDraw())
            {
                _drawingLine.UpdateEndPoint(currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsArrowDraw())
            {
                _drawingArrow.UpdateEndPoint(currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsRectangleDraw())
            {
                _drawingRectangle.UpdateEndPoint(currentCoordinates);
            }
            if (_isLeftMouseButtonPressedOnMainGrid == true && _screenshotAreaControl.IsDoExistAndIsMarkerDraw())
            {
                _drawingMarker.UpdateEndPoint(currentCoordinates);
            }
            _mouseCoordinates.UpdatePreviousMouseCoordinates();

            if (_isLeftMouseButtonPressedOnMainGrid == true && _isScreenshotAreaResizing == true)
            {
                ResizeScreenshotArea();
            }
        }
        private void mainGrid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isLeftMouseButtonPressedOnMainGrid = false;
            _isScreenshotAreaResizing = false;

            if (!_isScreenshotAreaResizing)
            {
                _screenshotAreaControl.CreateAndAddOrShow(screenshotAreaGrid, paintAndUserControlsCanvas, rdUp, rdDown, cdLeft, cdRight);
            }
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

            if (_screenshotArea != null)
            {
                _screenshotArea.ReleaseMouseCapture();
            }
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
