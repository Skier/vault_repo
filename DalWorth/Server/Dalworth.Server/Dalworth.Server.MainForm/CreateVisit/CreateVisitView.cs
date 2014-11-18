using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Windows;
using DevExpress.XtraBars;

namespace Dalworth.Server.MainForm.CreateVisit
{
    public partial class CreateVisitView : BaseForm
    {
        public CreateVisitView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Dalworth - Create Visit";
        }
    }
}