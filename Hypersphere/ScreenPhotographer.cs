using System.Drawing;
using System.Windows;

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
        private string _imageName;

        private ScreenshotAreaSize _screenshotAreaSize;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public ScreenPhotographer(string path, string imageName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            _path = path;
            _imageFormat = imageFormat;
            _imageName = imageName;

            _screenshotAreaSize = new ScreenshotAreaSize();
            // TODO: передавать в конструктор размер области для скриншота
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
            TakeScreenshotAndSave();
        }
        #endregion Public_Methods



        #region Private_Methods
        private void TakeScreenshotAndSave()
        {
            TakeScreenshot();
            SaveScreenshot();
        }
        private void TakeScreenshot()
        {
            System.Drawing.Size size = _screenshotAreaSize.GetPrintscreenSize();
            int sourceY = _screenshotAreaSize.GetSourceY();
            int sourceX = _screenshotAreaSize.GetSourceX();

            // TODO: сделать вызов функции или по нажатию кнопку или комбинации клавиш ( копировать )
            _printscreen = new Bitmap(size.Width, size.Height);
            _graphics = Graphics.FromImage(_printscreen);
            _graphics.CopyFromScreen(sourceX, sourceY, 0, 0, _printscreen.Size);
            _graphics.Dispose();
        }
        private void SaveScreenshot()
        {
            string imageExtension = ".png";
            if (_imageFormat == System.Drawing.Imaging.ImageFormat.Png)
            {
                imageExtension = ".png";
            }
            // png ярче чем jpeg
            // ImageFormat - содержит множество других форматов
            _printscreen.Save($"{_path}{_imageName}{imageExtension}", _imageFormat);
            _printscreen.Dispose();
        }
        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers 
    }
}
