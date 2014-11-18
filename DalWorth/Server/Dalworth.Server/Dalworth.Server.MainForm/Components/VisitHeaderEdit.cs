using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.MapscoLookup;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class VisitHeaderEdit : BaseControl
    {
        private enum TimeFrameTemplateEnum
        {
            Custom = 1,
            InAM = 2,
            InPM = 3
        }

        #region HasErrors
        
        public bool HasErrors
        {
            get { return m_errorProvider.HasErrors; }
        }

        #endregion

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get
            {
                if (m_visit == null)
                    InitDefaultVisit();
                LoadDataFromUI();
                return m_visit;
            }
            set
            {
                m_visit = value;
                if (m_visit == null)
                    InitDefaultVisit();
                LoadDataToUI();
            }
        }

        #endregion

        #region InitDefaultVisit

        private void InitDefaultVisit()
        {
            m_visit = new Visit();
            m_visit.ServiceDate = DateTime.Now;
            m_visit.VisitStatus = VisitStatusEnum.Pending;            
        }

        #endregion

        #region IsReadOnly

        public bool IsReadOnly
        {
            get { return m_dtpServiceDate.Properties.ReadOnly; }
            set
            {
                m_dtpServiceDate.Properties.ReadOnly = value;
                m_cmbTimeFrameTemplate.Properties.ReadOnly = value;
                m_dtpTimeFrameBegin.Properties.ReadOnly = value;
                m_dtpTimeFrameEnd.Properties.ReadOnly = value;
                m_txtNotes.Properties.ReadOnly = value;
            }
        }

        #endregion

        #region Constructor

        public VisitHeaderEdit()
        {
            InitializeComponent();

            m_cmbTimeFrameTemplate.SelectedIndexChanged += OnTimeFrameTemplateChanged;
            m_dtpTimeFrameBegin.Validating += OnTimeFrameValidating;
            m_dtpTimeFrameEnd.Validating += OnTimeFrameValidating;
            m_dtpTimeFrameBegin.EditValueChanged += OnTimeFrameChanged;
            m_dtpTimeFrameEnd.EditValueChanged += OnTimeFrameChanged;
            this.GotFocus += OnGotFocus;
        }

        #endregion

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Visit = null;
        }

        #endregion

        #region OnGotFocus

        private void OnGotFocus(object sender, EventArgs e)
        {
            m_dtpServiceDate.Focus();
        }

        #endregion


        #region OnTimeFrameTemplateChanged

        private bool m_isTimeFrameUpdatingEachOther = false;

        private void OnTimeFrameTemplateChanged(object sender, EventArgs e)
        {
            if (m_isTimeFrameUpdatingEachOther)
                return;

            m_isTimeFrameUpdatingEachOther = true;

            if ((int)m_cmbTimeFrameTemplate.EditValue == (int)TimeFrameTemplateEnum.InAM)
            {
                m_dtpTimeFrameBegin.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    0, 0, 0);
                m_dtpTimeFrameEnd.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    12, 0, 0);
            }
            else if ((int)m_cmbTimeFrameTemplate.EditValue == (int)TimeFrameTemplateEnum.InPM)
            {
                m_dtpTimeFrameBegin.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    12, 0, 0);
                m_dtpTimeFrameEnd.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    23, 0, 0);
            }

            m_isTimeFrameUpdatingEachOther = false;
        }

        #endregion

        #region OnTimeFrameChanged

        private void OnTimeFrameChanged(object sender, EventArgs e)
        {
            if (m_isTimeFrameUpdatingEachOther)
                return;

            m_isTimeFrameUpdatingEachOther = true;

            if (m_dtpTimeFrameBegin.Time.Hour == 0
                && m_dtpTimeFrameEnd.Time.Hour == 12)
            {
                m_cmbTimeFrameTemplate.EditValue = (int)TimeFrameTemplateEnum.InAM;

            }
            else if (m_dtpTimeFrameBegin.Time.Hour == 12
                && m_dtpTimeFrameEnd.Time.Hour == 23)
            {
                m_cmbTimeFrameTemplate.EditValue = (int)TimeFrameTemplateEnum.InPM;
            }
            else
                m_cmbTimeFrameTemplate.EditValue = (int)TimeFrameTemplateEnum.Custom;

            m_isTimeFrameUpdatingEachOther = false;
        }

        #endregion

        #region OnTimeFrameValidating

        private void OnTimeFrameValidating(object sender, CancelEventArgs e)
        {
            if (m_dtpTimeFrameBegin.EditValue != null
                && m_dtpTimeFrameEnd.EditValue != null)
            {
                if (m_dtpTimeFrameBegin.Time > m_dtpTimeFrameEnd.Time)
                {
                    m_errorProvider.SetError((Control)sender, "Start time should be less than End time");
                    return;
                }
            }

            m_errorProvider.SetError(m_dtpTimeFrameBegin, string.Empty);
            m_errorProvider.SetError(m_dtpTimeFrameEnd, string.Empty);
        }

        #endregion

        #region LoadDataFromUI

        private void LoadDataFromUI()
        {
            m_visit.ServiceDate = m_dtpServiceDate.EditValue == null ? (DateTime?)null : m_dtpServiceDate.DateTime;
            m_visit.PreferedTimeFrom = m_dtpTimeFrameBegin.EditValue == null ? (DateTime?)null : m_dtpTimeFrameBegin.Time;
            m_visit.PreferedTimeTo = m_dtpTimeFrameEnd.EditValue == null ? (DateTime?)null : m_dtpTimeFrameEnd.Time;
            m_visit.Notes = m_txtNotes.Text;
        }

        #endregion

        #region LoadDataToUI

        private void LoadDataToUI()
        {
            m_lblVisitNumber.Text = m_visit.ID <= 0 ? "[Unknown]" : m_visit.ID.ToString();
            m_dtpServiceDate.EditValue = m_visit.ServiceDate;
            m_dtpTimeFrameBegin.EditValue = m_visit.PreferedTimeFrom;
            m_dtpTimeFrameEnd.EditValue = m_visit.PreferedTimeTo;
            OnTimeFrameChanged(null, null);
            m_txtNotes.Text = m_visit.Notes;
        }

        #endregion
    }
}