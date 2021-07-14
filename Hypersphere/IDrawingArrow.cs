using System.Windows;
using System.Windows.Controls;

namespace Hypersphere
{
    interface IDrawingArrow
    {
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint);
        public void UpdateEndPoint(Point endPoint);
    }
}
