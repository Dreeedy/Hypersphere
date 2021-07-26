using System.Windows;
using System.Windows.Input;
using Hypersphere.UserControls;

namespace Hypersphere
{
    /// <summary>
    /// Предоставляет связываемые свойства и команды для NotifyIcon.
    /// В этом примере модель представления назначается NotifyIcon в XAML.
    /// В качестве альтернативы, маршрутизация при запуске в App.xaml.cs могла создать эту модель представления и назначить ее NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields
        SettingsMenuUC _settingsMenuUC;
        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        /// <summary>
        /// Показывает окно, если оно еще не открыто.
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }
        /// <summary>
        /// Скрывает главное окно. Эта команда доступна, только если окно открыто.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }
        /// <summary>
        /// Закрывает приложение.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }
        /// <summary>
        /// Открывает панель настроек
        /// </summary>
        public ICommand ShowSettingsCommand
        {
            get// TODO: меню настроек
            {
                return new DelegateCommand
                {                    
                    CommandAction = () =>
                    {
                        _settingsMenuUC = new SettingsMenuUC();
                        _settingsMenuUC.Show();                      
                    }
                };
            }
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
