using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Controls
{

    public class AutoDropDown
    {

        public void Add(ComboBox comboBox)
        {
            comboBox.KeyDown += new KeyEventHandler(OnKeyDown);
            comboBox.KeyPress += new KeyPressEventHandler(OnKeyPress);
            comboBox.LostFocus += new EventHandler(OnLostFocus);
        }

        public void Add(DateTimePicker picker)
        {
            picker.KeyDown += new KeyEventHandler(OnKeyDown);
            picker.KeyPress += new KeyPressEventHandler(OnKeyPress);
        }

        void OnLostFocus(object sender, EventArgs e)
        {
            WinAPI.DropDown((sender as ComboBox), false);
        }

        void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is ComboBox)
            {
                if (e.KeyData == Keys.Enter &&
                    !WinAPI.IsDropedDown(sender as ComboBox))
                {
                    e.Handled = true;

                    WinAPI.DropDown(sender as ComboBox, true);
                }
            }
            else if (sender is DateTimePicker)
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.Handled = true;

                    DateTimePicker picker = sender as DateTimePicker;
                    IntPtr key = (IntPtr)Keys.F4;

                    WinAPI.SendMessage(picker.Handle, WinAPI.WM_KEYDOWN, key, IntPtr.Zero);
                }
            }
        }
    }


}
