using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public partial class SynchronizeBasicView : BaseControl
    {
        public SynchronizeBasicView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Synchronize - Q-Agent";
        }
    }
}