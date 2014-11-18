using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.ServiceVisit;

namespace Dalworth.Windows.ServiceVisit.CustomerMessage
{
    public class CustomerMessageController : SingleFormController<CustomerMessageModel, CustomerMessageView>
    {
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
            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsRightActionExist = true;
            RightActionName = "Next";

            View.m_txtMessage.Text = Model.Visit.Tasks[0].Task.Message;
            View.m_txtMessage.Focus();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return false;
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            ServiceVisitController controller = Prepare<ServiceVisitController>(Model.Visit);
            controller.Closed += OnServiceVisitClosed;
            controller.Execute();
        }

        private void OnServiceVisitClosed(SingleFormController controller)
        {
            View.Destroy();
        }

        #endregion
    }
}
