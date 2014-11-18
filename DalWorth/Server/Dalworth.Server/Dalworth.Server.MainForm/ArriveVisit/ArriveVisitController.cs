using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.ArriveVisit
{
    public class ArriveVisitController : Controller<ArriveVisitModel, ArriveVisitView>
    {
        private DateTime m_selectedTime;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region SelectedTime

        public DateTime SelectedTime
        {
            get { return m_selectedTime; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.WorkDetail = (WorkDetail)data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_timeArrive.ButtonClick += OnTimeArriveButtonClick;
            View.m_timeArrive.Validating += OnTimeArriveValidating;
            View.m_timeArrive.KeyDown += OnTimeArriveKeyDown;

            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (Model.WorkDetail.TimeArrive.HasValue)
                View.m_timeArrive.Time = Model.WorkDetail.TimeArrive.Value;
            else if (DateTime.Now - Model.WorkDetail.TimeEnd > new TimeSpan(2, 0, 0))
                View.m_timeArrive.Time = Model.WorkDetail.TimeBegin.AddMinutes(30);
            else
                View.m_timeArrive.Time = DateTime.Now;

            View.m_timeArrive.Focus();            
        }

        #endregion

        #region OnTimeArriveKeyDown

        private void OnTimeArriveKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.C)
            {
                ButtonPressedEventArgs arg = new ButtonPressedEventArgs(
                    View.m_timeArrive.Properties.Buttons[1]);
                OnTimeArriveButtonClick(null, arg);
            }                
        }

        #endregion


        #region OnTimeArriveValidating

        private void OnTimeArriveValidating(object sender, CancelEventArgs e)
        {
            if (View.m_timeArrive.Time > DateTime.Now)
                View.m_errorProvider.SetError(View.m_timeArrive, "Arrival time cannot be in the future");
            else if (View.m_timeArrive.Time < Model.WorkDetail.TimeBegin)
                View.m_errorProvider.SetError(View.m_timeArrive, "Arrival time cannot be less than visit start time");
            else if (Model.WorkDetail.TimeComplete.HasValue && View.m_timeArrive.Time > Model.WorkDetail.TimeComplete.Value)
                View.m_errorProvider.SetError(View.m_timeArrive, "Arrival time cannot be greater than visit completion time");
            else
                View.m_errorProvider.SetError(View.m_timeArrive, string.Empty);
        }

        #endregion

        #region OnTimeArriveButtonClick

        private void OnTimeArriveButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                View.m_timeArrive.Time = DateTime.Now;
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            m_selectedTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                View.m_timeArrive.Time.Hour,
                View.m_timeArrive.Time.Minute, 0);

            CollisionErrorEnum collision
                = WorkDetail.IsExistCollision(Model.WorkDetail, Model.WorkDetail.TimeBegin, m_selectedTime);

            if (collision == CollisionErrorEnum.ProcessedVisit)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Arrive. There is time collision with another processed visit",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (collision == CollisionErrorEnum.StartDay)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Arrive. There is time collision with Start Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (collision == CollisionErrorEnum.EndDay)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Arrive. There is time collision with End Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Host.TraceUserAction("Arrive Visit " + Model.WorkDetail.VisitId);
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
