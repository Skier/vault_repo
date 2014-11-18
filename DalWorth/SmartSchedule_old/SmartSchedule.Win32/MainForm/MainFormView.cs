using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.MainForm
{
    public partial class MainFormView : BaseForm
    {
        public MainFormView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Smart Schedule";
        }
    }
}