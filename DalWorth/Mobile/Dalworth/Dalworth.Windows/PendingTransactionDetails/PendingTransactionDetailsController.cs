using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;

namespace Dalworth.Windows.PendingTransactionDetails
{
    public class PendingTransactionDetailsController : SingleFormController<PendingTransactionDetailsModel, PendingTransactionDetailsView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Transaction = (WorkTransaction) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            IsRightActionExist = true;
            RightActionName = "Close";
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_txtText.Text = Model.GetWorkTransactionText();
            View.m_txtText.Focus();
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
