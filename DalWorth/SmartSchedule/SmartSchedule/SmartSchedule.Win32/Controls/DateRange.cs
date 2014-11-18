using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartSchedule.Domain;

namespace SmartSchedule.Win32.Controls
{
    public partial class DateRange : Windows.BaseControl
    {
        public event EventHandler DateRangeValueChanged;

        public DateRange()
        {
            InitializeComponent();            
            m_dtpStart.EditValueChanged += OnDateChanged;
            m_dtpEnd.EditValueChanged += OnDateChanged;
        }

        private void OnDateChanged(object sender, EventArgs e)
        {
            if (m_dtpStart != null && m_dtpEnd.EditValue != null 
                && m_dtpEnd.DateTime.Date < m_dtpStart.DateTime.Date)
            {
                if (((DateEdit)sender).Name == "m_dtpStart")
                    m_dtpEnd.EditValue = m_dtpStart.DateTime.Date;
                else
                    m_dtpStart.EditValue = m_dtpEnd.DateTime.Date;
            }

            if (DateRangeValueChanged != null)
                DateRangeValueChanged.Invoke(this, e);
        }

        public void Clear()
        {
            EditValue = null;
        }

        public TimeInterval EditValue
        {
            get
            {
                return new TimeInterval(
                    m_dtpStart.EditValue == null ? DateTime.MinValue : (DateTime)m_dtpStart.EditValue,
                    m_dtpEnd.EditValue == null ? DateTime.MaxValue : (DateTime)m_dtpEnd.EditValue);
            }
            set
            {
                if (value == null)
                    value = new TimeInterval(DateTime.MinValue, DateTime.MaxValue);

                if (value.Start == DateTime.MinValue)
                    m_dtpStart.EditValue = null;
                else
                    m_dtpStart.EditValue = value.Start;

                if (value.End == DateTime.MaxValue || value.End.Year == 9999)
                    m_dtpEnd.EditValue = null;
                else
                    m_dtpEnd.EditValue = value.End;
            }
        }
    }
}
