using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportAdSourceByYear
{
    public partial class ReportAdSourceByYearView : BaseControl
    {
        public ReportAdSourceByYearView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Dalworth - Reports";
        }
    }
}
