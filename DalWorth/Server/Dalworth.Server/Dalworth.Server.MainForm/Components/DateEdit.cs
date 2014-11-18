using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;

namespace Dalworth.Server.MainForm.Components
{
    public class DateEdit : DevExpress.XtraEditors.DateEdit
    {        
        #region OnKeyDown

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.T)
                {
                    ClosePopup();                    
                    DateTime = DateTime.Now;                    
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.C
                         && (Properties.AllowNullInput == DefaultBoolean.Default
                             || Properties.AllowNullInput == DefaultBoolean.True))
                {
                    ClosePopup();
                    EditValue = null;                    
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }

            }

            base.OnKeyDown(e);
        }

        #endregion
    }
}
