using System.Windows;
using System.Windows.Controls;

namespace Hypersphere
{
    interface IDrawingLine
    {
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint);
        public void UpdateEndPoint(Point endPoint);
    }
}
