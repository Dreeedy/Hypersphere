using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Hypersphere.ScreenshotArea;

namespace Hypersphere
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // TODO: все приватные перенные должны начинаться с _
        // TODO: везде внедрить #region
        private TaskbarIcon _notifyIcon;
        private static KeyboardHookManager _keyboardHookManager;

        private static Mutex _instanceCheckMutex;

        private static ScreenshotWindow _screenshotWindow;



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // выключить если программа уже запущена
            if (!InstanceCheck())
            {
                Application.Current.Shutdown();
            }

            // создаем значок уведомления (это ресурс, объявленный в NotifyIconResources.xaml
            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            _keyboardHookManager = new KeyboardHookManager();
            _keyboardHookManager.Start();

            Application.Current.Dispatcher.Invoke(() => 
            {
                PerformRegisterHotkey();
            });
        }



        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose(); // очистить память от значка
            base.OnExit(e);
        }
        static void PerformRegisterHotkey()
        {            
            // 0x2C - print screen virutal code
            _keyboardHookManager.RegisterHotkey(ModifierKeys.Control, 0x2C, () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // чтобы закрывать если окно уже было открыто
                    if (_screenshotWindow != null)
                    {
                        _screenshotWindow.Close();
                    }
                    _screenshotWindow = new ScreenshotWindow();
                    _screenshotWindow.Show();
                });                

                _keyboardHookManager.SetScreenPhotographer(new ScreenPhotographer("A:\\myScreenshots\\",
                    "myImage",
                    System.Drawing.Imaging.ImageFormat.Png));
                //keyboardHookManager.NotityScreenPhotographer(); // сделать это но кнопку копировать

                Debug.WriteLine("Ctrl+Print Screen detected");
            });
        }
        
        private static bool InstanceCheck()
        {
            bool isNew;
            _instanceCheckMutex = new Mutex(true, "Hypersphere", out isNew);
            return isNew;
        }
    }
}
