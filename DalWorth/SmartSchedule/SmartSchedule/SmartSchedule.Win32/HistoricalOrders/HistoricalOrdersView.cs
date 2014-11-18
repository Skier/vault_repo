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

namespace SmartSchedule.Win32.HistoricalOrders
{
    public partial class HistoricalOrdersView : BaseForm
    {
        #region Model

        private HistoricalOrdersModel m_model;
        internal HistoricalOrdersModel Model
        {
            set { m_model = value; }
        }

        #endregion

        public HistoricalOrdersView()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {
            if (m_dtpDateSchedule.EditValue == null)
                m_model.RefreshOrders(null);
            else
                m_model.RefreshOrders(m_dtpDateSchedule.DateTime);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Historical Orders";
        }
    }
}