using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Dalworth.Data;
using Dalworth.Domain.SyncService;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.NoGo
{
    public class NoGoController : SingleFormController<NoGoModel, NoGoView>
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
            View.m_btnYes.Click += OnYesClick;
            View.m_btnNo.Click += OnNoClick;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_btnNo.Focus();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            IsCancelled = true;
            return base.OnSave();
        }

        #endregion

        #region OnNoClick

        private void OnNoClick(object sender, EventArgs e)
        {
            IsCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnYesClick

        private void OnYesClick(object sender, EventArgs e)
        {
            try
            {
                using (new WaitCursor())
                {
                    Database.Begin();
                    Model.SubmitNoGo();
                    Database.Commit();
                    WorkTransaction.Send();
                    View.Destroy();
                }
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("NoGoController::OnYesClick", ex.Message + ex.StackTrace);
                return;
            }
        }

        #endregion

    }
}
