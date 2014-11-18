using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.VisitAdd
{
    public partial class VisitAddView : BaseForm
    {
        public VisitAddView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Add Visit";
        }
    }
}