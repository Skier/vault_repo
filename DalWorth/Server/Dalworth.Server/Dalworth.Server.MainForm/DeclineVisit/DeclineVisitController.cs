using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.DeclineVisit
{
    public class DeclineVisitController : Controller<DeclineVisitModel, DeclineVisitView>
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
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            //Decline operation is not used right now

//            try
//            {
//                Database.Begin();
//                Visit.Decline(Model.Work.TechnicianEmployeeId, Model.Visit.ID, View.m_txtReason.Text,
//                    Model.Work.StartDate.Value.Date == DateTime.Now.Date, Model.Work);                
//                Database.Commit();
//                View.Destroy();
//            }
//            catch (Exception)
//            {
//                Database.Rollback();
//                throw;
//            }                
            
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
