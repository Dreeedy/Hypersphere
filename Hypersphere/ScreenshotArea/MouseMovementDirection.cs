using System.Windows;
using System.Windows.Input;
using static Hypersphere.MouseDirectionEnum;

namespace Hypersphere
{
    class MouseMovementDirection : IMouseMovementDirection
    {
        Point previousPoint = new Point();
        Point currentPoint = new Point();
        MouseDirection direction;

        public MouseMovementDirection()
        {

        }

        public MouseDirection GetMouseDirection(UIElement mouseMotionDetectionArea, MouseEventArgs e)
        {
            currentPoint = e.GetPosition(mouseMotionDetectionArea);
            bool mouseMoved = (previousPoint != currentPoint);

            if (mouseMoved)
            {
                direction = DetermineMouseDirection();
                previousPoint = currentPoint;
                return direction;
            }
            else
            {
                return MouseDirection.None;
            }           
        }

        private MouseDirection DetermineMouseDirection()
        {
            // Mouse moved up
            if ((previousPoint.X == currentPoint.X) && (previousPoint.Y > currentPoint.Y))
                return MouseDirection.Up;

            // Mouse moved down
            if ((previousPoint.X == currentPoint.X) && (previousPoint.Y < currentPoint.Y))
                return MouseDirection.Down;

            // Mouse moved left
            if ((previousPoint.X > currentPoint.X) && (previousPoint.Y == currentPoint.Y))
                return MouseDirection.Left;

            // Mouse moved right
            if ((previousPoint.X < currentPoint.X) && (previousPoint.Y == currentPoint.Y))
                return MouseDirection.Right;

            // Mouse moved diagonally up-right
            if ((previousPoint.X < currentPoint.X) && (previousPoint.Y > currentPoint.Y))
                return MouseDirection.TopRight;

            // Mouse moved diagonally up-left
            if ((previousPoint.X > currentPoint.X) && (previousPoint.Y > currentPoint.Y))
                return MouseDirection.TopLeft;

            // Mouse moved diagonally down-right
            if ((previousPoint.X < currentPoint.X) && (previousPoint.Y < currentPoint.Y))
                return MouseDirection.BottomRight;

            // Mouse moved diagonally down-left
            if ((previousPoint.X > currentPoint.X) && (previousPoint.Y < currentPoint.Y))
                return MouseDirection.BottomLeft;

            // Mouse didn't move
            return MouseDirection.None;
        }
    }
}
