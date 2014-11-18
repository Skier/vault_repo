using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using Dalworth.Controls;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.DeclineVisit;
using Dalworth.Windows.ServiceVisit.VisitInfo;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.ReceiveVisit
{
    public class ReceiveVisitController : SingleFormController<ReceiveVisitModel, ReceiveVisitView>
    {
        #region Fields

        private bool m_isButtonRed;
        private Sound m_sound;

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Visit = (VisitPackage) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnAccept.Click += OnAcceptClick;
            View.m_timerBlink.Tick += OnTimerBlinkTick;
            View.m_timerSound.Tick += OnTimerSoundTick;

            m_sound = new Sound(Host.GetPath("Sounds") + "\\Alarm4.wav");
            StartVisualNotification();
        }

        #endregion

        #region OnTimerSoundTick

        private void OnTimerSoundTick(object sender, EventArgs e)
        {
            m_sound.Play();
        }

        #endregion

        #region OnTimerBlinkTick

        private void OnTimerBlinkTick(object sender, EventArgs e)
        {
            if (m_isButtonRed)
                View.m_btnAccept.BackColor = Color.White;
            else
                View.m_btnAccept.BackColor = Color.Red;

            m_isButtonRed = !m_isButtonRed;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = true;
            IsRightActionExist = true;
            LeftActionName = "Decline";
            RightActionName = "Accept";

            View.m_lblDate.Text = DateTime.Now.ToString("ddd, MMM dd yyyy");
            View.m_lblCustomerName.Text = Model.Visit.Customer.FirstName + ", " +Model.Visit.Customer.LastName;

            View.m_lblTaskType.Text = TaskType.GetText((TaskTypeEnum)Model.Visit.Tasks[0].Task.TaskTypeId);
            View.m_lblTaskNumber.Text = "TSK: " + Model.Visit.Tasks[0].Task.Number;
            View.m_txtNotes.Text = Model.Visit.Visit.Notes;            
        }

        #endregion

        #region OnViewActivated

        public override void OnViewActivated()
        {
            View.m_btnAccept.Focus();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return false;
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction() //Accept
        {            
            try
            {
                StopVisualNotification();
                using (WaitCursor cursor = new WaitCursor())
                {
                    Database.Begin();
                    Model.AcceptVisit();                    
                    Database.Commit();
                    WorkTransaction.Send();
                }
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("ReceiveVisitController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }

            VisitInfoController controller = Prepare<VisitInfoController>(Model.Visit);
            controller.Closed += OnCompleteClosed;
            controller.Execute();

        }

        private void OnCompleteClosed(SingleFormController controller)
        {
            VisitInfoController infoController = (VisitInfoController) controller;
            if (!infoController.IsCancelled)
                View.Destroy();
        }

        #endregion

        #region OnAcceptClick

        private void OnAcceptClick(object sender, EventArgs e)
        {
            OnRightAction();
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction() //Decline
        {
            StopVisualNotification();
            DeclineVisitController controller = Prepare<DeclineVisitController>(Model.Visit);
            controller.Closed += OnDeclineClosed;
            controller.Execute();
        }

        private void OnDeclineClosed(SingleFormController controller)
        {
            DeclineVisitController declineController = (DeclineVisitController)controller;
            if (!declineController.IsCancelled)
                View.Destroy();            
        }

        #endregion

        #region StartVisualNotification

        private void StartVisualNotification()
        {
            Backlight.Activate();            
            m_sound.Play();
            View.m_timerBlink.Enabled = true;
            View.m_timerSound.Enabled = true;
        }

        #endregion

        #region StopVisualNotification

        private void StopVisualNotification()
        {
            Backlight.Release();
            View.m_timerBlink.Enabled = false;
            View.m_timerSound.Enabled = false;
            View.m_btnAccept.BackColor = Color.White;
        }

        #endregion

    }
}
