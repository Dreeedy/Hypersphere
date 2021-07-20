using System.Windows;
using System.Windows.Controls;

namespace Hypersphere
{
    interface IDrawingPencilBrush
    {
        public void CreatePencil(Canvas canvasForDraw);
        public void DrawLineGeometry(Point startPoint, Point endPoint);
    }
}
