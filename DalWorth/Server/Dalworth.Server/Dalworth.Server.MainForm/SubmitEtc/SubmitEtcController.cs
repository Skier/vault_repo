using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.SubmitEtc
{
    public class SubmitEtcController : Controller<SubmitEtcModel, SubmitEtcView>
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
            Model.Work = (Work)data[0];
            Model.Visit = (Visit)data[1];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
            View.m_dtpEstimatedCompletionTime.Validating += OnEstimatedCompletionTimeValidating;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (DateTime.Now.Hour + 1 >= 24)
                View.m_dtpEstimatedCompletionTime.Time = DateTime.Now;
            else
                View.m_dtpEstimatedCompletionTime.Time = new DateTime(
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    DateTime.Now.Hour + 1, 0, 0);

            View.m_dtpEstimatedCompletionTime.Select();
        }

        #endregion

        #region OnEstimatedCompletionTimeValidating

        private void OnEstimatedCompletionTimeValidating(object sender, CancelEventArgs e)
        {
            if (View.m_dtpEstimatedCompletionTime.Time < DateTime.Now)
                View.m_errorProvider.SetError(View.m_dtpEstimatedCompletionTime, "Estimated completion time cannot be in the past");
            else
                View.m_errorProvider.SetError(View.m_dtpEstimatedCompletionTime, string.Empty);
        }

        #endregion


        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;            

            WorkDetail workDetail = WorkDetail.FindByWorkAndVisit(Model.Work, Model.Visit, null);

            CollisionErrorEnum collision
                = WorkDetail.IsExistCollision(workDetail, workDetail.TimeBegin, 
                    View.m_dtpEstimatedCompletionTime.Time);

            if (collision == CollisionErrorEnum.ProcessedVisit)
            {
                XtraMessageBox.Show(
                    "Cannot submit ETC. There is time collision with another processed visit",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (collision == CollisionErrorEnum.StartDay)
            {
                XtraMessageBox.Show(
                    "Cannot submit ETC. There is time collision with Start Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (collision == CollisionErrorEnum.EndDay)
            {
                XtraMessageBox.Show(
                    "Cannot submit ETC. There is time collision with End Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            

            try
            {
                Database.Begin();

                Visit.Etc(Model.Work.TechnicianEmployeeId, Model.Visit.ID,
                    (decimal)(View.m_txtEstimatedClosedAmount.EditValue ?? decimal.Zero),
                    View.m_dtpEstimatedCompletionTime.Time, View.m_txtNotes.Text, null);

                Database.Commit();
                Host.TraceUserAction("Submit ETC for Visit " + Model.Visit.ID);
                View.Destroy();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

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
