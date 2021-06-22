using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace Hypersphere
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        private static KeyboardHookManager keyboardHookManager;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // создаем значок уведомления (это ресурс, объявленный в NotifyIconResources.xaml
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            keyboardHookManager = new KeyboardHookManager();
            keyboardHookManager.Start();

            RegisterHotkeyAsync();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); // очистить память от значка
            base.OnExit(e);
        }

        static void PerformRegisterHotkey()
        {
            // 0x2C - print screen virutal code
            keyboardHookManager.RegisterHotkey(ModifierKeys.Control, 0x2C, () =>
            {
                Debug.WriteLine("Ctrl+Print Screen detected");
            });

        }

        static async void RegisterHotkeyAsync()
        {  
            await Task.Run(() => PerformRegisterHotkey());
        }
    }
}
