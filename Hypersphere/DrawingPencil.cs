using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    class DrawingPencil : IDrawingPencil
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private Path _path;
        private GeometryGroup _geometryGroup;
        private SelectedColor _selectedColor;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public DrawingPencil()
        {
            _selectedColor = new SelectedColor();
        }
        public void CreatePencil(Canvas canvasForDraw)
        {
            // TODO: нормальная инициализация карандаша
            // TODO: максимально вынести рисование в отдельный класс
            _path = new Path();
            _path.Stroke = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
            _path.StrokeThickness = 2;

            _geometryGroup = new GeometryGroup();
            _path.Data = _geometryGroup;

            canvasForDraw.Children.Add(_path);
        }
        public void DrawLineGeometry(Point start, Point end)
        {
            LineGeometry line = new LineGeometry();
            line.StartPoint = start;
            line.EndPoint = end;

            _geometryGroup.Children.Add(line);
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
