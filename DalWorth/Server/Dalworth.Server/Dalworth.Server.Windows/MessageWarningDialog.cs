using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Server.Windows
{
    public partial class MessageWarningDialog : BaseForm
    {
        public MessageWarningDialog(String message)
        {
            InitializeComponent();
        }

        #region ApplyUIResources

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            Text = "Information - Dalworth";
        }

        #endregion  
    }
}