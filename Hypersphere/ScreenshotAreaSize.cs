﻿using System.Drawing;

namespace Hypersphere
{
    /// <summary>
    /// Используется для хранения и передами размеров и позиции screenshotArea
    /// </summary>
    class ScreenshotAreaSize
    {
        #region Public_Static_Constants

        #endregion Public_Static_Constants



        #region Private_Static_Fields
        private static Size s_printscreenSize;
        private static int s_sourceY;
        private static int s_sourceX;
        #endregion Private_Static_Fields     



        #region Private_Fields

        #endregion Private_Fields



        #region Properties

        #endregion Properties



        #region Public_Methods
        public ScreenshotAreaSize()
        {
        
        }
        public Size GetPrintscreenSize()
        {
            return s_printscreenSize;
        }
        public int GetSourceY()
        {
            return s_sourceY;
        }
        public int GetSourceX()
        {
            return s_sourceX;
        }
        public void SetPrintscreenSize(Size size)
        {
            s_printscreenSize = size;

            return;
        }
        public void SetSourceY(int value)
        {
            s_sourceY = value;

            return;
        }
        public void SetSourceX(int value)
        {
            s_sourceX = value;

            return;
        }
        #endregion Public_Methods



        #region Private_Methods

        #endregion Private_Methods



        #region Event_handlers

        #endregion Event_handlers
    }
}
