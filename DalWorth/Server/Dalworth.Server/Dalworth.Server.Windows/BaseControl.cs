using System;
using System.Windows.Forms;
using System.Globalization;

namespace Dalworth.Server.Windows
{
    public class BaseControl : UserControl
    {
        protected virtual void ApplyUIResources(CultureInfo cultureInfo)
        { }


        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            ApplyUIResources(Host.Instance.Culture);
        }
    }
}
