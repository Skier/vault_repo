using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.MainForm.Components
{
    public class DalworthTabControl : DevExpress.XtraTab.XtraTabControl
    {
        #region Constructor

        public DalworthTabControl()
        {
            SetStyle(System.Windows.Forms.ControlStyles.ContainerControl, true);
        }

        #endregion
    }
}
