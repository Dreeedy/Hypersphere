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
    /// <summary>
    /// Interaction logic for PaintUC.xaml
    /// </summary>
    public partial class PaintUC : UserControl
    {
        #region Public_Properties
        public UIElementCollection elementCollection
        {
            get; set;
        }
        #endregion Public_Properties



        #region Private_Fields
        private bool isAnyBrushDraw;
        private Dictionary<string, bool> allElementsForDrawingDictionary;
        private Image image;
        #endregion Private_Fields



        #region Public_Functions
        public PaintUC()
        {
            InitializeComponent();

            allElementsForDrawingDictionary = new Dictionary<string, bool>();
            allElementsForDrawingDictionary.Add(pencilImage.Name, false);
            allElementsForDrawingDictionary.Add(lineImale.Name, false);
            allElementsForDrawingDictionary.Add(arrowImage.Name, false);
            allElementsForDrawingDictionary.Add(rectangleImage.Name, false);
            allElementsForDrawingDictionary.Add(markerImage.Name, false);
            allElementsForDrawingDictionary.Add(textImage.Name, false);
            // TODO: отдельную ветку для "выбор цвета" и тд
        }

        public bool IsAnyBrushDraw()
        {
            isAnyBrushDraw = ChekIsAnyBrushDraw();
            return isAnyBrushDraw;
        }

        public bool IsPencilDraw()
        {
            return allElementsForDrawingDictionary[pencilImage.Name];
        }
        public bool IsLineDraw()
        {
            return allElementsForDrawingDictionary[lineImale.Name];
        }
        public bool IsArrowDraw()
        {
            return allElementsForDrawingDictionary[arrowImage.Name];
        }
        public bool IsRectangleDraw()
        {
            return allElementsForDrawingDictionary[rectangleImage.Name];
        }
        public bool IsMarkerDraw()
        {
            return allElementsForDrawingDictionary[markerImage.Name];
        }
        public bool IsTextDraw()
        {
            return allElementsForDrawingDictionary[textImage.Name];
        }
        #endregion Public_Functions



        #region Private_Functions
        private bool ChekIsAnyBrushDraw()
        {
            foreach (var item in allElementsForDrawingDictionary)
            {
                if (item.Value == true)
                {
                    return true;
                }                
            }
            return false;
        }      

        private void RemoveLastChildren(UIElementCollection elementCollection)
        {
            int count = elementCollection.Count;
            elementCollection.RemoveAt(count - 1);
        }     

        private void DisableAllElementsForDrawing()
        {
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
            allElementsForDrawingDictionary[image.Name] = false;
        }

        private void DisableLineImage()
        {
            Image image = lineImale;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_disabled.png"));
            allElementsForDrawingDictionary[image.Name] = false;
        }

        private void DisableArrowImage()
        {
            Image image = arrowImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_disabled.png"));
            allElementsForDrawingDictionary[image.Name] = false;
        }

        private void DisableRectangleImage()
        {
            Image image = rectangleImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_disabled.png"));
            allElementsForDrawingDictionary[image.Name] = false;
        }

        private void DisableMarkerImage()
        {
            Image image = markerImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_disabled.png"));
            allElementsForDrawingDictionary[image.Name] = false;
        }

        private void DisableTextImage()
        {
            Image image = textImage;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_disabled.png"));
            allElementsForDrawingDictionary[image.Name] = false;
        }
        #endregion Private_Functions



        #region Event_handlers
        private void pencilImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                DisablePencilImage();
            }
            else
            {
                DisableAllElementsForDrawing();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/pencil_32x32_enabled.png"));
                allElementsForDrawingDictionary[image.Name] = true;
            }
        }

        private void lineImale_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                DisableLineImage();
            }
            else
            {
                DisableAllElementsForDrawing();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_enabled.png"));
                allElementsForDrawingDictionary[image.Name] = true;
            }
        }

        private void arrowImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                DisableArrowImage();
            }
            else
            {
                DisableAllElementsForDrawing();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_enabled.png"));
                allElementsForDrawingDictionary[image.Name] = true;
            }
        }

        private void rectangleImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                DisableRectangleImage();
            }
            else
            {
                DisableAllElementsForDrawing();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_enabled.png"));
                allElementsForDrawingDictionary[image.Name] = true;
            }
        }

        private void markerImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                DisableMarkerImage();
            }
            else
            {
                DisableAllElementsForDrawing();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_enabled.png"));
                allElementsForDrawingDictionary[image.Name] = true;
            }
        }

        private void textImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                DisableTextImage();
            }
            else
            {
                DisableAllElementsForDrawing();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_enabled.png"));
                allElementsForDrawingDictionary[image.Name] = true;
            }
        }

        private void undoImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (elementCollection == null)
            {
                return;
            }
            RemoveLastChildren(elementCollection);
        }

        private void pencilImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/pencil_32x32_enabled.png"));
        }

        private void pencilImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/pencil_32x32_disabled.png"));
        }

        private void lineImale_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_enabled.png"));
        }

        private void lineImale_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/line_32x32_disabled.png"));
        }

        private void arrowImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_enabled.png"));
        }

        private void arrowImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/arrow_32x32_disabled.png"));
        }

        private void rectangleImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_enabled.png"));
        }

        private void rectangleImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/rectangle_32x32_disabled.png"));
        }

        private void markerImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_enabled.png"));
        }

        private void markerImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/marker_32x32_disabled.png"));
        }

        private void textImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_enabled.png"));
        }

        private void textImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            if (allElementsForDrawingDictionary[image.Name])
            {
                return;
            }
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/text_32x32_disabled.png"));
        }

        private void undoImage_MouseEnter(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/undo_32x32_enabled.png"));
        }

        private void undoImage_MouseLeave(object sender, MouseEventArgs e)
        {
            image = sender as Image;
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Hypersphere;component/Resource/Icons/undo_32x32_disabled.png"));
        }

        private void colorImage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectedColor.ColorDialogAndSetBrushColor(colorImage);
        }
        #endregion Event_handlers
    }
}
