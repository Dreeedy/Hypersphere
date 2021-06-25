using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Hypersphere
{
    /// <summary>
    /// Interaction logic for TestInvisibleWindow.xaml
    /// </summary>
    public partial class TestInvisibleWindow : Window
    {
        Point previousMouseCoordinates = new Point();
        Point currentMouseCoordinates = new Point();
        Point offset = new Point();
        UIElement dragObject;

        public TestInvisibleWindow()
        {
            InitializeComponent();
        }        

        private void children_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            dragObject = children as UIElement;
            previousMouseCoordinates = e.GetPosition(mainOwner);
            children.CaptureMouse();
        }

        private void children_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            dragObject = null;
            children.ReleaseMouseCapture();
        }

        private void mainOwner_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragObject == null)
            {
                return;
            }
            currentMouseCoordinates = e.GetPosition(mainOwner);
            offset.Y = previousMouseCoordinates.Y - currentMouseCoordinates.Y;
            offset.X = previousMouseCoordinates.X - currentMouseCoordinates.X;
            MoveScreenshotArea();
            previousMouseCoordinates = currentMouseCoordinates;
        }

        private void MoveScreenshotArea()
        {
            if (offset.Y > 0)
            {
                MoveScreenshotAreaUp();
            }
            if (offset.Y < 0)
            {
                MoveScreenshotAreaDown();
            }
            if (offset.X > 0)
            {
                MoveScreenshotAreaLeft();
            }
            if (offset.X < 0)
            {
                MoveScreenshotAreaRight();
            }
        }

        private void MoveScreenshotAreaUp()
        {
            if (rdUp.ActualHeight - offset.Y >= 0)
            {
                rdUp.Height = new GridLength(rdUp.ActualHeight - offset.Y);
            }
            rdDown.Height = new GridLength(rdDown.ActualHeight + offset.Y);
        }

        private void MoveScreenshotAreaDown()
        {
            if (rdDown.ActualHeight + offset.Y >= 0)
            {
                rdDown.Height = new GridLength(rdDown.ActualHeight + offset.Y);
            }
            rdUp.Height = new GridLength(rdUp.ActualHeight - offset.Y);
        }

        private void MoveScreenshotAreaLeft()
        {
            if (cdLeft.ActualWidth - offset.X >= 0)
            {
                cdLeft.Width = new GridLength(cdLeft.ActualWidth - offset.X);
            }
            cdRight.Width = new GridLength(cdRight.ActualWidth + offset.X);
        }

        private void MoveScreenshotAreaRight()
        {
            if (cdRight.ActualWidth + offset.X >= 0)
            {
                cdRight.Width = new GridLength(cdRight.ActualWidth + offset.X);
            }
            cdLeft.Width = new GridLength(cdLeft.ActualWidth - offset.X);
        }
    }
}
