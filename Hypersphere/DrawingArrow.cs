using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    class DrawingArrow
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private Path _path;
        private GeometryGroup _geometryGroup;
        private SelectedColor _selectedColor;
        private LineGeometry _line;
        private EllipseGeometry _ellipse;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public DrawingArrow()
        {
            _selectedColor = new SelectedColor();
        }
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint)
        {
            _path = new Path();
            _path.Stroke = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
            _path.StrokeThickness = 2;

            _geometryGroup = new GeometryGroup();
            _path.Data = _geometryGroup;

            _line = new LineGeometry();
            _line.StartPoint = startPoint;
            _line.EndPoint = startPoint;

            _ellipse = new EllipseGeometry();
            _ellipse.RadiusX = 5;
            _ellipse.RadiusY = 5;
            _ellipse.Center = startPoint;

            _geometryGroup.Children.Add(_line);
            _geometryGroup.Children.Add(_ellipse);


            canvasForDraw.Children.Add(_path);
        }
        public void UpdateEndPoint(Point endPoint)
        {
            Debug.WriteLine(endPoint);
            _line.EndPoint = endPoint;
            _ellipse.Center = endPoint;
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
