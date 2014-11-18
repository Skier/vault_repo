using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.PayByCheck;

namespace Dalworth.Windows.ServiceVisit.PayByCheckDone
{
    public class PayByCheckDoneController : SingleFormController<PayByCheckDoneModel, PayByCheckDoneView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.PayByCheckModel = (PayByCheckModel)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsRightActionExist = true;
            RightActionName = "Done";

            if (Model.PayByCheckModel.PaymentResult.IsAccepted)
                View.m_lblStatus.Text = "Bank check payment has been successfully processed";
            else
                View.m_lblStatus.Text = "Bank check payment has been failed";

            View.m_lblCheckNumber.Text = Model.PayByCheckModel.PaymentResult.BankCheckNumber;
            View.m_lblAmount.Text = Model.PayByCheckModel.PaymentResult.Amount.ToString("C");
            View.m_lblResponse.Text = Model.PayByCheckModel.PaymentResult.ServerResponse;
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            View.Destroy();
        }

        #endregion
    }
}
