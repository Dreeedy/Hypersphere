using System.Windows;
using System.Windows.Input;

namespace Hypersphere
{
    interface IMouseCoordinates
    {
        public void SetPreviousMouseCoordinates(UIElement spaceForMouse, MouseButtonEventArgs e);

        public Point GetPreviousMouseCoordinates();

        public void SetCurrentMouseCoordinates(UIElement spaceForMouse, MouseEventArgs e);

        public Point GetCurrentMouseCoordinates();

        public void СalculateOffsetMouseCoordinates();

        public void UpdatePreviousMouseCoordinates();

        public Point GetOffsetMouseCoordinates();
    }
}
