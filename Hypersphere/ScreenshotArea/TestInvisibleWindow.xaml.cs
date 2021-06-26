using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Hypersphere.UserControls;

namespace Hypersphere
{
    /// <summary>
    /// Interaction logic for TestInvisibleWindow.xaml
    /// </summary>
    public partial class TestInvisibleWindow : Window
    {
        Point previousMouseCoordinates = new Point();
        Point currentMouseCoordinates = new Point();
        Point offset = new Point();
        UIElement dragObject;

        Canvas canvas;
        Point previousCoordinates = new Point();
        Point currentCoordinates = new Point();

        bool pres = false;

        PaintUC paintUC;
        SystemUC systemUC;

        public TestInvisibleWindow()
        {
            InitializeComponent();            
        }        

        private void children_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            dragObject = children as UIElement;
            previousMouseCoordinates = e.GetPosition(mainOwner);
            children.CaptureMouse();
        }

        private void children_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            dragObject = null;
            children.ReleaseMouseCapture();
        }

        private void mainOwner_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragObject == null)
            {
                return;
            }
            currentMouseCoordinates = e.GetPosition(mainOwner);
            offset.Y = previousMouseCoordinates.Y - currentMouseCoordinates.Y;
            offset.X = previousMouseCoordinates.X - currentMouseCoordinates.X;
            MoveScreenshotArea();
            previousMouseCoordinates = currentMouseCoordinates;
        }

        private void MoveScreenshotArea()
        {
            if (offset.Y > 0)
            {
                MoveScreenshotAreaUp();
            }
            if (offset.Y < 0)
            {
                MoveScreenshotAreaDown();
            }
            if (offset.X > 0)
            {
                MoveScreenshotAreaLeft();
            }
            if (offset.X < 0)
            {
                MoveScreenshotAreaRight();
            }
        }

        private void MoveScreenshotAreaUp()
        {
            if (rdUp.ActualHeight - offset.Y >= 0)
            {
                rdUp.Height = new GridLength(rdUp.ActualHeight - offset.Y);
            }
            rdDown.Height = new GridLength(rdDown.ActualHeight + offset.Y);
        }

        private void MoveScreenshotAreaDown()
        {
            if (rdDown.ActualHeight + offset.Y >= 0)
            {
                rdDown.Height = new GridLength(rdDown.ActualHeight + offset.Y);
            }
            rdUp.Height = new GridLength(rdUp.ActualHeight - offset.Y);
        }

        private void MoveScreenshotAreaLeft()
        {
            if (cdLeft.ActualWidth - offset.X >= 0)
            {
                cdLeft.Width = new GridLength(cdLeft.ActualWidth - offset.X);
            }
            cdRight.Width = new GridLength(cdRight.ActualWidth + offset.X);
        }

        private void MoveScreenshotAreaRight()
        {
            if (cdRight.ActualWidth + offset.X >= 0)
            {
                cdRight.Width = new GridLength(cdRight.ActualWidth + offset.X);
            }
            cdLeft.Width = new GridLength(cdLeft.ActualWidth - offset.X);
        }

        private void mainOwner_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void mainOwner_Loaded(object sender, RoutedEventArgs e)
        {
            if (god.Children.Contains(canvas))
            {
                return;
            }
            canvas = new Canvas();
            canvas.Height = mainOwner.ActualHeight;
            canvas.Width = mainOwner.ActualWidth;
            Background = new SolidColorBrush(Colors.Transparent);
            god.Children.Add(canvas);
        }        

        private void god_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            pres = true;
            previousCoordinates = e.GetPosition(mainOwner);
        }

        private void god_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // TODO: рисование
            if (pres == false)
            {
                return;
            }
            currentCoordinates = e.GetPosition(mainOwner);

            Line line = new Line();
            canvas.Children.Add(line);

            line.Stroke = new SolidColorBrush(Colors.Aqua);
            line.StrokeThickness = 2;            
            line.Y1 = previousCoordinates.Y;
            line.X1 = previousCoordinates.X;
            line.Y2 = currentCoordinates.Y;
            line.X2 = currentCoordinates.X;            

            previousCoordinates = currentCoordinates;

            CheckUserControlAndRemove();
        }

        private void god_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {            
            pres = false;

            CheckUserControlAndRemove();
            CreateAndAddUserControl();


        }

        private void CheckUserControlAndRemove()
        {
            if (paintUC != null)
            {
                canvas.Children.Remove(paintUC);
                paintUC = null;
            }
            if (systemUC != null)
            {
                canvas.Children.Remove(systemUC);
                systemUC = null;
            }
        }

        private void CreateAndAddUserControl()
        {
            // TODO: если UC выходит за границу экрана, помещать его внутрь области
            // TODO: динамическое перемещение UC в зависемости от доступного места
            // TODO: исключить пересечение UC друг с другом
            Point paintUCPoint = children.PointToScreen(new Point(0, 0));
            Point systemUCPoint = children.PointToScreen(new Point(0, 0));

            paintUCPoint.X += children.ActualWidth + 6;// 3px gridSplitter, и еще 3px
            paintUC = new PaintUC();// vertical
            Canvas.SetTop(paintUC, paintUCPoint.Y);
            Canvas.SetLeft(paintUC, paintUCPoint.X);
            canvas.Children.Add(paintUC);

            systemUCPoint.Y += children.ActualHeight + 6;// 3px gridSplitter, и еще 3px
            systemUC = new SystemUC();// horizontal
            Canvas.SetTop(systemUC, systemUCPoint.Y);
            Canvas.SetLeft(systemUC, systemUCPoint.X);
            canvas.Children.Add(systemUC);
        }
    }
}
