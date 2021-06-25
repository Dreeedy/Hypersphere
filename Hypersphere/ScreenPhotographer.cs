using System.Drawing;

namespace Hypersphere
{
    class ScreenPhotographer : IScreenPhotographer
    {
        Bitmap printscreen;
        Graphics graphics;

        string path;
        System.Drawing.Imaging.ImageFormat imageFormat;
        string imageName;

        public ScreenPhotographer(string path, string imageName, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            this.path = path;
            this.imageFormat = imageFormat;
            this.imageName = imageName;
            // TODO: передавать в конструктор размер области для скриншота
            // TODO: % качества в котором нужно сохранить изображение
            // TODO: формат изобаржения
            // TODO: передать путь, куда сохранить изображение
        }

        private void TakeScreenshotAndSave()
        {
            TakeScreenshot();
            SaveScreenshot();
        }

        private void TakeScreenshot()
        {
            printscreen = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            graphics.Dispose();
        }

        private void SaveScreenshot()
        {
            string imageExtension = ".png";
            if (imageFormat == System.Drawing.Imaging.ImageFormat.Png)
            {
                imageExtension = ".png";
            }
            // png ярче чем jpeg
            // ImageFormat - содержит множество других форматов
            printscreen.Save($"{path}{imageName}{imageExtension}", imageFormat);
            printscreen.Dispose();
        }

        public void KeyCombinationPressed()
        {
            if (path == null || imageFormat == null)
            {
                return;
            }
            TakeScreenshotAndSave();
        }
    }
}
