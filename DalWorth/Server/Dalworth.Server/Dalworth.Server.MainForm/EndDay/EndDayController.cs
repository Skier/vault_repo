using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.MakePayment;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.EndDay
{
    public class EndDayController : Controller<EndDayModel, EndDayView>
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
            base.OnModelInitialize(data);
        }

        #endregion        

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;

            View.m_timeEndDay.Validating += OnTimeEndDayValidating;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_timeEndDay.Time = Model.GetDefaultEndDayTime();
        }

        #endregion              

        #region OnTimeEndDayValidating

        private void OnTimeEndDayValidating(object sender, CancelEventArgs e)
        {
            if (Model.Work.StartDate.Value.Date == DateTime.Now.Date)
            {
                if (View.m_timeEndDay.Time.TimeOfDay > DateTime.Now.TimeOfDay)
                    View.m_errorProvider.SetError(View.m_timeEndDay, "End Day time cannot be in the future");
                else
                    View.m_errorProvider.SetError(View.m_timeEndDay, string.Empty);
            }
        }

        #endregion      

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            DateTime endDayDate = Model.Work.StartDate.Value.Date;
            endDayDate = new DateTime(
                endDayDate.Year,
                endDayDate.Month,
                endDayDate.Day,
                View.m_timeEndDay.Time.Hour,
                View.m_timeEndDay.Time.Minute,
                0);

            if (endDayDate < Model.Work.StartDayDate.Value)
            {
                XtraMessageBox.Show("End Day time should be greater than Start Day time",
                                    "Incorrect End Day time", MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                View.m_timeEndDay.Focus();
                return;
            }


            if (Model.IsVisitsExistsAfter(endDayDate))
            {
                XtraMessageBox.Show("There are visits scheduled after " + endDayDate.ToShortTimeString()
                                    + ". Please enter correct End Day time",
                                    "Incorrect End Day time", MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                View.m_timeEndDay.Focus();
                return;
            }            

            try
            {
                Database.Begin();
                Model.CompleteWork(endDayDate);
                Database.Commit();
                Host.TraceUserAction("Complete Work " + Model.Technician.DisplayName);
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
