using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    class DrawingRectangleBrush : ITwoPointDrawingBrush
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private SelectedColor _selectedColor;

        private Point _startPoint;

        private Path _path;
        private GeometryGroup _geometryGroup;  
        
        private RectangleGeometry _rectangle;        
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public DrawingRectangleBrush()
        {
            _selectedColor = new SelectedColor();
        }
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint)
        {
            _startPoint = startPoint;

            _path = new Path();
            _path.Stroke = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
            _path.StrokeThickness = SelectedBrushThickness.STROKE_THICKNESS;

            _geometryGroup = new GeometryGroup();
            _path.Data = _geometryGroup;

            _rectangle = new RectangleGeometry();
            _rectangle.Rect = new Rect(_startPoint, startPoint);

            _geometryGroup.Children.Add(_rectangle);

            canvasForDraw.Children.Add(_path);
        }
        public void UpdateEndPoint(Point endPoint)
        {
            _rectangle.Rect = new Rect(_startPoint, endPoint);
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
