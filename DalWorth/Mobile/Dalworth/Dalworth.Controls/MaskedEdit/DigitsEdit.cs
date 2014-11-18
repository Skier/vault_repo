using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Controls
{
    public class DigitsEdit : TextBox
    {
        #region TextBox

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (Char.IsLetter(e.KeyChar))
            {                                
                if (e.KeyChar == 'w' || e.KeyChar == 'W')                    
                    WinAPI.SendKey('1');
                else if (e.KeyChar == 'e' || e.KeyChar == 'E')
                    WinAPI.SendKey('2');
                else if (e.KeyChar == 'r' || e.KeyChar == 'R')
                    WinAPI.SendKey('3');
                else if (e.KeyChar == 's' || e.KeyChar == 'S')
                    WinAPI.SendKey('4');
                else if (e.KeyChar == 'd' || e.KeyChar == 'D')
                    WinAPI.SendKey('5');
                else if (e.KeyChar == 'f' || e.KeyChar == 'F')
                    WinAPI.SendKey('6');
                else if (e.KeyChar == 'z' || e.KeyChar == 'Z')
                    WinAPI.SendKey('7');
                else if (e.KeyChar == 'x' || e.KeyChar == 'X')
                    WinAPI.SendKey('8');
                else if (e.KeyChar == 'c' || e.KeyChar == 'C')
                    WinAPI.SendKey('9');

                e.Handled = true;
            }
        }

        #endregion
    }
}
