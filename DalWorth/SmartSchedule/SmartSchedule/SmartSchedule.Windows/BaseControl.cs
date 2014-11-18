using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SmartSchedule.Windows
{
    public class BaseControl : UserControl
    {
        public BaseControl()
        {
            
        }

        protected virtual void ApplyUIResources(CultureInfo cultureInfo)
        { }


        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            ApplyUIResources(Host.Instance.Culture);
        }
    }
}
