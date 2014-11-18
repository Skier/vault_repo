using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SmartSchedule.Windows
{

    public static class WinAPI
    {


        #region DeviceID
        private static Int32 METHOD_BUFFERED = 0;
        private static Int32 FILE_ANY_ACCESS = 0;
        private static Int32 FILE_DEVICE_HAL = 0x00000101;

        private const Int32 ERROR_NOT_SUPPORTED = 0x32;
        private const Int32 ERROR_INSUFFICIENT_BUFFER = 0x7A;

        private static Int32 IOCTL_HAL_GET_DEVICEID =
            ((FILE_DEVICE_HAL) << 16) | ((FILE_ANY_ACCESS) << 14)
            | ((21) << 2) | (METHOD_BUFFERED);

        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool KernelIoControl(Int32 dwIoControlCode,
            IntPtr lpInBuf, Int32 nInBufSize, byte[] lpOutBuf,
            Int32 nOutBufSize, ref Int32 lpBytesReturned);

        public static string GetDeviceID()
        {
            // Initialize the output buffer to the size of a 
            // Win32 DEVICE_ID structure.
            byte[] outbuff = new byte[20];
            Int32 dwOutBytes;
            bool done = false;

            Int32 nBuffSize = outbuff.Length;

            // Set DEVICEID.dwSize to size of buffer.  Some platforms look at
            // this field rather than the nOutBufSize param of KernelIoControl
            // when determining if the buffer is large enough.
            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
            dwOutBytes = 0;

            // Loop until the device ID is retrieved or an error occurs.
            while (!done)
            {
                if (KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero,
                    0, outbuff, nBuffSize, ref dwOutBytes))
                {
                    done = true;
                }
                else
                {
                    int error = Marshal.GetLastWin32Error();
                    switch (error)
                    {
                        case ERROR_NOT_SUPPORTED:
                            throw new NotSupportedException(
                                "IOCTL_HAL_GET_DEVICEID is not supported on this device");

                        case ERROR_INSUFFICIENT_BUFFER:

                            // The buffer is not big enough for the data.  The
                            // required size is in the first 4 bytes of the output
                            // buffer (DEVICE_ID.dwSize).
                            nBuffSize = BitConverter.ToInt32(outbuff, 0);
                            outbuff = new byte[nBuffSize];

                            // Set DEVICEID.dwSize to size of buffer.  Some
                            // platforms look at this field rather than the
                            // nOutBufSize param of KernelIoControl when
                            // determining if the buffer is large enough.
                            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
                            break;

                        default:
                            throw new Exception("Unexpected error");
                    }
                }
            }

            // Copy the elements of the DEVICE_ID structure.
            Int32 dwPresetIDOffset = BitConverter.ToInt32(outbuff, 0x4);
            Int32 dwPresetIDSize = BitConverter.ToInt32(outbuff, 0x8);
            Int32 dwPlatformIDOffset = BitConverter.ToInt32(outbuff, 0xc);
            Int32 dwPlatformIDSize = BitConverter.ToInt32(outbuff, 0x10);
            StringBuilder sb = new StringBuilder();

            for (int i = dwPresetIDOffset;
                i < dwPresetIDOffset + dwPresetIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));
            }

            sb.Append("-");

            for (int i = dwPlatformIDOffset;
                i < dwPlatformIDOffset + dwPlatformIDSize; i++)
            {
                sb.Append(String.Format("{0:X2}", outbuff[i]));
            }
            return sb.ToString();
        }

        #endregion


        public const UInt32 SHDB_SHOW = 0x0001;
        public const UInt32 SHDB_HIDE = 0x0002;
        public const int GWL_STYLE = -16;
        public const UInt32 WS_NONAVDONEBUTTON = 0x00010000;
        public const UInt32 WM_LBUTTONUP = 0x0202;
        public const int GWL_WNDPROC = -4;
        public const int SPI_GETWORKAREA = 0x0030;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int SPI_GETSIPINFO = 225;
        public const int SPI_SETSIPINFO = 224;
        public const UInt32 SIPF_ON = 0x00000001;
        public const UInt32 SIPF_DISABLECOMPLETION = 0x08;
        public const int WM_KEYDOWN = 256;
        public const int WM_KEYUP = 257;
        public const int WM_CHAR = 258;
        public const int SHFS_HIDESIPBUTTON = 0x0008;
        public const int SHFS_SHOWSIPBUTTON = 0x0004;
        

        public delegate int WndProc(IntPtr hwnd, uint msg, uint wParam, int lParam);

        public static int HIWORD(int Number)
        {
            return ((Number >> 16) & 0xffff);
        }

        public static int LOWORD(int Number)
        {
            return (Number & 0xffff);
        }

        public static int MAKELONG(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        public static IntPtr MAKELPARAM(int LoWord, int HiWord)
        {
            return new IntPtr(MAKELONG(LoWord, HiWord));
        }


		[DllImport("user32.dll")]
        public static extern bool PostMessage(
            IntPtr hWnd,
            UInt32 Msg,
            UInt32 wParam,
            UInt32 lParam);

		[DllImport("user32.dll")]
        public static extern bool ShowWindow(
                IntPtr hWnd,
                UInt32 nCmdShow);


        [DllImport("user32.dll")]
        private static extern void MessageBeep(uint beepType);


        public enum MessageBeepType
        {
            MB_ICONHAND = 1,
            MB_ICONQUESTION = 32,
            MB_ICONEXCLAMATION = 48,
            MB_ICONASTERISK = 64,
            MB_ICONINFORMATION = 64,
            MB_ICONSTOP = 16
        }

        public enum ShowWindowConstants
        {
          SW_HIDE = 0,
          SW_SHOWNORMAL = 1,
          SW_NORMAL = 1,
          SW_SHOWMINIMIZED = 2,
          SW_SHOWMAXIMIZED = 3,
          SW_MAXIMIZE = 3,
          SW_SHOWNOACTIVATE = 4,
          SW_SHOW = 5,
          SW_MINIMIZE = 6,
          SW_SHOWMINNOACTIVE = 7,
          SW_SHOWNA = 8,
          SW_RESTORE = 9,
          SW_SHOWDEFAULT = 10,
          SW_FORCEMINIMIZE = 11,
          SW_MAX = 11
        }

        [DllImport("User32.dll")]
        public static extern int ShowWindowAsync(IntPtr hWnd, int swCommand);

        public static void MessageBeep(MessageBeepType type)
        {
            MessageBeep((uint)type);
        }


		[DllImport("user32.dll")]
        public extern static IntPtr SetWindowLong(
            IntPtr hwnd,
            int nIndex,
            IntPtr dwNewLong);
        
        
		[DllImport("user32.dll")]
        public static extern UInt32 GetWindowLong(
            IntPtr hWnd,
            int nIndex);

		[DllImport("user32.dll")]
        public static extern IntPtr FindWindow(
            string lpClassName,
            string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int CallWindowProc(
            IntPtr lpPrevWndFunc,
            IntPtr hwnd,
            uint msg,
            uint wParam,
            int lParam);

		[DllImport("user32.dll")]
        public static extern int CallWindowProc(
            IntPtr lpPrevWndFunc,
            IntPtr hwnd,
            uint msg,
            uint wParam,
            IntPtr lParam);

		[DllImport("user32.dll")]
        public extern static int DefWindowProc(
            IntPtr hwnd,
            uint msg,
            uint wParam,
            int lParam);

		[DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(
          UInt32 uiAction,
          UInt32 uiParam,
          IntPtr pvParam,
          UInt32 fWinIni
        );

		[DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(
          UInt32 uiAction,
          UInt32 uiParam,
          ref RECT pvParam,
          UInt32 fWinIni
        );

		[DllImport("user32.dll")]
        public static extern bool PtInRect(
        ref RECT lprc,
        POINT pt
        );


		[DllImport("user32.dll")]
        public static extern Boolean SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);        

        
		[DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        
        public static void CloseWindow(IntPtr hWnd)
        {
            PostMessage(hWnd, 0x0010, 0, 0);
        }
        public static void CloseWindow(Form form)
        {
            PostMessage(form.Handle, 0x0010, 0, 0);
        }

        public static void HideWindow(Form form)
        {
            ShowWindow(form.Handle, 0);
        }

    }       
}
