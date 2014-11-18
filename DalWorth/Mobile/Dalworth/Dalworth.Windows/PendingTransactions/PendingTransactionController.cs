using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Windows.PendingTransactionDetails;

namespace Dalworth.Windows.PendingTransactions
{
    public class PendingTransactionController : SingleFormController<PendingTransactionModel, PendingTransactionView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_table.Enter += OnTableEnter;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = true;
            IsRightActionExist = true;

            LeftActionName = "Details";
            RightActionName = "Close";

            View.m_table.AddColumn(new TableColumn(0, 40));
            View.m_table.AddColumn(new TableColumn(1));
            View.m_table.AddColumn(new TableColumn(2, 60));
            View.m_table.BindModel(Model);
            View.m_table.Select(0, 0);
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            if (View.m_table.CurrentRowIndex >= 0)
            {
                PendingTransactionDetailsController controller
                    = Prepare<PendingTransactionDetailsController>(Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));
                controller.Execute();                
            } 
        }

        private void OnTableEnter(TableCell cell)
        {
            OnLeftAction();
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
