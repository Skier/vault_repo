using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Synchronize.Details
{
    public partial class SynchronizeDetailsView : QuickBooksAgent.Windows.UI.BaseControl
    {
        public SynchronizeDetailsView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Details - Q-Agent";
        }
    }
}

