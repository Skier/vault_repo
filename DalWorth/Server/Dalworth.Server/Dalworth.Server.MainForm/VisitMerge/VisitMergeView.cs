using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.VisitMerge
{
    public partial class VisitMergeView : BaseForm
    {
        public VisitMergeView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Visits Merge";
        }

    }
}