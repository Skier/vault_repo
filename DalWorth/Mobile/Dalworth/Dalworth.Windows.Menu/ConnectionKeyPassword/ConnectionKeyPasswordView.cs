using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.Menu.ConnectionKeyPassword
{
    public partial class ConnectionKeyPasswordView : BaseControl
    {
        public ConnectionKeyPasswordView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Connection Key Password";
        }

    }
}
