using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Dalworth.Controls
{
    public enum VideoPowerState : uint
    {
        VideoPowerOn = 1,
        VideoPowerStandBy,
        VideoPowerSuspend,
        VideoPowerOff
    }

    public class Video
    {
        private const uint SETPOWERMANAGEMENT = 6147;

        public static void PowerOff()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            byte[] vpm = new byte[13] {12, 0, 0, 0, 1, 0, 0, 0, (byte)VideoPowerState.VideoPowerOff, 0, 0, 0, 0};
            ExtEscapeSet(hdc, SETPOWERMANAGEMENT, 12, vpm, 0, IntPtr.Zero);
        }

        public static void PowerOn()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            byte[] vpm = new byte[13] {12, 0, 0, 0, 1, 0, 0, 0, (byte)VideoPowerState.VideoPowerOn, 0, 0, 0, 0};
            ExtEscapeSet(hdc, SETPOWERMANAGEMENT, 12, vpm, 0, IntPtr.Zero);
        }

        [DllImport("coredll", EntryPoint = "ExtEscape")]
        private static extern int ExtEscapeSet(
            IntPtr hdc,
            uint nEscape,
            uint cbInput,
            byte[] lpszInData,
            int cbOutput,
            IntPtr lpszOutData
            );

        [DllImport("coredll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

    }
}
