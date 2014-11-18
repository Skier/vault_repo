using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.CallbackSurvey
{
    public class CallbackSurveyController : Controller<CallbackSurveyModel, CallbackSurveyView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region SurveyInfo

        public SurveyInfo SurveyInfo
        {
            get { return Model.SurveyInfo; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length > 0 && data[0] != null)
                Model.SurveyInfo = (SurveyInfo) data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;            
            View.m_btnCancel.Click += OnCancelClick;

            View.m_cmbInterval.SelectedIndexChanged += OnIntervalChanged;
            View.m_dtpExactDate.DateTimeChanged += OnExactDateChanged;
            View.m_chkDoNotCall.CheckedChanged += OnDoNotCallChanged;
            View.m_dtpExactDate.Validating += OnExactDateValidating;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (Model.SurveyInfo.IsUserEntryExists)
            {
                if (Model.SurveyInfo.CallbackDaysInterval.HasValue)
                    View.m_cmbInterval.EditValue = Model.SurveyInfo.CallbackDaysInterval.Value;
                else if (Model.SurveyInfo.ExactDate.HasValue)
                    View.m_dtpExactDate.DateTime = Model.SurveyInfo.ExactDate.Value;
                else if (Model.SurveyInfo.IsDoNotCall)
                    View.m_chkDoNotCall.Checked = true;
            } else
            {
                if (Model.SurveyInfo.ExistingCallbackTransaction != null)
                {
                    if (Model.SurveyInfo.ExistingCallbackTransaction.CallbackDaysInterval.HasValue)
                        View.m_cmbInterval.EditValue = Model.SurveyInfo.ExistingCallbackTransaction.CallbackDaysInterval.Value;
                    else if (Model.SurveyInfo.ExistingCallbackTransaction.CallbackExactDate.HasValue)
                        View.m_dtpExactDate.DateTime = Model.SurveyInfo.ExistingCallbackTransaction.CallbackExactDate.Value;
                    else if (Model.SurveyInfo.ExistingCallbackTransaction.DoNotCall)
                        View.m_chkDoNotCall.Checked = true;
                }
            }
        }

        #endregion

        #region OnIntervalChanged

        private void OnIntervalChanged(object sender, EventArgs e)
        {
            if (View.m_cmbInterval.SelectedIndex > 0)
            {
                View.m_dtpExactDate.EditValue = null;
                OnExactDateValidating(null, null);

                View.m_chkDoNotCall.Checked = false;
            }                            
        }

        #endregion

        #region OnExactDateChanged

        private void OnExactDateChanged(object sender, EventArgs e)
        {
            if (View.m_dtpExactDate.EditValue != null)
            {
                View.m_cmbInterval.SelectedIndex = 0;
                View.m_chkDoNotCall.Checked = false;
            }                
        }

        #endregion

        #region OnDoNotCallChanged

        private void OnDoNotCallChanged(object sender, EventArgs e)
        {
            if (View.m_chkDoNotCall.Checked)
            {
                View.m_cmbInterval.SelectedIndex = 0;
                View.m_dtpExactDate.EditValue = null;
                OnExactDateValidating(null, null);
            }
        }

        #endregion


        #region OnExactDateValidating

        private void OnExactDateValidating(object sender, CancelEventArgs e)
        {
            if (View.m_dtpExactDate.EditValue != null 
                && View.m_dtpExactDate.DateTime < Model.SurveyInfo.SurveyDate.Date)
            {
                View.m_errorProvider.SetError(View.m_dtpExactDate, "Exact callback date cannot be in the past");
            } else
                View.m_errorProvider.SetError(View.m_dtpExactDate, string.Empty);
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {   
            if (View.m_errorProvider.HasErrors)
                return;

            Model.SurveyInfo.CallbackDaysInterval
                = View.m_cmbInterval.SelectedIndex != 0 ? (int?) View.m_cmbInterval.EditValue : null;
            Model.SurveyInfo.ExactDate
                = View.m_dtpExactDate.EditValue != null ? View.m_dtpExactDate.DateTime : (DateTime?) null;
            Model.SurveyInfo.IsDoNotCall
                = View.m_chkDoNotCall.Checked;

            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

    }
}
