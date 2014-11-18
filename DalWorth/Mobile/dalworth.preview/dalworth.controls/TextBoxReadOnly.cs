using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace dalworth.controls
{
    public class TextBoxReadOnly : TextBox
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
