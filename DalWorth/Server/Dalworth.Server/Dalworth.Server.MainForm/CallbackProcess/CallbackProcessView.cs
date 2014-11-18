using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CallbackProcess
{
    public partial class CallbackProcessView : BaseForm
    {
        public CallbackProcessView()
        {
            InitializeComponent();

            m_cmbNextCallPeriod.Properties.Items.Clear();
            m_cmbNextCallPeriod.Properties.Items.Add("1 day");
            m_cmbNextCallPeriod.Properties.Items.Add("2 days");
            m_cmbNextCallPeriod.Properties.Items.Add("3 days");
            m_cmbNextCallPeriod.Properties.Items.Add("7 days");
            m_cmbNextCallPeriod.Properties.Items.Add("15 days");
            m_cmbNextCallPeriod.Properties.Items.Add("1 month");
            m_cmbNextCallPeriod.Properties.Items.Add("2 months");
            m_cmbNextCallPeriod.Properties.Items.Add("3 months");
            m_cmbNextCallPeriod.Properties.Items.Add("6 months");
            m_cmbNextCallPeriod.Properties.Items.Add("1 year");
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Callback Process";
        }
    }
}