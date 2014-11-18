using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI
{
    public interface IJoystickElement
    {
        Point JoystickPosition {get;set;}

        bool JoystickEnabled {get;set;}
    }
}
