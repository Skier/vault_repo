using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Banking.CreditCardCharges;
using QuickBooksAgent.Windows.UI.Banking.WriteCheck;

namespace QuickBooksAgent.Windows.UI.Banking.Menu
{
    public class BankingMenuController : SingleFormController<BankingMenuModel, BankingMenuView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_mbWriteCheck.Click += new EventHandler(OnWriteCheckClick);
            View.m_menuWriteCheck.Click += new EventHandler(OnWriteCheckClick);
            
            View.m_mbCreditCard.Click += new EventHandler(OnCreditCardClick);
            View.m_menuCreditCard.Click += new EventHandler(OnCreditCardClick);
            

            IsDefaultActionExist = false;
            DefaultActionName = "None";
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_mbWriteCheck.Focus();
        }

        #endregion

        #region WriteCheck

        private void OnWriteCheckClick(object sender, EventArgs e)
        {
            WriteCheckController writeCheckController
                = Prepare<WriteCheckController>(null, null, false, null);
            writeCheckController.Closed += new SingleFormClosedHandler(OnWriteCheckClosed);
            writeCheckController.Execute();                        
        }

        void OnWriteCheckClosed(SingleFormController controller)
        {
            View.m_mbWriteCheck.Select();
        }

        #endregion

        #region CreditCard

        private void OnCreditCardClick(object sender, EventArgs e)
        {
            CreditCardController creditCardController
                = Prepare<CreditCardController>();
            creditCardController.Closed += new SingleFormClosedHandler(OnCreditCardClosed);
            creditCardController.Execute();
        }

        void OnCreditCardClosed(SingleFormController controller)
        {
            View.m_mbCreditCard.Select();
        }

        #endregion
        
    }
}
