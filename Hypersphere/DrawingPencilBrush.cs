using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    class DrawingPencilBrush : IDrawingPencilBrush
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private SelectedColor _selectedColor;

        private Path _path;
        private GeometryGroup _geometryGroup;        
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public DrawingPencilBrush()
        {
            _selectedColor = new SelectedColor();
        }
        public void CreatePencil(Canvas canvasForDraw)
        {
            _path = new Path();
            _path.Stroke = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
            _path.StrokeThickness = 2;

            _geometryGroup = new GeometryGroup();
            _path.Data = _geometryGroup;

            canvasForDraw.Children.Add(_path);
        }
        public void DrawLineGeometry(Point startPoint, Point endPoint)
        {
            LineGeometry line = new LineGeometry();
            line.StartPoint = startPoint;
            line.EndPoint = endPoint;

            _geometryGroup.Children.Add(line);
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
