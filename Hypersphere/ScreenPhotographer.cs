using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Hypersphere
{
    class ScreenPhotographer : IScreenPhotographer
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        private Bitmap _printscreen;
        private Graphics _graphics;

        private string _path;
        private System.Drawing.Imaging.ImageFormat _imageFormat;

        private ScreenshotAreaSize _screenshotAreaSize;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public ScreenPhotographer(string path, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            _path = path;
            _imageFormat = imageFormat;

            _screenshotAreaSize = new ScreenshotAreaSize();
            // TODO: % качества в котором нужно сохранить изображение
            // TODO: формат изобаржения
            // TODO: передать путь, куда сохранить изображение
        }
        public void KeyCombinationPressed()
        {
            if (_path == null || _imageFormat == null)
            {
                return;
            }
            TakeTakeScreenshotAndAddToClipboard();
        }
        #endregion Public_Methods



        #region Private_Methods
        /// <summary>
        /// Добавляет screenshot в буфер обмена, не сохраняя на компьютер
        /// </summary>
        private void TakeTakeScreenshotAndAddToClipboard()
        {
            TakeScreenshot();
            AddScreenshotToClipboard();

            DisposeGraphic();
        }
        // TODO: вызов этой функции на диалог типо выбор папки куда сохранить
        private void TakeScreenshotAndSaveToFolder()
        {
            TakeScreenshot();
            SaveScreenshotToFolder();

            DisposeGraphic();
        }
        private void TakeScreenshot()
        {
            System.Drawing.Size size = _screenshotAreaSize.GetPrintscreenSize();
            int sourceY = _screenshotAreaSize.GetSourceY();
            int sourceX = _screenshotAreaSize.GetSourceX();

            _printscreen = new Bitmap(size.Width, size.Height);
            _graphics = Graphics.FromImage(_printscreen);
            _graphics.CopyFromScreen(sourceX, sourceY, 0, 0, _printscreen.Size);
        }
        private void AddScreenshotToClipboard()
        {
            Clipboard.Clear();
            Clipboard.SetImage(ConvertBitmap(_printscreen));        
        }
        private BitmapSource ConvertBitmap(Bitmap bitmap)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap
                (
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                );

            return bitmapSource;
        }
        private void SaveScreenshotToFolder()
        {
            string imageName = GenerateUniquleName();
            string imageExtension = DefineExtension();
            _printscreen.Save($"{_path}{imageName}{imageExtension}", _imageFormat);
        }
        private string GenerateUniquleName()
        {
            Guid guid = Guid.NewGuid();
            string uniquleName = guid.ToString();
            return uniquleName;
        }
        private string DefineExtension()
        {
            string imageExtension = "";
            if (_imageFormat == System.Drawing.Imaging.ImageFormat.Png)
            {
                imageExtension = ".png";
            }
            return imageExtension;
        }
        private void DisposeGraphic()
        {
            _graphics.Dispose();
            _printscreen.Dispose();
            _graphics = null;
            _printscreen = null;
        }
        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers 
    }
}
