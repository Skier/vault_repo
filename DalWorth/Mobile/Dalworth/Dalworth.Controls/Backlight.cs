using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Dalworth.Controls
{
    public class Backlight
    {
        private static IntPtr m_handle;

        private enum PowerState
        {
            PowerDeviceUnspecified = -1,
            FullOn = 0,
            LowPower = 1,
            StandBy = 2,
            Sleep = 3,
            Off = 4,
            PowerDeviceMaximum = 5
        }

        public static void Activate()
        {
            m_handle = SetPowerRequirement("BKL1:", (int)PowerState.FullOn, 1, IntPtr.Zero, 0);
        }

        public static void Release()
        {
            if (m_handle != IntPtr.Zero)
            {
                ReleasePowerRequirement(m_handle);
                m_handle = IntPtr.Zero;
            }            
        }

        [DllImport("coredll")]
        private static extern IntPtr SetPowerRequirement(string pvDevice, int powerState, int DeviceFlags,
            IntPtr pvSystemState, int StateFlags);

        [DllImport("coredll")]
        private static extern int ReleasePowerRequirement(IntPtr handle);

    }
}
