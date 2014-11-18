using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BaseControl=Dalworth.Server.Windows.BaseControl;

namespace Dalworth.Server.MainForm.Components
{
    public partial class DateRange : BaseControl
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

        public Domain.DateRange EditValue
        {
            get
            {
                return new Domain.DateRange(
                    (DateTime?)m_dtpStart.EditValue,
                    (DateTime?)m_dtpEnd.EditValue);
            }
            set
            {
                if (value == null)
                    value = new Domain.DateRange();

                m_dtpStart.EditValue = value.StartDate;
                m_dtpEnd.EditValue = value.EndDate;
            }
        }
    }
}
