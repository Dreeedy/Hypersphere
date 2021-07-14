using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    class DrawingPencil : IDrawingPencil
    {
        Path path;
        GeometryGroup geometryGroup;

        public void CreatePencil(Canvas canvasForDraw)
        {
            // TODO: нормальная инициализация карандаша
            // TODO: максимально вынести рисование в отдельный класс
            path = new Path();
            path.Stroke = SelectedColor.SelectedSolidColorBrush;
            path.StrokeThickness = 2;

            geometryGroup = new GeometryGroup();            
            path.Data = geometryGroup;

            canvasForDraw.Children.Add(path);
        }

        public void DrawLineGeometry(Point start, Point end)
        {
            LineGeometry line = new LineGeometry();
            line.StartPoint = start;
            line.EndPoint = end;

            geometryGroup.Children.Add(line);
        }
    }
}
