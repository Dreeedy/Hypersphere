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
                return;
            }

            // создаем значок уведомления (это ресурс, объявленный в NotifyIconResources.xaml
            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            _keyboardHookManager = new KeyboardHookManager();
            _keyboardHookManager.Start();

            Application.Current.Dispatcher.Invoke(() => 
            {
                PerformRegisterHotkeyCtrlAndPrintScreen();
                PerformRegisterHotKeyEscape();
                PerformRegisterHotKeyCtrlAndC();
            });
        }



        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose(); // очистить память от значка
            base.OnExit(e);
        }
        static void PerformRegisterHotkeyCtrlAndPrintScreen()
        {
            // VK_SNAPSHOT - 0x2C - PRINT SCREEN key
            _keyboardHookManager.RegisterHotkey(ModifierKeys.Control, 0x2C, () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // чтобы закрывать если окно уже было открыто
                    CloseScreenshotWindow();

                    _screenshotWindow = new ScreenshotWindow();
                    _screenshotWindow.Show();                    
                });              

                Debug.WriteLine("CTRL + PRINT_SCREEN detected");
            });
        }
        private static void PerformRegisterHotKeyEscape()
        {
            // VK_ESCAPE - 0x1B - ESC key
            _keyboardHookManager.RegisterHotkey(0x1B, () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (_screenshotWindow != null && _screenshotWindow.IsActive)
                    {
                        CloseScreenshotWindow();
                    }                    
                });                

                Debug.WriteLine("ESC detected");
            });
        }
        private static void PerformRegisterHotKeyCtrlAndC()
        {
            // 0x43 - C key
            _keyboardHookManager.RegisterHotkey(ModifierKeys.Control, 0x43, () =>
            {              
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (_screenshotWindow != null && _screenshotWindow.IsActive)
                    {
                        _keyboardHookManager.SetScreenPhotographer(new ScreenPhotographer("A:\\myScreenshots\\",                            
                            System.Drawing.Imaging.ImageFormat.Png));

                        _keyboardHookManager.NotityScreenPhotographer();

                        CloseScreenshotWindow();
                        return;
                    }                    
                });

                Debug.WriteLine("CTRL + C detected");
            });
        }
        private static void CloseScreenshotWindow()
        {
            if (_screenshotWindow != null)
            {
                _screenshotWindow.Close();
                _screenshotWindow = null;
                return;
            }
        }

        private static bool InstanceCheck()
        {
            bool isNew;
            _instanceCheckMutex = new Mutex(true, "Hypersphere", out isNew);
            return isNew;
        }
    }
}
