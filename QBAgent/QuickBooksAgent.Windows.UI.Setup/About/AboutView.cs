using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Setup.About
{
    public partial class AboutView : BaseControl
    {
        public AboutView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "About - Q-Agent";
        }
        
    }
}
