using System.Windows;
using System.Windows.Controls;

namespace Hypersphere
{
    interface ITwoPointDrawingBrush
    {
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint);
        public void UpdateEndPoint(Point endPoint);
    }
}
