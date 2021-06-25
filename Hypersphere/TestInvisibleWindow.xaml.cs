using System.Windows;
using System.Windows.Input;

namespace Hypersphere
{
    /// <summary>
    /// Interaction logic for TestInvisibleWindow.xaml
    /// </summary>
    public partial class TestInvisibleWindow : Window
    {
        IScreenshotAreaMover screenshotAreaMover;

        public TestInvisibleWindow()
        {
            InitializeComponent();
            screenshotAreaMover = new ScreenshotAreaMover(cdLeft, cdRight, rdUp, rdDown);
        }        

        private void children_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            UIElement element = sender as UIElement;
            screenshotAreaMover.CaptureScreenshotArea(element);
        }

        private void children_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            screenshotAreaMover.MoveScreenshotArea(mainOwner, e);
        }

        private void children_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            screenshotAreaMover.ReleaseScreenshotArea();
        }          
    }
}
