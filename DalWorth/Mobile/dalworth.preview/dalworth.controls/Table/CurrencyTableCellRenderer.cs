using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MobileTech.Windows.UI.Controls
{
    public class CurrencyTableCellRenderer : DefaultTableCellRenderer
    {
        #region Constructor

        public CurrencyTableCellRenderer()
        {            
            if (this.StringFormat == null)
                this.StringFormat = new StringFormat();

            this.StringFormat.Alignment = StringAlignment.Far;
            this.StringFormat.LineAlignment = StringAlignment.Center;            
        }

        #endregion
    }
}
