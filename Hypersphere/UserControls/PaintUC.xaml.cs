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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hypersphere.UserControls
{
    public partial class PaintUC : UserControl
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private bool _isAnyBrushDraw;
        private Dictionary<string, bool> _allElementsForDrawingDictionary;
        private Image _image;
        private SelectedColor _selectedColor;
        #endregion Private_Fields



        #region Properties
        public UIElementCollection elementCollection
        {
            get; set;
        }
        #endregion Properties



        #region Public_Functions
        public PaintUC()
        {
            InitializeComponent();

            _allElementsForDrawingDictionary = new Dictionary<string, bool>();
            _allElementsForDrawingDictionary.Add(pencilImage.Name, false);
            _allElementsForDrawingDictionary.Add(lineImale.Name, false);
            _allElementsForDrawingDictionary.Add(arrowImage.Name, false);
            _allElementsForDrawingDictionary.Add(rectangleImage.Name, false);
            _allElementsForDrawingDictionary.Add(markerImage.Name, false);
            _allElementsForDrawingDictionary.Add(textImage.Name, false);

            _selectedColor = new SelectedColor();

            // TODO: refactor class
        }
        public bool IsAnyBrushDraw()
        {
            _isAnyBrushDraw = ChekIsAnyBrushDraw();
            return _isAnyBrushDraw;
        }
        public bool IsPencilDraw()
        {
            return _allElementsForDrawingDictionary[pencilImage.Name];
        }
        public bool IsLineDraw()
        {
            return _allElementsForDrawingDictionary[lineImale.Name];
        }
        public bool IsArrowDraw()
        {
            return _allElementsForDrawingDictionary[arrowImage.Name];
        }
        public bool IsRectangleDraw()
        {
            return _allElementsForDrawingDictionary[rectangleImage.Name];
        }
        public bool IsMarkerDraw()
        {
            return _allElementsForDrawingDictionary[markerImage.Name];
        }
        public bool IsTextDraw()
        {
            return _allElementsForDrawingDictionary[textImage.Name];
        }
        #endregion Public_Functions



        #region Private_Functions
        private bool ChekIsAnyBrushDraw()
        {
            foreach (var item in _allElementsForDrawingDictionary)
            {
                if (item.Value == true)
                {
                    return true;
                }
            }
            return false;
        }
        private void RemoveLastChildren()
        {
            if (elementCollection.Count > 2)// чтобы не удалял PaintUC
            {
                elementCollection.RemoveAt(elementCollection.Count - 1);                
            }
            if (elementCollection.Count > 2 && ChekIsAnyBrushDraw())// чтобы работал undo при активной кистиы
            {
                elementCollection.RemoveAt(elementCollection.Count - 1);
            }
        }
        private void DisableAllElementsForDrawing()
        {
            if (ChekIsAnyBrushDraw())
            {
                RemoveLastChildren();// удаляет лишний элемент
            }
            DisablePencilImage();
            DisableLineImage();
            DisableArrowImage();
            DisableRectangleImage();
            DisableMarkerImage();
            DisableTextImage();           
        }
        private void DisablePencilImage()
        {
            Image image = pencilImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/pencil_32x32_disabled.png"));
            _allElementsForDrawingDictionary[image.Name] = false;
        }
        private void DisableLineImage()
        {
            Image image = lineImale;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_disabled.png"));
            _allElementsForDrawingDictionary[image.Name] = false;
        }
        private void DisableArrowImage()
        {
            Image image = arrowImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_disabled.png"));
            _allElementsForDrawingDictionary[image.Name] = false;
        }
        private void DisableRectangleImage()
        {
            Image image = rectangleImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_disabled.png"));
            _allElementsForDrawingDictionary[image.Name] = false;
        }

        private void DisableMarkerImage()
        {
            Image image = markerImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_disabled.png"));
            _allElementsForDrawingDictionary[image.Name] = false;
        }
        private void DisableTextImage()
        {
            Image image = textImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_disabled.png"));
            _allElementsForDrawingDictionary[image.Name] = false;
        }
        #endregion Private_Functions



        #region Event_handlers
        private void pencilImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                DisablePencilImage();
                RemoveLastChildren();// удаляет лишний элемент
            }
            else
            {
                DisableAllElementsForDrawing();
                _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/pencil_32x32_enabled.png"));
                _allElementsForDrawingDictionary[_image.Name] = true;

                // проверка. если больше 2 то удаляем последний элемент
            }
        }
        private void lineImale_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                DisableLineImage();
                RemoveLastChildren();// удаляет лишний элемент
            }
            else
            {
                DisableAllElementsForDrawing();
                _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_enabled.png"));
                _allElementsForDrawingDictionary[_image.Name] = true;
            }
        }
        private void arrowImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                DisableArrowImage();
                RemoveLastChildren();// удаляет лишний элемент
            }
            else
            {
                DisableAllElementsForDrawing();
                _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_enabled.png"));
                _allElementsForDrawingDictionary[_image.Name] = true;                
            }
        }
        private void rectangleImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                DisableRectangleImage();
                RemoveLastChildren();// удаляет лишний элемент
            }
            else
            {
                DisableAllElementsForDrawing();
                _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_enabled.png"));
                _allElementsForDrawingDictionary[_image.Name] = true;
            }
        }
        private void markerImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                DisableMarkerImage();
                RemoveLastChildren();// удаляет лишний элемент
            }
            else
            {
                DisableAllElementsForDrawing();
                _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_enabled.png"));
                _allElementsForDrawingDictionary[_image.Name] = true;
            }
        }
        private void textImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                DisableTextImage();
                RemoveLastChildren();// удаляет лишний элемент
            }
            else
            {
                DisableAllElementsForDrawing();
                _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_enabled.png"));
                _allElementsForDrawingDictionary[_image.Name] = true;
            }
        }
        private void undoImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (elementCollection == null)
            {
                return;
            }
            RemoveLastChildren();
        }
        private void pencilImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/pencil_32x32_enabled.png"));
        }
        private void pencilImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/pencil_32x32_disabled.png"));
        }
        private void lineImale_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_enabled.png"));
        }
        private void lineImale_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_disabled.png"));
        }
        private void arrowImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_enabled.png"));
        }
        private void arrowImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_disabled.png"));
        }
        private void rectangleImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_enabled.png"));
        }
        private void rectangleImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_disabled.png"));
        }
        private void markerImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_enabled.png"));
        }
        private void markerImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_disabled.png"));
        }
        private void textImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_enabled.png"));
        }
        private void textImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            if (_allElementsForDrawingDictionary[_image.Name])
            {
                return;
            }
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_disabled.png"));
        }
        private void undoImage_MouseEnter(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/undo_32x32_enabled.png"));
        }
        private void undoImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _image = sender as Image;
            _image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/undo_32x32_disabled.png"));
        }
        private void colorImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _selectedColor.ShowColorDialogAndSetBrushColor();
            colorImage.Fill = _selectedColor.GetSelectedOrDefaultSolidColorBrush();
        }
        #endregion Event_handlers
    }
}
