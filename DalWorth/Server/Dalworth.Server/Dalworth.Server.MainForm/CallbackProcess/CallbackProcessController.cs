using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.Components;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.MainForm.Dashboard;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;

namespace Dalworth.Server.MainForm.CallbackProcess
{
    public class CallbackProcessController : Controller<CallbackProcessModel, CallbackProcessView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length > 0 && data[0] != null)
                Model.ItemsToProcess = (List<CallbackReportWrapper>) data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnProcess.Click += OnProcessClick;
            View.m_btnCancel.Click += OnCancelClick;

            View.m_chkBusy.CheckedChanged += OnCheckChanged;
            View.m_chkCallReschedule.CheckedChanged += OnCheckChanged;
            View.m_chkCreateVisit.CheckedChanged += OnCheckChanged;
            View.m_chkDoNotCall.CheckedChanged += OnCheckChanged;
            View.m_chkLeftMessage.CheckedChanged += OnCheckChanged;
            View.m_chkNotInterested.CheckedChanged += OnCheckChanged;

            View.m_cmbNextCallPeriod.EditValueChanged += OnNextCallPeriodChanged;
            View.m_dtpNextCallDate.DateTimeChanged += OnNextCallDateChanged;

            View.m_cmbNextCallPeriod.Validating += OnNextCallPeriodValidating;
            View.m_dtpNextCallDate.Validating += OnNextCallDateValidating;

            View.m_groupNextCall.EnabledChanged += OnGroupNextCallEnabledChanged;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_groupNextCall.Enabled = false;

            if (Model.Customer != null)
            {
                View.m_ctlCustomer.Customer = Model.Customer;
                View.m_ctlCustomer.Enabled = false;                
            } else
                View.m_chkCreateVisit.Enabled = false;
                
        }

        #endregion

        #region GetDaysDuration

        private int? GetDaysDuration()
        {            
            string value = (string)View.m_cmbNextCallPeriod.EditValue;
            if (value == string.Empty)
                return null;

            int number;
            try
            {
                number = int.Parse(value.Split(new string[1] {" "}, StringSplitOptions.None)[0]);
            }
            catch (Exception)
            {
                return null;
            }

            if (value.Contains("day"))
                return number;

            if (value.Contains("month"))
                return (int)(number * 30.4);

            if (value.Contains("year"))
                return number * 365;

            return null;
        }

        #endregion

        #region OnCheckChanged

        private void OnCheckChanged(object sender, EventArgs e)
        {
            if (((CheckEdit)sender).Name == View.m_chkBusy.Name)
            {
                View.m_groupNextCall.Enabled = true;
                View.m_cmbNextCallPeriod.EditValue = "1 day";
            } 
            else if (((CheckEdit)sender).Name == View.m_chkLeftMessage.Name)
            {
                View.m_groupNextCall.Enabled = true;
                View.m_cmbNextCallPeriod.EditValue = "7 days";                
            }
            else if (((CheckEdit)sender).Name == View.m_chkCallReschedule.Name)
            {
                View.m_groupNextCall.Enabled = true;
                View.m_cmbNextCallPeriod.SelectedIndex = -1;                
            }
            else
            {
                View.m_groupNextCall.Enabled = false;
                View.m_cmbNextCallPeriod.SelectedIndex = -1;                                
            }
        }

        #endregion

        #region OnNextCallPeriodChanged

        private void OnNextCallPeriodChanged(object sender, EventArgs e)
        {
            if (GetDaysDuration() != null)
            {                
                View.m_dtpNextCallDate.EditValue = null;
                View.m_errorProvider.SetError(View.m_dtpNextCallDate, string.Empty);
            }
        }

        #endregion

        #region OnNextCallDateChanged

        private void OnNextCallDateChanged(object sender, EventArgs e)
        {
            if (View.m_dtpNextCallDate.EditValue != null)
            {
                View.m_cmbNextCallPeriod.SelectedIndex = -1;
                View.m_errorProvider.SetError(View.m_cmbNextCallPeriod, string.Empty);
            }                
        }

        #endregion

        #region Validation

        private void OnNextCallPeriodValidating(object sender, CancelEventArgs e)
        {
            if ((string)View.m_cmbNextCallPeriod.EditValue != string.Empty
                && GetDaysDuration() == null)
            {
                View.m_errorProvider.SetError(View.m_cmbNextCallPeriod, "Please enter valid Interval");
            } else
                View.m_errorProvider.SetError(View.m_cmbNextCallPeriod, string.Empty);
        }

        private void OnNextCallDateValidating(object sender, CancelEventArgs e)
        {
            if (View.m_dtpNextCallDate.EditValue != null 
                && View.m_dtpNextCallDate.DateTime.Date < DateTime.Now.Date)
            {
                View.m_errorProvider.SetError(View.m_dtpNextCallDate, "Next call date cannot be in the past");
            } else
                View.m_errorProvider.SetError(View.m_dtpNextCallDate, string.Empty);
        }

        #endregion

        #region OnGroupNextCallEnabledChanged

        private void OnGroupNextCallEnabledChanged(object sender, EventArgs e)
        {
            if (!View.m_groupNextCall.Enabled)
                View.m_errorProvider.ClearErrors();
        }

        #endregion



        #region OnProcessClick

        private void OnProcessClick(object sender, EventArgs e)
        {
            if (View.m_errorProvider.HasErrors)
                return;

            if (View.m_groupNextCall.Enabled
                && GetDaysDuration() == null
                && View.m_dtpNextCallDate.EditValue == null)
            {
                View.m_errorProvider.SetError(View.m_cmbNextCallPeriod, "Please specify next call period or date");
                return;
            }
            
            if (View.m_chkCreateVisit.Checked)
            {
                Visit visit = new Visit();
                visit.CustomerId = Model.Customer.ID;
                

                using (CreateVisitController controller = Prepare<CreateVisitController>(
                    null, null, null, visit))
                {
                    controller.Execute(false);
                    if (controller.IsCancelled)
                        return;
                }                                    
            }

            CallbackProcessTransactionStatusEnum action;

            if (View.m_chkBusy.Checked)
                action = CallbackProcessTransactionStatusEnum.Busy;
            else if (View.m_chkCallReschedule.Checked)
                action = CallbackProcessTransactionStatusEnum.CallReschedule;
            else if (View.m_chkCreateVisit.Checked)
                action = CallbackProcessTransactionStatusEnum.VisitCreated;
            else if (View.m_chkDoNotCall.Checked)
                action = CallbackProcessTransactionStatusEnum.DoNotCall;
            else if (View.m_chkLeftMessage.Checked)
                action = CallbackProcessTransactionStatusEnum.LeftMessage;
            else 
                action = CallbackProcessTransactionStatusEnum.NotIntrested;

            try
            {
                Database.Begin();
                Model.Process(GetDaysDuration(),
                    View.m_dtpNextCallDate.EditValue == null ? (DateTime?)null : View.m_dtpNextCallDate.DateTime.Date,
                    action);
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
            
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
