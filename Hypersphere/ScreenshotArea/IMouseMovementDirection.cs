using System.Windows;
using System.Windows.Input;
using static Hypersphere.MouseDirectionEnum;

namespace Hypersphere
{
    interface IMouseMovementDirection
    {
        public MouseDirection GetMouseDirection(UIElement mouseMotionDetectionArea, MouseEventArgs e);
    }
}
