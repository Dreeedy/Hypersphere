using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Hypersphere
{
    /// <summary>
    /// Interaction logic for TestInvisibleWindow.xaml
    /// </summary>
    public partial class TestInvisibleWindow : Window
    {
        object dragObject;
        Point offset;

        Point previous = new Point();
        Point current = new Point();

        public enum MouseDirection
        {
            None,
            Up,
            Down,
            Left,
            Right,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
        }

        double mouseSensitivity = 1;

        public TestInvisibleWindow()
        {
            InitializeComponent();
            // TODO: сделать рефакторинг
        }

        

        private void children_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = sender as Grid;// чтобы нельзя было перемещать выделенную область за элементы управления (resize и тд)

            children.CaptureMouse();
        }

        private void children_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragObject == null)
            {
                return;
            }

            current = e.GetPosition(mainOwner);

            bool mouseMoved = (previous != current);

            if (mouseMoved)
            {
                MouseDirection direction = GetMouseDirection(previous, current);
                Move(direction);
            }
            previous = current;            
        }

        private void Move(MouseDirection direction)
        {
            if (direction == MouseDirection.Up)
            {
                MoveUp();
            }
            if (direction == MouseDirection.Down)
            {
                MoveDown();
            }
            if (direction == MouseDirection.Left)
            {
                MoveLeft();
            }
            if (direction == MouseDirection.Right)
            {
                MoveRight();
            }
            if (direction == MouseDirection.TopLeft)
            {
                MoveUp();
                MoveLeft();
            }
            if (direction == MouseDirection.TopRight)
            {
                MoveUp();
                MoveRight();
            }
            if (direction == MouseDirection.BottomLeft)
            {
                MoveDown();
                MoveLeft();
            }
            if (direction == MouseDirection.BottomRight)
            {
                MoveDown();
                MoveRight();
            }
        }

        private void MoveUp()
        {
            if (rfUp.ActualHeight - 1 >= 0)
            {
                rfUp.Height = new GridLength(rfUp.ActualHeight - (1 * mouseSensitivity));
            }
            rfDown.Height = new GridLength(rfDown.ActualHeight + (1 * mouseSensitivity));
        }

        private void MoveDown()
        {
            if (rfDown.ActualHeight - 1 >= 0)
            {
                rfDown.Height = new GridLength(rfDown.ActualHeight - (1 * mouseSensitivity));
            }
            rfUp.Height = new GridLength(rfUp.ActualHeight + (1 * mouseSensitivity));
        }

        private void MoveLeft()
        {
            if (dfLeft.ActualWidth - 1 >= 0)
            {
                dfLeft.Width = new GridLength(dfLeft.ActualWidth - (1 * mouseSensitivity));
            }
            dfRight.Width = new GridLength(dfRight.ActualWidth + (1 * mouseSensitivity));
        }
        private void MoveRight()
        {
            if (dfRight.ActualWidth - 1 >= 0)
            {
                dfRight.Width = new GridLength(dfRight.ActualWidth - (1 * mouseSensitivity));
            }
            dfLeft.Width = new GridLength(dfLeft.ActualWidth + (1 * mouseSensitivity));
        }

        private void children_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = null;
            children.ReleaseMouseCapture();
        }

        private MouseDirection GetMouseDirection(Point previous, Point current)
        {
            // Mouse moved up
            if ((previous.X == current.X) && (previous.Y > current.Y))
                return MouseDirection.Up;

            // Mouse moved down
            if ((previous.X == current.X) && (previous.Y < current.Y))
                return MouseDirection.Down;

            // Mouse moved left
            if ((previous.X > current.X) && (previous.Y == current.Y))
                return MouseDirection.Left;

            // Mouse moved right
            if ((previous.X < current.X) && (previous.Y == current.Y))
                return MouseDirection.Right;

            // Mouse moved diagonally up-right
            if ((previous.X < current.X) && (previous.Y > current.Y))
                return MouseDirection.TopRight;
            //return MouseDirection.Up;

            // Mouse moved diagonally up-left
            if ((previous.X > current.X) && (previous.Y > current.Y))
                return MouseDirection.TopLeft;
            //return MouseDirection.Up;

            // Mouse moved diagonally down-right
            if ((previous.X < current.X) && (previous.Y < current.Y))
                return MouseDirection.BottomRight;
            //return MouseDirection.Down;

            // Mouse moved diagonally down-left
            if ((previous.X > current.X) && (previous.Y < current.Y))
                return MouseDirection.BottomLeft;
            //return MouseDirection.Down;

            // Mouse didn't move
            return MouseDirection.None;
        }
    }
}
