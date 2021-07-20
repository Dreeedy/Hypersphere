using System.Windows;
using System.Windows.Input;

namespace Hypersphere
{  
    class MouseCoordinates : IMouseCoordinates
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private UIElement _spaceForMouse;

        private Point _previousMouseCoordinates;
        private Point _currentMouseCoordinates;
        private Point _offsetMouseCoordinates;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public MouseCoordinates()
        {
            _previousMouseCoordinates = new Point();
            _currentMouseCoordinates = new Point();
            _offsetMouseCoordinates = new Point();
        }
        public void SetPreviousMouseCoordinates(UIElement spaceForMouse, MouseButtonEventArgs e)
        {
            _spaceForMouse = spaceForMouse;
            _previousMouseCoordinates = e.GetPosition(spaceForMouse);
        }
        public Point GetPreviousMouseCoordinates()
        {
            return _previousMouseCoordinates;
        }
        public void SetCurrentMouseCoordinates(UIElement spaceForMouse, MouseEventArgs e)
        {
            _spaceForMouse = spaceForMouse;
            _currentMouseCoordinates = e.GetPosition(spaceForMouse);
        }
        public Point GetCurrentMouseCoordinates()
        {
            return _currentMouseCoordinates;
        }
        public void СalculateOffsetMouseCoordinates()
        {
            _offsetMouseCoordinates.Y = _previousMouseCoordinates.Y - _currentMouseCoordinates.Y;
            _offsetMouseCoordinates.X = _previousMouseCoordinates.X - _currentMouseCoordinates.X;
        }
        public void UpdatePreviousMouseCoordinates()
        {
            _previousMouseCoordinates = _currentMouseCoordinates;
        }
        public Point GetOffsetMouseCoordinates()
        {
            return _offsetMouseCoordinates;
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers    
    }
}
