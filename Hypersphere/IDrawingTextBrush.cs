using System.Windows;
using System.Windows.Controls;

namespace Hypersphere
{
    interface IDrawingTextBrush
    {
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint);
    }
}
