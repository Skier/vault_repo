using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.UI;

namespace Dalworth.Server.MainForm.ReportRugReadyAging
{
    public partial class ReportRugReadyAgingView : BaseControl
    {
        public ReportRugReadyAgingView()
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
