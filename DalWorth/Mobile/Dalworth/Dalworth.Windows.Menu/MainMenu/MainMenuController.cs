using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Dalworth.Controls;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.GPS;
using Dalworth.SDK;
using Dalworth.Windows.EndDay.EndDayDone;
using Dalworth.Windows.PendingTransactions;
using Dalworth.Windows.ServiceVisit.ReceiveVisit;
using Dalworth.Windows.StartDay.Login;
using Microsoft.WindowsMobile.Telephony;
using Address=Dalworth.Domain.Address;
using Application=Dalworth.Domain.Application;
using Visit=Dalworth.Domain.Visit;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.Menu.MainMenu
{
    public class MainMenuController:SingleFormController<MainMenuModel,MainMenuView>
    {
        private DateTime m_lastSuccessWebAccessDate;
        private bool m_isGpsDataNeeded;

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnStartDay.Click += OnStartDayClick;
            View.m_btnEndDay.Click += OnEndDayClick;
            View.m_timerIncome.Tick += OnTimerIncomeTick;
            View.m_timerGps.Tick += OnTimerGpsTick;

            View.m_timerIncome.Interval = Configuration.checkIngoingVisitPeriod;
            View.m_timerGps.Interval = Configuration.GpsTrackPeriod;
            WorkTransaction.SendAttemptThresholdExceeded += OnSendAttemptThresholdExceeded; 
                        
            MainFormController.Instance.GpsPositionChanded += OnGpsPositionChanded;
        }

        #endregion


        #region OnViewActivated

        public override void OnViewActivated()
        {
            m_lastSuccessWebAccessDate = DateTime.Now;
            using(new WaitCursor())
            {
                ApplicationStateEnum applicationState = Model.GetApplicationPackage().Application.ApplicationState;
                View.m_btnStartDay.Enabled = (applicationState == ApplicationStateEnum.StartDay);
                View.m_btnEndDay.Enabled = (applicationState != ApplicationStateEnum.StartDay);
                View.m_timerIncome.Enabled = (applicationState == ApplicationStateEnum.StartDayDone && Configuration.ConnectionKey != string.Empty);
                View.m_timerGps.Enabled = (applicationState == ApplicationStateEnum.StartDayDone);

                if (View.m_btnStartDay.Enabled)
                    View.m_btnStartDay.Focus();
                else if (View.m_btnEndDay.Enabled)
                    View.m_btnEndDay.Focus();
            }            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {            
            WorkTransaction.Send(20000); //Attempt to send transactions will start in 20 sec
        }

        #endregion

        #region OnStartDayClick

        private void OnStartDayClick(object sender, EventArgs e)
        {
            try
            {
                using(new WaitCursor())
                {
                    Database.Begin();
                    Model.SynchronizeEmployees();
                    Database.Commit();
                }                
            }
            catch (WebException ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Couldn't establish connection to the server. Please check your connection availability and try again.");
                Host.Trace("MainMenuController::OnStartDayClick", ex.Message + ex.StackTrace);
                return;                
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("MainMenuController::OnStartDayClick", ex.Message + ex.StackTrace);
                return;
            }

            LoginController loginController = Prepare<LoginController>();
            loginController.Execute();
        }

        #endregion

        #region OnEndDayClick

        private void OnEndDayClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to complete Work Day?", "Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
                    Database.Begin();
                    Model.CompleteDay();
                    Database.Commit();                    
                }

                EndDayDoneController controller = Prepare<EndDayDoneController>();
                controller.Execute();
            }
            catch (WebException ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Couldn't establish connection to the server. Please check your connection availability and try again.");
                Host.Trace("MainMenuController::OnEndDayClick", ex.Message + ex.StackTrace);
                return;
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("MainMenuController::OnEndDayClick", ex.Message + ex.StackTrace);
                return;
            }
        }

        #endregion

        #region OnTimerIncomeTick

        private bool m_isIncomeFailMessageActive = false;

        private void OnTimerIncomeTick(object sender, EventArgs e)
        {
            if (m_isIncomeFailMessageActive)
                return;

            IDbTransaction transaction = null;

            try
            {
                Message message;
                message = Model.GetIncomingMessage();

                m_lastSuccessWebAccessDate = DateTime.Now;

                if (message != null)
                {
                    if (message.MessageTypeId == (int)MessageTypeEnum.VisitIncome && message.VisitId != null)
                    {                        
                        VisitPackage package = Model.GetVisit(message.VisitId.Value);
                        View.m_timerIncome.Enabled = false;


                        IDbConnection connection = Connection.Instance.GetDbConnection(ConnectionKeyEnum.Temporary1);
                        transaction = connection.BeginTransaction();
                        Visit.SaveReceivedVisit(package, transaction);
                        Model.NotifyMessageReceived(message.ID);                        
                        transaction.Commit();


                        ReceiveVisitController receiveVisitController = Prepare<ReceiveVisitController>(package);
                        receiveVisitController.Execute();
                    }
                }
                    
            }
            catch (WebException ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                if (DateTime.Now.Subtract(m_lastSuccessWebAccessDate).TotalMilliseconds > Configuration.checkIngoingVisitFailThreshold)
                {
                    m_isIncomeFailMessageActive = true;
                    Host.Trace("MainMenuController::OnTimerIncomeTick", ex.Message + ex.StackTrace);                    

                    if (MessageDialog.Show(MessageDialogType.Warning,
                            "Couldn't establish connection to the server. Please check your connection availability and try again.") == DialogResult.OK)
                    {
                        m_isIncomeFailMessageActive = false;
                        m_lastSuccessWebAccessDate = DateTime.Now;
                    }
                                        
                }
                
                return;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("MainMenuController::OnTimerIncomeTick", ex.Message + ex.StackTrace);
                return;
            }
        }

        #endregion

        #region OnTimerGpsTick

        private void OnTimerGpsTick(object sender, EventArgs e)
        {
            View.m_timerGps.Enabled = false;
            m_isGpsDataNeeded = true;
        }

        #endregion

        #region OnGpsPositionChanded

        private void OnGpsPositionChanded(GpsPosition position)
        {
            if (m_isGpsDataNeeded)
            {
                try
                {
                    Database.Begin();
                    WorkTransaction.SaveGpsInfo((float)position.Latitude, (float)position.Longitude, position.Time);
                    Database.Commit();
                    m_isGpsDataNeeded = false;
                    View.m_timerGps.Enabled = true;
                    WorkTransaction.Send();
                }
                catch (Exception ex)
                {
                    Database.Rollback();
                    MessageDialog.Show(MessageDialogType.Warning,
                            "Unknown application error. Please contact dispatch");
                    Host.Trace("MainMenuController::GpsDataUpdate", ex.Message + ex.StackTrace);
                }
            }
        }

        #endregion

        #region OnSendAttemptThresholdExceeded

        private void OnSendAttemptThresholdExceeded()
        {
            View.Invoke(new RunPendingTransactionFormDelegate(RunPendingTransactionForm));
        }

        private delegate void RunPendingTransactionFormDelegate();
        private void RunPendingTransactionForm()
        {
            PendingTransactionController controller = Prepare<PendingTransactionController>();
            controller.Execute();
        }

        #endregion        
    }
}
