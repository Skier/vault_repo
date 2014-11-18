using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartSchedule.Domain;
using BaseControl=SmartSchedule.Windows.BaseControl;

namespace SmartSchedule.Win32.Controls
{
    public partial class VisitInfo : BaseControl
    {
        public delegate void DurationAmountChangedHandler();
        public event DurationAmountChangedHandler DurationAmountChanged;

        private Visit m_visit;    

        public VisitInfo()
        {
            InitializeComponent();
            m_txtDurationAmount.EditValueChanged += OnDurationAmountChanged;
        }

        public string Caption
        {
            get { return m_groupContol.Text; }
            set { m_groupContol.Text = value; }
        }

        private bool m_isShowDurationAmount;
        public bool IsShowDurationAmount
        {
            get { return m_isShowDurationAmount; }
            set
            {
                m_isShowDurationAmount = value;
                m_lblDurationAmount.Visible = value;
                m_txtDurationAmount.Visible = value;
            }
        }

        public Visit Visit
        {
            get { return m_visit; }
            set
            {
                m_visit = value;
                ShowVisitInfo();
            }
        }

        private void OnDurationAmountChanged(object sender, EventArgs args)
        {
            m_visit.DurationCost = decimal.Parse(m_txtDurationAmount.EditValue.ToString(), 
                NumberStyles.Currency);

            if (DurationAmountChanged != null)
                DurationAmountChanged();
        }

        private void ShowVisitInfo()
        {
            m_pnlContent.Visible = m_visit != null;

            if (m_visit == null)
                return;

            m_lblCustomerName.Text = m_visit.CustomerName;
            m_lblHomePhone.Text = Utils.FormatPhone(m_visit.HomePhone);
            m_lblBusinessPhone.Text = Utils.FormatPhone(m_visit.BusinessPhone);
            m_lblZip.Text = m_visit.Zip;
            m_lblMap.Text = m_visit.Mapsco;
            m_lblTicketNumber.Text = m_visit.TicketNumber;
            m_lblTimeFrame.Text = m_visit.TimeFrameText;
            m_lblAmount.Text = m_visit.Cost.ToString("C");
            m_lblAddress.Text = m_visit.Address;
            m_txtDurationAmount.EditValue = m_visit.DurationCost;

            m_lblExclusiveToCaption.Visible = m_visit.ExclusiveTechnicianDefaultId.HasValue;
            m_lblExclusiveTo.Visible = m_visit.ExclusiveTechnicianDefaultId.HasValue;
            if (m_visit.ExclusiveTechnicianDefaultId.HasValue)
                m_lblExclusiveTo.Text = m_visit.ExclusiveTechnicianName;
        }
    }
}
