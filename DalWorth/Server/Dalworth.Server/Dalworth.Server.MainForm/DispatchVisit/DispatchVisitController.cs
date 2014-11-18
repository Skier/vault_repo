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

namespace Dalworth.Server.MainForm.DispatchVisit
{
    public class DispatchVisitController : Controller<DispatchVisitModel, DispatchVisitView>
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
            Model.VisitStartTime = (DateTime)data[1];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_timeDispatch.ButtonClick += OnTimeDispatchButtonClick;
            View.m_timeDispatch.Validating += OnTimeDispatchValidating;
            View.m_timeDispatch.KeyDown += OnTimeDispatchKeyDown;

            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (Model.WorkDetail.TimeDispatch.HasValue)
                View.m_timeDispatch.Time = Model.WorkDetail.TimeDispatch.Value;
            else if (DateTime.Now - Model.VisitStartTime > new TimeSpan(2, 0, 0))
                View.m_timeDispatch.Time = Model.VisitStartTime;
            else
                View.m_timeDispatch.Time = DateTime.Now;

            View.m_timeDispatch.Focus();            
        }

        #endregion

        #region OnTimeDispatchKeyDown

        private void OnTimeDispatchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.S)
            {
                ButtonPressedEventArgs arg = new ButtonPressedEventArgs(
                    View.m_timeDispatch.Properties.Buttons[1]);
                OnTimeDispatchButtonClick(null, arg);

            } else if (e.Alt && e.KeyCode == Keys.C)
            {
                ButtonPressedEventArgs arg = new ButtonPressedEventArgs(
                    View.m_timeDispatch.Properties.Buttons[2]);
                OnTimeDispatchButtonClick(null, arg);                
            }
        }

        #endregion

        #region OnTimeDispatchValidating

        private void OnTimeDispatchValidating(object sender, CancelEventArgs e)
        {
            if (View.m_timeDispatch.Time > DateTime.Now)
                View.m_errorProvider.SetError(View.m_timeDispatch, "Dispatch time cannot be in the future");
            else if (Model.WorkDetail.TimeArrive.HasValue && View.m_timeDispatch.Time > Model.WorkDetail.TimeArrive.Value)
                View.m_errorProvider.SetError(View.m_timeDispatch, "Dispatch time cannot be greater than arrival time");
            else if (Model.WorkDetail.TimeComplete.HasValue && View.m_timeDispatch.Time > Model.WorkDetail.TimeComplete.Value)
                View.m_errorProvider.SetError(View.m_timeDispatch, "Dispatch time cannot be greater than completion time");
            else
                View.m_errorProvider.SetError(View.m_timeDispatch, string.Empty);
        }

        #endregion

        #region OnTimeDispatchButtonClick

        private void OnTimeDispatchButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                View.m_timeDispatch.Time = Model.VisitStartTime;
            else if (e.Button.Index == 2)
                View.m_timeDispatch.Time = DateTime.Now;
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
                View.m_timeDispatch.Time.Hour,
                View.m_timeDispatch.Time.Minute, 0);

            CollisionErrorEnum collision;

            if (Model.WorkDetail.TimeComplete.HasValue)
            {
                collision = WorkDetail.IsExistCollision(Model.WorkDetail,
                    m_selectedTime, Model.WorkDetail.TimeComplete.Value);
            }
            else if (Model.WorkDetail.TimeArrive.HasValue)
            {
                collision = WorkDetail.IsExistCollision(Model.WorkDetail,
                    m_selectedTime, Model.WorkDetail.TimeArrive.Value);                
            } else
                collision = WorkDetail.IsExistCollision(Model.WorkDetail, m_selectedTime);


            if (collision == CollisionErrorEnum.ProcessedVisit)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Dispatch. There is time collision with another processed visit",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (collision == CollisionErrorEnum.StartDay)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Dispatch. There is time collision with Start Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (collision == CollisionErrorEnum.EndDay)
            {
                XtraMessageBox.Show(
                    "Cannot perform Visit Dispatch. There is time collision with End Day",
                    "Time collision",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Host.TraceUserAction("Dispatch Visit " + Model.WorkDetail.VisitId);
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
