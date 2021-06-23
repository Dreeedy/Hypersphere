﻿using System;
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
            whiteBox = new WhiteBox();
            whiteBox.Height = 600;
            whiteBox.Width = 600;
            Canvas.SetLeft(whiteBox, 100);
            Canvas.SetTop(whiteBox, 100);

            whiteBox.PreviewMouseDown += WhiteBox_PreviewMouseDown;

            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(whiteBox);
        }        

        private void WhiteBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.dragObject = sender as UIElement;
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
            if (position.Y - this.offset.Y >= 0 &&
                position.Y - this.offset.Y + whiteBox.ActualHeight <= mainCanvas.ActualHeight)// ограничивает движение по вертикали
            {
                Canvas.SetTop(this.dragObject, position.Y - this.offset.Y);
            }
        }

        private void MoveHorizontally(Point position, object sender, MouseEventArgs e)
        {
            if (position.X - this.offset.X >= 0 &&
                position.X - this.offset.X + whiteBox.ActualWidth <= mainCanvas.ActualWidth)// ограничивает движение по горозонтали
            {
                Canvas.SetLeft(this.dragObject, position.X - this.offset.X);
            }
        }
    }
}
