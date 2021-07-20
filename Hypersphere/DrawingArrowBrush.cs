using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Hypersphere
{
    class DrawingArrowBrush : ITwoPointDrawingBrush
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private SelectedColor _selectedColor;

        private Point _startPoint;

        private Path _path;
        private GeometryGroup _geometryGroup;
        
        private LineGeometry _line;
        private RectangleGeometry _rectangle;
        private Size _rectangleSize;
        private RotateTransform _newRectangleRotateTransform;        
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public DrawingArrowBrush()
        {
            _selectedColor = new SelectedColor();
        }
        public void CreateAndSetPoints(Canvas canvasForDraw, Point startPoint)
        {
            _startPoint = startPoint;

            _path = new Path();
            _path.Stroke = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
            _path.Fill = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
            _path.StrokeThickness = 2;

            _geometryGroup = new GeometryGroup();
            _path.Data = _geometryGroup;

            _line = new LineGeometry();
            _line.StartPoint = startPoint;
            _line.EndPoint = startPoint;
            
            _rectangle = new RectangleGeometry();
            _rectangleSize = new Size(4, 4);
            _rectangle.Rect = new Rect(startPoint, _rectangleSize);

            _newRectangleRotateTransform = new RotateTransform();
            _newRectangleRotateTransform.Angle = CalculeAngle(_startPoint, startPoint);
            _rectangle.Transform = _newRectangleRotateTransform;

            _geometryGroup.Children.Add(_line);
            _geometryGroup.Children.Add(_rectangle);

            canvasForDraw.Children.Add(_path);
        }
        public void UpdateEndPoint(Point endPoint)
        {
            _line.EndPoint = endPoint;

            _rectangle.Rect = new Rect(endPoint, _rectangleSize);

            _newRectangleRotateTransform.Angle = CalculeAngle(_startPoint, endPoint) - 45;// Чтобы поворачивать "arrow" на угол
            _newRectangleRotateTransform.CenterX = endPoint.X;
            _newRectangleRotateTransform.CenterY = endPoint.Y;
            _rectangle.Transform = _newRectangleRotateTransform;
        }
        #endregion Public_Methods



        #region Private_Methods
        /// <summary>
        /// Чтобы вычислять угол между стартовой и конечно точкой
        /// </summary>
        private double CalculeAngle(Point startPoint, Point endPoint)
        {
            var deltaX = Math.Pow((endPoint.X - startPoint.X), 2);
            var deltaY = Math.Pow((endPoint.Y - startPoint.Y), 2);

            var radian = Math.Atan2((endPoint.Y - startPoint.Y), (endPoint.X - startPoint.X));
            var angle = (radian * (180 / Math.PI) + 360) % 360;

            return angle;
        }
        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
