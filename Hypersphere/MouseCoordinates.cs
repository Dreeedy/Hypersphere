using System.Windows;
using System.Windows.Input;

namespace Hypersphere
{  
    class MouseCoordinates : IMouseCoordinates
    {
        Point previousMouseCoordinates = new Point();
        Point currentMouseCoordinates = new Point();
        Point offsetMouseCoordinates = new Point();

        UIElement spaceForMouse;

        public void SetPreviousMouseCoordinates(UIElement spaceForMouse, MouseButtonEventArgs e)
        {
            this.spaceForMouse = spaceForMouse;
            previousMouseCoordinates = e.GetPosition(spaceForMouse);
        }

        public Point GetPreviousMouseCoordinates()
        {
            return previousMouseCoordinates;
        }

        public void SetCurrentMouseCoordinates(UIElement spaceForMouse, MouseEventArgs e)
        {
            this.spaceForMouse = spaceForMouse;
            currentMouseCoordinates = e.GetPosition(spaceForMouse);
        }

        public Point GetCurrentMouseCoordinates()
        {
            return currentMouseCoordinates;
        }

        public void СalculateOffsetMouseCoordinates()
        {
            offsetMouseCoordinates.Y = previousMouseCoordinates.Y - currentMouseCoordinates.Y;
            offsetMouseCoordinates.X = previousMouseCoordinates.X - currentMouseCoordinates.X;
        }

        public void UpdatePreviousMouseCoordinates()
        {
            previousMouseCoordinates = currentMouseCoordinates;
        }

        public Point GetOffsetMouseCoordinates()
        {
            return offsetMouseCoordinates;
        }
    }
}
