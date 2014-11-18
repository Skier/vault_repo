using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.Banking.CreditCardCharges
{       
    public enum CustomerOrVendor {Customer, Vendor}
    
    public class ExpenceLineController : SingleFormController<ExpenceLineModel, ExpenceLineView>
    {
        #region Fields

        #region IsCanceled

        private bool m_isCanceled;
        public bool IsCanceled
        {
            get { return m_isCanceled; }
        }

        #endregion        

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.CreditCardModel = (CreditCardModel) data[0];

            if (data.Length >= 2)
                Model.EditedExpenceLine = (CreditCardExpenceLine) data[1];
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsDefaultActionExist = true;
            DefaultActionName = "Cancel";
            
            foreach (Account account in Model.CreditCardModel.Accounts.Values)
                View.m_cmbAccount.Items.Add(account);                       
            
            foreach (Customer customer in Model.CreditCardModel.Customers)
                View.m_cmbCustomer.Items.Add(customer);    
            
            if (Model.EditedExpenceLine != null)
            {                                
                if (Model.EditedExpenceLine.Account != null)
                {
                    View.m_cmbAccount.SelectedItem
                        = Model.CreditCardModel.Accounts[Model.EditedExpenceLine.Account.AccountId];
                }                                
                                
                if (Model.EditedExpenceLine.Customer != null)
                    foreach (Customer customer in Model.CreditCardModel.Customers)
                        if (customer.CustomerId == Model.EditedExpenceLine.Customer.CustomerId)
                        {
                            View.m_cmbCustomer.SelectedItem = customer;
                            break;
                        }


                View.m_curAmount.Value = Model.EditedExpenceLine.Amount;                
                View.m_txtMemo.Text = Model.EditedExpenceLine.Memo;

                View.m_lblAmountLeft.Text 
                    = (Model.CreditCardModel.AmountLeft 
                       + Model.EditedExpenceLine.Amount.Value).ToString("0.00");
            } else
            {
                View.m_lblAmountLeft.Text = Model.CreditCardModel.AmountLeft.ToString("0.00");
            }
            
            View.m_cmbAccount.Focus();

            
        }

        #endregion

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            m_isCanceled = true;
            View.Destroy();            
        }

        #endregion        

        #region OnSave

        protected override bool OnSave()
        {
            if (!IsFormValid())
                return false;

            m_isCanceled = false;
                
            if (Model.EditedExpenceLine != null)
            {
                Model.EditedExpenceLine.Account = (Account) View.m_cmbAccount.SelectedItem;
                Model.EditedExpenceLine.Customer = (Customer) View.m_cmbCustomer.SelectedItem;
                                                               
                Model.EditedExpenceLine.Amount = View.m_curAmount.Value;
                Model.EditedExpenceLine.Memo = View.m_txtMemo.Text;
                    
                Model.CreditCardModel.UpdateTable();

                return true;
            }

            CreditCardExpenceLine expenceLine = new CreditCardExpenceLine(
                (Account) View.m_cmbAccount.SelectedItem,
                null,
                (Customer) View.m_cmbCustomer.SelectedItem,
                0,
                null,
                View.m_curAmount.Value,
                View.m_txtMemo.Text);
            
            Model.CreditCardModel.AddExpenceLine(expenceLine);

            return true;
        }

        #endregion

        #region IsFormValid

        private bool IsFormValid()
        {
            if (View.m_cmbAccount.SelectedItem == null)
            {
                MessageDialog.Show(MessageDialogType.Information, "Account should be selected");                
                View.m_cmbAccount.Focus();
                return false;                
            }

            Account account = (Account) View.m_cmbAccount.SelectedItem;
                        
            if (account.AccountType == AccountType.AccountsReceivable
                && View.m_cmbCustomer.SelectedItem == null)
            {
                MessageDialog.Show(MessageDialogType.Information, "Customer should be selected");                
                View.m_cmbCustomer.Focus();
                return false;                                                    
            }

            decimal amountLeft = Model.CreditCardModel.AmountLeft;
            if (Model.EditedExpenceLine != null)
                amountLeft = Model.EditedExpenceLine.Amount.Value + amountLeft;
            
            if (View.m_curAmount.Value.Value > amountLeft)
            {
                MessageDialog.Show(MessageDialogType.Information, "Amount shouldn't be greater than amount left");
                View.m_curAmount.Focus();
                View.m_curAmount.SelectAll();
                return false;                                                                    
            }
            
            return true;
        }

        #endregion        
    }
}
