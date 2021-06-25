using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hypersphere
{
    class MouseMovementDirection
    {
        public UIElement mouseMotionDetectionArea
        {
            get; set;
        }

        private Point previousPoint = new Point();
        private Point currentPoint = new Point();

        private MouseDirection direction;

        public MouseMovementDirection()
        {

        }

        public enum MouseDirection
        {
            None,
            Up,
            Down,
            Left,
            Right,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
        }

        /// <summary>
        /// 
        /// </summary>
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
                return direction;
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
            //return MouseDirection.Up;

            // Mouse moved diagonally up-left
            if ((previousPoint.X > currentPoint.X) && (previousPoint.Y > currentPoint.Y))
                return MouseDirection.TopLeft;
            //return MouseDirection.Up;

            // Mouse moved diagonally down-right
            if ((previousPoint.X < currentPoint.X) && (previousPoint.Y < currentPoint.Y))
                return MouseDirection.BottomRight;
            //return MouseDirection.Down;

            // Mouse moved diagonally down-left
            if ((previousPoint.X > currentPoint.X) && (previousPoint.Y < currentPoint.Y))
                return MouseDirection.BottomLeft;
            //return MouseDirection.Down;

            // Mouse didn't move
            return MouseDirection.None;
        }
    }
}
