using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    class DrawingMarker
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
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public DrawingMarker()
        {
            _selectedColor = new SelectedColor();
        }
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint)
        {
            _path = new Path();
            _path.Stroke = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
            _path.StrokeThickness = 16;
            _path.Opacity = 0.30;

            _geometryGroup = new GeometryGroup();
            _path.Data = _geometryGroup;

            _line = new LineGeometry();
            _line.StartPoint = startPoint;
            _line.EndPoint = startPoint;

            _geometryGroup.Children.Add(_line);

            canvasForDraw.Children.Add(_path);
        }
        public void UpdateEndPoint(Point endPoint)
        {
            _line.EndPoint = endPoint;
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers

    }
}
