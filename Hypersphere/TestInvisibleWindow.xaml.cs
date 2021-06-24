using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hypersphere.UserControls;

namespace Hypersphere
{
    /// <summary>
    /// Interaction logic for TestInvisibleWindow.xaml
    /// </summary>
    public partial class TestInvisibleWindow : Window
    {
        UIElement dragObject;
        Point offset;
        WhiteBox whiteBox;       

        public TestInvisibleWindow()
        {
            InitializeComponent();
        }

        private void mainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            // tblock.SetValue(Grid.RowProperty, 4);
            // Brushes.Transparent
            whiteBox = new WhiteBox();
            whiteBox.Height = 600;
            whiteBox.Width = 600;
            Canvas.SetLeft(whiteBox, 100);
            Canvas.SetTop(whiteBox, 100);

            whiteBox.mainGrid.PreviewMouseDown += WhiteBox_PreviewMouseDown;

            //mainCanvas.Children.Clear();
            mainCanvas.Children.Add(whiteBox);
        }        

        private void WhiteBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = whiteBox as UIElement;// чтобы нельзя было перемещать выделенную область за элементы управления (resize и тд)

            this.offset = e.GetPosition(this.mainCanvas);
            this.offset.Y -= Canvas.GetTop(this.dragObject);
            this.offset.X -= Canvas.GetLeft(this.dragObject);
            this.mainCanvas.CaptureMouse();
        }

        private void mainCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.dragObject == null)
            {
                return;
            }

            Point position = e.GetPosition(sender as IInputElement);

            MoveVertically(position, sender, e);            
            MoveHorizontally(position, sender, e);            
        }       

        private void mainCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = null;
            this.mainCanvas.ReleaseMouseCapture();
        }

        private void MoveVertically(Point position, object sender, MouseEventArgs e)
        {
            if (position.Y - this.offset.Y >= 0
                && position.Y - this.offset.Y + whiteBox.ActualHeight <= mainCanvas.ActualHeight)// ограничивает движение по вертикали
            {
                DrawVerticalBlackArea(position);
                DrawHorizontalBlackArea(position);
                Canvas.SetTop(this.dragObject, position.Y - this.offset.Y);
            }
        }

        private void DrawVerticalBlackArea(Point position)
        {            
            if (position.Y - this.offset.Y <= mainCanvas.ActualHeight - whiteBox.ActualHeight
                && position.Y - this.offset.Y >= 0)// вниз
            {
                topBorder.Height = position.Y - this.offset.Y;
            }
            if (mainCanvas.ActualHeight - topBorder.Height - whiteBox.ActualHeight >= 0)// вверх
            {
                bottomBorder.Height = mainCanvas.ActualHeight - topBorder.Height - whiteBox.ActualHeight;
            }            
        }

        private void DrawHorizontalBlackArea(Point position)
        {
            if (position.X - this.offset.X >= 0
                && position.X - this.offset.X <= mainCanvas.ActualWidth - whiteBox.ActualWidth)// вправо
            {
                leftBorder.Width = position.X - this.offset.X;
                Canvas.SetTop(leftBorder, topBorder.Height);
                leftBorder.Height = whiteBox.ActualHeight;
            }
            else
            {
                Canvas.SetTop(leftBorder, topBorder.Height);
                leftBorder.Height = whiteBox.ActualHeight;
            }

            if (mainCanvas.ActualWidth - leftBorder.Width - whiteBox.ActualWidth >= 0)// влево
            {
                rigthBorder.Width = mainCanvas.ActualWidth - leftBorder.Width - whiteBox.ActualWidth;
                Canvas.SetTop(rigthBorder, topBorder.Height);
                rigthBorder.Height = whiteBox.ActualHeight;
            }           
        }

        private void MoveHorizontally(Point position, object sender, MouseEventArgs e)
        {
            if (position.X - this.offset.X >= 0
                && position.X - this.offset.X + whiteBox.ActualWidth <= mainCanvas.ActualWidth)// ограничивает движение по горозонтали
            {
                DrawVerticalBlackArea(position);
                DrawHorizontalBlackArea(position);
                Canvas.SetLeft(this.dragObject, position.X - this.offset.X);
            }
        }
    }
}
