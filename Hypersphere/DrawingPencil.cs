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
            path = new Path();
            path.Stroke = Brushes.Aquamarine;
            path.StrokeThickness = 2;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 77, 180, 121);
            path.Fill = mySolidColorBrush;

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
