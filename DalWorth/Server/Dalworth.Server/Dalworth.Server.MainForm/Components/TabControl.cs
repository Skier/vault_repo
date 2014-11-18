using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Dalworth.Server.MainForm.Components
{
    public class TabControl : XtraTabControl
    {
        public TabControl()
        {
            SetStyle(ControlStyles.ContainerControl, true);
        }
    }
}
