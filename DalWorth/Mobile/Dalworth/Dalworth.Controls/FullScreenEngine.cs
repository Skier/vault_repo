using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;

namespace Dalworth.Controls
{
    public class FullScreenEngine
    {
        private static FullScreenEngine m_instance = new FullScreenEngine(); 
        private static Form m_form; 

        private FullScreenEngine(){}

        private const int SPI_SETWORKAREA = 47;
        private const int SPI_GETWORKAREA = 48;
        private const int SPIF_UPDATEINIFILE = 0x01;

//        private static IntPtr hWndInputPanel;
//        private static IntPtr hWndSipButton;
        private static IntPtr hWndTaskBar;
        private static RECT rtDesktop;
        private static RECT rtNewDesktop;
        private static RECT rtInputPanel;
        private static RECT rtSipButton;
        private static RECT rtTaskBar;

        [DllImport("coredll.dll")]
        extern private static IntPtr FindWindowW(string lpClassName, string lpWindowName);

        [DllImport("coredll.dll")]
        extern private static int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, int bRepaint);

        [DllImport("coredll.dll")]
        extern private static int SetRect(ref RECT lprc, int xLeft, int yTop, int xRight, int yBottom);

        [DllImport("coredll.dll")]
        extern private static int GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("coredll.dll")]
        extern private static int SystemParametersInfo(int uiAction, int uiParam, ref RECT pvParam, int fWinIni);

        public static int InitFullScreen(Form form)
        {
            m_form = form;

            // // Declare & Instatiate local variable
            int Result = 0;
            try
            {
                if ((SystemParametersInfo(SPI_GETWORKAREA, 0, ref rtDesktop, 0) == 1))
                {
                    // // Successful obtain the system working area (Desktop)
                    SetRect(ref rtNewDesktop, 0, 0, 240, 320);
                }
//                // // Find the Input panel window handle
//                hWndInputPanel = FindWindowW("SipWndClass", null);
//                // // Checking...
//                if ((hWndInputPanel.ToInt64() != 0))
//                {
//                    // // Get the original Input panel window size
//                    GetWindowRect(hWndInputPanel, ref rtInputPanel);
//                }
//                // // Find the SIP Button window handle
//                hWndSipButton = FindWindowW("MS_SIPBUTTON", null);
//                // // Checking...
//                if ((hWndSipButton.ToInt64() != 0))
//                {
//                    // // Get the original Input panel window size
//                    GetWindowRect(hWndSipButton, ref rtSipButton);
//                }
                // // Find the Taskbar window handle
                hWndTaskBar = FindWindowW("HHTaskBar", null);
                // // Checking...
                if ((hWndTaskBar.ToInt64() != 0))
                {
                    // // Get the original Input panel window size
                    GetWindowRect(hWndTaskBar, ref rtTaskBar);
                }
            }
            catch (Exception ex)
            {
                // // PUT YOUR ERROR LOG CODING HERE
                // // Set return value
                Result = 1;
            }
            // // Return result code
            return Result;
        }

        private int DoFullScreen(bool mode)
        {
            // // Declare & Instatiate local variable
            int Result = 0;
            try
            {
                if ((mode == true))
                {
                    // // Update window working area size
                    SystemParametersInfo(SPI_SETWORKAREA, 0, ref rtNewDesktop, SPIF_UPDATEINIFILE);
                    if ((hWndTaskBar.ToInt64() != 0))
                    {
                        // // Hide the TaskBar
                        MoveWindow(hWndTaskBar, 0, rtNewDesktop.bottom, (rtTaskBar.right - rtTaskBar.left), (rtTaskBar.bottom - rtTaskBar.top), 0);
                    }
//                    if ((hWndInputPanel.ToInt64() != 0))
//                    {
//                        // // Reposition the input panel 
//                        MoveWindow(hWndInputPanel, 0, (rtNewDesktop.bottom
//                        - (rtInputPanel.bottom - rtInputPanel.top)), (rtInputPanel.right - rtInputPanel.left), (rtInputPanel.bottom - rtInputPanel.top), 0);
//                    }
//                    if ((hWndSipButton.ToInt64() != 0))
//                    {
//                        // // Hide the SIP button 
//                        MoveWindow(hWndSipButton, 0, rtNewDesktop.bottom, (rtSipButton.right - rtSipButton.left), (rtSipButton.bottom - rtSipButton.top), 0);
//                    }
                }
                else
                {
                    // // Update window working area size
                    SystemParametersInfo(SPI_SETWORKAREA, 0, ref rtDesktop, SPIF_UPDATEINIFILE);
                    // // Restore the TaskBar
                    if ((hWndTaskBar.ToInt64() != 0))
                    {
                        MoveWindow(hWndTaskBar, rtTaskBar.left, rtTaskBar.top, (rtTaskBar.right - rtTaskBar.left), (rtTaskBar.bottom - rtTaskBar.top), 0);
                    }
//                    // // Restore the input panel
//                    if ((hWndInputPanel.ToInt64() != 0))
//                    {
//                        MoveWindow(hWndInputPanel, rtInputPanel.left, (rtDesktop.bottom
//                        - ((rtInputPanel.bottom - rtInputPanel.top)
//                        - (rtTaskBar.bottom - rtTaskBar.top))), (rtInputPanel.right - rtInputPanel.left), (rtInputPanel.bottom - rtInputPanel.top), 0);
//                    }
//                    if ((hWndSipButton.ToInt64() != 0))
//                    {
//                        // // Restore the SIP button 
//                        MoveWindow(hWndSipButton, rtSipButton.left, rtSipButton.top, (rtSipButton.right - rtSipButton.left), (rtSipButton.bottom - rtSipButton.top), 0);
//                    }
                }
            }
            catch (Exception ex)
            {
                // // PUT YOUR ERROR LOG CODING HERE
                // // Set return value
                Result = 1;
            }
            // // Return result code
            return Result;
        }

        public static void Activate()
        {
            if (m_instance.DoFullScreen(true) == 0)
            {
                WinAPI.MoveWindow(WinAPI.FindWindowW(null, m_form.Text), 0, 0, 240, 294, 1);
            }            
        }

        public static void Dactivate()
        {
            if (m_instance.DoFullScreen(false) == 0)
            {
                WinAPI.MoveWindow(WinAPI.FindWindowW(null, m_form.Text), 0, 26, 240, 294, 1);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
