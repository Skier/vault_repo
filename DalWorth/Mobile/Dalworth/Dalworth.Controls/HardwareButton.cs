using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Dalworth.SDK;
using Microsoft.WindowsCE.Forms;

namespace Dalworth.Controls
{
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        Modkeyup = 0x1000,
    }

    public enum KeysHardware : int
    {
        Hardware1 = 193,
        Hardware2 = 194,
        Hardware3 = 195,
        Hardware4 = 196,
        Hardware5 = 197,
        Hardware6 = 198,
        Hardware7 = 199
    }

    public class InternalMessageWindow : MessageWindow
    {
        public delegate void HardwareButtonKeyPressHandler(KeysHardware key);
        public event HardwareButtonKeyPressHandler HardwareButtonKeyPress;

        public const int WM_HOTKEY = 0x0312;

        Form referedForm;

        public InternalMessageWindow(Form referedForm)
        {
            this.referedForm = referedForm;
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                case WM_HOTKEY:

                    if (HardwareButtonKeyPress != null)
                    {
                        HardwareButtonKeyPress.Invoke((KeysHardware)msg.WParam.ToInt32());
                    }

                    return;
            }
            base.WndProc(ref msg);
        }
    }

    public class RegisterHKeys
    {
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
        IntPtr hWnd, // handle to window 
        int id, // hot key identifier 
        KeyModifiers Modifiers, // key-modifier options 
        int key //virtual-key code 
        );

        [DllImport("coredll.dll")]
        private static extern bool UnregisterFunc1(KeyModifiers
        modifiers, int keyID);        

        public static void RegisterRecordKey(IntPtr hWnd)
        {
            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware3);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware3, KeyModifiers.Windows, (int)KeysHardware.Hardware3);

            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware6);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware6, KeyModifiers.Windows, (int)KeysHardware.Hardware6);

            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware7);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware7, KeyModifiers.Windows, (int)KeysHardware.Hardware7);

            UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.Hardware5);
            RegisterHotKey(hWnd, (int)KeysHardware.Hardware5, KeyModifiers.Windows, (int)KeysHardware.Hardware5);
        }
    }

}
