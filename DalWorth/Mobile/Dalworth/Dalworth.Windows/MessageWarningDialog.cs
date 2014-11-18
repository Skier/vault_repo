using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows
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
            CommonUIResources.Culture = cultureInfo;

            Text = CommonResources.InformationTitle;
        }

        #endregion  
    }
}