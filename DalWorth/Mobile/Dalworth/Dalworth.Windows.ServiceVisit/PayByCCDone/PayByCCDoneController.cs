using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.PayByCC;

namespace Dalworth.Windows.ServiceVisit.PayByCCDone
{
    public class PayByCCDoneController : SingleFormController<PayByCCDoneModel, PayByCCDoneView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.PayByCCModel = (PayByCCModel) data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsRightActionExist = true;
            RightActionName = "Done";

            if (Model.PayByCCModel.PaymentResult.IsAccepted)
                View.m_lblStatus.Text = "CC payment has been successfully processed";
            else
                View.m_lblStatus.Text = "CC payment has been failed";

            View.m_lblCCNumber.Text = Model.PayByCCModel.PaymentResult.CreditCardNumber;
            View.m_lblAmount.Text = Model.PayByCCModel.PaymentResult.Amount.ToString("C");
            View.m_lblConfirmationNumber.Text = Model.PayByCCModel.PaymentResult.ServerResponse;
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
