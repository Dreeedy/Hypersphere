using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Hypersphere.MouseDirectionEnum;

namespace Hypersphere
{
    class ScreenshotAreaMover : IScreenshotAreaMover
    {
        IMouseMovementDirection mouseMovementDirection;

        UIElement dragObject;        
        double mouseSensitivity = 1;
        ColumnDefinition cdLeft;
        ColumnDefinition cdRight;
        RowDefinition rdUp;
        RowDefinition rdDown;

        public ScreenshotAreaMover(ColumnDefinition cdLeft, ColumnDefinition cdRight, RowDefinition rdUp, RowDefinition rdDown)
        {
            this.mouseMovementDirection = new MouseMovementDirection();
            this.cdLeft = cdLeft;
            this.cdRight = cdRight;
            this.rdUp = rdUp;
            this.rdDown = rdDown;
        }

        public void CaptureScreenshotArea(UIElement dragObject)
        {
            this.dragObject = dragObject;
            dragObject.CaptureMouse();
        }

        public void MoveScreenshotArea(UIElement mouseMotionDetectionArea, MouseEventArgs e)
        {
            if (this.dragObject == null)
            {
                return;
            }
            MouseDirection direction = mouseMovementDirection.GetMouseDirection(mouseMotionDetectionArea, e);
            Move(direction);
        }

        public void ReleaseScreenshotArea()
        {
            dragObject.ReleaseMouseCapture();
            this.dragObject = null;            
        }

        private void Move(MouseDirection direction)
        {
            if (direction == MouseDirection.Up)
            {
                MoveUp();
            }
            if (direction == MouseDirection.Down)
            {
                MoveDown();
            }
            if (direction == MouseDirection.Left)
            {
                MoveLeft();
            }
            if (direction == MouseDirection.Right)
            {
                MoveRight();
            }
            if (direction == MouseDirection.TopLeft)
            {
                MoveUp();
                MoveLeft();
            }
            if (direction == MouseDirection.TopRight)
            {
                MoveUp();
                MoveRight();
            }
            if (direction == MouseDirection.BottomLeft)
            {
                MoveDown();
                MoveLeft();
            }
            if (direction == MouseDirection.BottomRight)
            {
                MoveDown();
                MoveRight();
            }
        }

        private void MoveUp()
        {
            if (rdUp.ActualHeight - 1 >= 0)
            {
                rdUp.Height = new GridLength(rdUp.ActualHeight - (1 * mouseSensitivity));
            }
            rdDown.Height = new GridLength(rdDown.ActualHeight + (1 * mouseSensitivity));
        }

        private void MoveDown()
        {
            if (rdDown.ActualHeight - 1 >= 0)
            {
                rdDown.Height = new GridLength(rdDown.ActualHeight - (1 * mouseSensitivity));
            }
            rdUp.Height = new GridLength(rdUp.ActualHeight + (1 * mouseSensitivity));
        }

        private void MoveLeft()
        {
            if (cdLeft.ActualWidth - 1 >= 0)
            {
                cdLeft.Width = new GridLength(cdLeft.ActualWidth - (1 * mouseSensitivity));
            }
            cdRight.Width = new GridLength(cdRight.ActualWidth + (1 * mouseSensitivity));
        }

        private void MoveRight()
        {
            if (cdRight.ActualWidth - 1 >= 0)
            {
                cdRight.Width = new GridLength(cdRight.ActualWidth - (1 * mouseSensitivity));
            }
            cdLeft.Width = new GridLength(cdLeft.ActualWidth + (1 * mouseSensitivity));
        }
    }
}
