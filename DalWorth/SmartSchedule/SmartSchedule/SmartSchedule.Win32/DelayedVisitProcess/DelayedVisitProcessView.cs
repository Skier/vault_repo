using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.DelayedVisitProcess
{
    public partial class DelayedVisitProcessView : BaseForm
    {
        public DelayedVisitProcessView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Process Bucket Visit";
        }
    }
}