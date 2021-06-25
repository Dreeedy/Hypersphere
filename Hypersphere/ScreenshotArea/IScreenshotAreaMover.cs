using System.Windows;
using System.Windows.Input;

namespace Hypersphere
{
    interface IScreenshotAreaMover
    {
        public void CaptureScreenshotArea(UIElement dragObject);
        public void MoveScreenshotArea(UIElement mouseMotionDetectionArea, MouseEventArgs e);
        public void ReleaseScreenshotArea();
    }
}
