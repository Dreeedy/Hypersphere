using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace Hypersphere
{
    class AutomaticProgramStart
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields

        #endregion Private_Static_Fields     



        #region Private_Fields

        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public static bool GetAutomaticProgramStartStatus()
        {
            return Hypersphere.Properties.Settings.Default.AUTOMATIC_START;
        }
        public static void EnableAutomaticProgramStartValue()
        {            
            if (!Hypersphere.Properties.Settings.Default.AUTOMATIC_START)
            {
                Hypersphere.Properties.Settings.Default.AUTOMATIC_START = true;
                Hypersphere.Properties.Settings.Default.Save();
            }
        }
        public static void DisableAutomaticProgramStartValue()
        {
            if (Hypersphere.Properties.Settings.Default.AUTOMATIC_START)
            {
                Hypersphere.Properties.Settings.Default.AUTOMATIC_START = false;
                Hypersphere.Properties.Settings.Default.Save();
            }
        }
        /// <summary>
        /// Добавляет или удаляет программу из реестра windows
        /// </summary>
        public static bool SwithAutomaticProgramStart()
        {
            const string nameInRegistry = "Hypersphere";
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (Hypersphere.Properties.Settings.Default.AUTOMATIC_START)
                {
                    reg.SetValue(nameInRegistry, exePath);
                }
                else
                {
                    reg.DeleteValue(nameInRegistry);
                }
                reg.Flush();
                reg.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;            
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
