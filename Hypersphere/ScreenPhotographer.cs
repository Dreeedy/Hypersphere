using System.Drawing;

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
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public ScreenPhotographer(string path, string imageName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            _path = path;
            _imageFormat = imageFormat;
            _imageName = imageName;
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
            _printscreen = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            _graphics = Graphics.FromImage(_printscreen as Image);
            _graphics.CopyFromScreen(0, 0, 0, 0, _printscreen.Size);
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
