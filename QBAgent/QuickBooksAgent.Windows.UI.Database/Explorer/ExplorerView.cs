using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Explorer
{
    public partial class ExplorerView : QuickBooksAgent.Windows.UI.BaseControl
    {
        public ExplorerView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Domain Objects Viewer - Q-Agent";
        }
    }
}

