using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Dalworth.Data;
using Dalworth.Domain.SyncService;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.DeclineVisit
{
    public class DeclineVisitController : SingleFormController<DeclineVisitModel, DeclineVisitView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
            set { m_isCancelled = value; }
        }

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
            IsLeftActionExist = true;
            IsRightActionExist = true;
            LeftActionName = "Back";
            RightActionName = "Done";
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_txtReason.Focus();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return false;
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
                    Database.Begin();
                    Model.DeclineVisit(View.m_txtReason.Text);
                    Database.Commit();
                    WorkTransaction.Send();
                    m_isCancelled = false;
                    View.Destroy();
                }
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("DeclineVisitController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }
            
        }

        #endregion
    }
}
