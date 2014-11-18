using System;
using System.Drawing;
using System.Windows.Forms;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.CreditCardCharges
{
    #region PayeeType

    public enum PayeeType
    {
        Vendor = 0,
        Customer = 1,
        Employee = 2
    }

    #endregion

    #region EnteredFrom

    public enum EnteredFromEnum { ManageCreditCards }

    #endregion    
    
    public class CreditCardController : SingleFormController<CreditCardModel, CreditCardView>
    {
        #region Feilds

        private decimal m_currentAmountLeft;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region CreatedCard

        private CreditCard m_createdCard;
        public CreditCard CreatedCard
        {
            get { return m_createdCard; }
        }

        #endregion

        private EnteredFromEnum? m_enteredFrom;
        private CreditCard m_incomingCard;
        private bool? m_isReadOnly;
        private Account m_incomingAccount;

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {            
            View.m_cmbCreditCard.SelectedIndexChanged += new EventHandler(OnCreditCardChanged);
            View.m_cmbPayeeType.SelectedIndexChanged += new EventHandler(OnPayeeTypeChanged);
            View.m_table.LostFocus += new EventHandler(OnTableFocusChanged);
            View.m_table.GotFocus += new EventHandler(OnTableFocusChanged);
            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_table.Enter += new CellValueHandler(OnTableEnter);

            View.m_menuAddExpence.Click += new EventHandler(OnAddExpenceClick);
            View.m_menuEditExpence.Click += new EventHandler(OnEditExpenceClick);
            View.m_menuDeleteExpence.Click += new EventHandler(OnDeleteExpenceClick);

            View.m_tabs.SelectedIndexChanged += new EventHandler(OnTabChanged);
            Model.TotalExpencesChanged += new CreditCardModel.TotalExpencesChangedHandler(OnTotalExpencesChanged);
            View.m_curAmount.TextChanged += new EventHandler(OnAmountChanged);


            InitCreditCardsCombo();
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_enteredFrom = null;
                m_incomingCard = null;
                m_isReadOnly = false;
                m_incomingAccount = null;
            } else
            {
                if (data.Length >= 1)
                    m_enteredFrom = (EnteredFromEnum)data[0];

                if (data.Length >= 2)
                    m_incomingCard = (CreditCard)data[1];

                if (data.Length >= 3)
                    m_isReadOnly = (bool)data[2];

                if (data.Length >= 4)
                    m_incomingAccount = (Account)data[3];                
            }            

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_table.BindModel(Model);
            View.m_table.AddColumn(
                new TableColumn(0, 0,
                                new ExpenceLineTableCellRenderer(),
                                new DefaultTableCellEditor(),
                                new ExpenceLineTableHeaderRenderer()));

            View.m_table.AddColumn(
                new TableColumn(1, 60,
                                new ExpenceLineTableCellRenderer(),
                                new DefaultTableCellEditor(),
                                new ExpenceLineTableHeaderRenderer()));

            if (m_isReadOnly.HasValue && m_isReadOnly.Value)
                View.m_tabs.Focus();
            else
                View.m_cmbCreditCard.Focus();

            IsDefaultActionExist = true;
            DefaultActionName = "Cancel";

            View.m_cmbTransactionType.SelectedIndex = 0;

            PopulateIncomingInfo();
        }

        #endregion        

        #region OnCreditCardChanged

        private void OnCreditCardChanged(object sender, EventArgs e)
        {
            Account account = (Account)View.m_cmbCreditCard.SelectedItem;

            if (account != null)
            {
                decimal balance = Account.GetBalance(account);
                View.m_lblBalance.Text = balance.ToString("0.00");
                if (balance < 0)
                    View.m_lblBalance.ForeColor = Color.Red;
                else
                    View.m_lblBalance.ForeColor = Color.Black;                
            }
        }

        #endregion

        #region OnPayeeTypeChanged

        private void OnPayeeTypeChanged(object sender, EventArgs e)
        {
            if (View.m_cmbPayeeType.SelectedIndex >= 0)
            {
                View.m_cmbPayee.Items.Clear();
                UpdatePayeeCombo((PayeeType)View.m_cmbPayeeType.SelectedIndex);
            }
        }

        #endregion        

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            if (IsChanged()
                && MessageDialog.Show(MessageDialogType.Question,
                "All changes will be lost. Continue?") == DialogResult.No)
                return;

            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnAddExpenceClick

        private void OnAddExpenceClick(object sender, EventArgs e)
        {
            ExpenceLineController expenceLineController
                = Prepare<ExpenceLineController>(Model);
            expenceLineController.Closed += new SingleFormClosedHandler(OnExpenceLineAddClosed);
            expenceLineController.Execute();
        }

        private void OnExpenceLineAddClosed(SingleFormController controller)
        {
            View.m_tabs.SelectedIndex = 2;
            View.m_table.Focus();
            if (!((ExpenceLineController)controller).IsCanceled)
                View.m_table.Select(0);
        }

        #endregion

        #region OnDeleteExpenceClick

        private void OnDeleteExpenceClick(object sender, EventArgs e)
        {
            Model.DeleteExpenceLine(
                (CreditCardExpenceLine)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));

            if (Model.GetRowCount() <= 0)
            {
                View.m_menuEditExpence.Enabled = false;
                View.m_menuDeleteExpence.Enabled = false;
            }
        }

        #endregion

        #region OnEditExpenceClick

        private void OnEditExpenceClick(object sender, EventArgs e)
        {
            ExpenceLineController expenceLineController
                = Prepare<ExpenceLineController>(Model,
                    Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));
            expenceLineController.Closed += new SingleFormClosedHandler(OnExpenceLineEditClosed);
            expenceLineController.Execute();
        }

        private void OnExpenceLineEditClosed(SingleFormController controller)
        {
            View.m_table.Focus();
        }

        #endregion

        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            if (Model.GetRowCount() > 0 && rowIndex >= 0)
            {
                View.m_menuEditExpence.Enabled = true;
                View.m_menuDeleteExpence.Enabled = true;
            }
            else
            {
                View.m_menuEditExpence.Enabled = false;
                View.m_menuDeleteExpence.Enabled = false;
            }

        }

        #endregion

        #region OnTableFocusChanged

        private void OnTableFocusChanged(object sender, EventArgs e)
        {
            if (!View.m_table.Focused)
            {
                View.m_menuEditExpence.Enabled = false;
                View.m_menuDeleteExpence.Enabled = false;
            }
            else if (Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
            {
                View.m_menuEditExpence.Enabled = true;
                View.m_menuDeleteExpence.Enabled = true;
            }
        }

        #endregion

        #region OnTableEnter

        private void OnTableEnter(TableCell cell)
        {
            if (Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
                OnEditExpenceClick(this, EventArgs.Empty);
        }

        #endregion

        #region OnTabChanged

        private void OnTabChanged(object sender, EventArgs e)
        {
            if (View.m_tabs.SelectedIndex != 0)
            {
                View.m_menuDeleteExpence.Enabled = false;
                View.m_menuEditExpence.Enabled = false;

                if (View.m_curAmount.Value.Value <= 0)
                {
                    View.m_tabs.SelectedIndex = 0;
                    MessageDialog.Show(MessageDialogType.Information, "Amount should be greater than zero");
                    View.m_curAmount.Focus();
                    View.m_curAmount.SelectAll();
                    return;
                }
            }
        }

        #endregion

        #region OnTotalExpencesChanged

        private void OnTotalExpencesChanged(decimal totalExpences)
        {
            UpdateAmountLeft();
        }

        #endregion

        #region OnAmountChanged

        private void OnAmountChanged(object sender, EventArgs e)
        {
            UpdateAmountLeft();
        }

        #endregion

        #region UpdateAmountLeft

        private void UpdateAmountLeft()
        {
            try
            {
                decimal currentAmount = View.m_curAmount.Value.Value;
                m_currentAmountLeft = currentAmount - Model.TotalExpences;
                View.m_menuAddExpence.Enabled = (currentAmount != 0);
                View.m_lblAmountLeft.Text = m_currentAmountLeft.ToString("0.00");
            }
            catch (Exception)
            {
                m_currentAmountLeft = -1;
                View.m_menuAddExpence.Enabled = false;
                View.m_lblAmountLeft.Text = "Undefined";
            }

            Model.AmountLeft = m_currentAmountLeft;
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            if (m_isReadOnly.HasValue && m_isReadOnly.Value)
            {
                m_isCancelled = true;
                return true;
            }


            if (!IsFormValid())
                return false;

            CreditCard creditCard = new CreditCard();
            if (m_incomingCard != null)
            {
                creditCard.CreditCardId = m_incomingCard.CreditCardId;
                creditCard.EntityState = EntityState.Created;
            }

            creditCard.Account = (Account)View.m_cmbCreditCard.SelectedItem;
            creditCard.TxnDate = View.m_dtpDate.Value;

            if (View.m_cmbPayee.SelectedItem != null)
            {
                if (((PayeeType)View.m_cmbPayeeType.SelectedIndex) == PayeeType.Vendor)
                    creditCard.PayeeQBEntityId = ((Vendor)View.m_cmbPayee.SelectedItem).QuickBooksListId;
                else if (((PayeeType)View.m_cmbPayeeType.SelectedIndex) == PayeeType.Customer)
                    creditCard.PayeeQBEntityId = ((Customer)View.m_cmbPayee.SelectedItem).QuickBooksListId;
                else if (((PayeeType)View.m_cmbPayeeType.SelectedIndex) == PayeeType.Employee)
                    creditCard.PayeeQBEntityId = ((Employee)View.m_cmbPayee.SelectedItem).QuickBooksListId;
            }

            creditCard.RefNumber = View.m_txtRefNumber.Text;

            creditCard.Amount = View.m_curAmount.Value.Value;
            creditCard.Memo = View.m_txtNotes.Text;

            if (View.m_cmbTransactionType.SelectedIndex == 0)
                creditCard.CreditCardType = CreditCardType.Charge;
            else
                creditCard.CreditCardType = CreditCardType.Credit;
            
            Database.Begin();
            try
            {
                Model.Save(creditCard, m_incomingCard != null);
                Database.Commit();
                m_createdCard = creditCard;
                m_isCancelled = false;
                return true;
            }
            catch (Exception ex)
            {
                Database.Rollback();
                throw ex;
            }
        }

        #endregion

        #region IsFormValid

        private bool IsFormValid()
        {
            if (View.m_cmbCreditCard.SelectedItem == null)
            {
                MessageDialog.Show(MessageDialogType.Information, "Credit Card should be selected");
                View.m_tabs.SelectedIndex = 0;
                View.m_cmbCreditCard.Focus();
                return false;
            }

            if (View.m_curAmount.Value.Value <= 0)
            {
                MessageDialog.Show(MessageDialogType.Information, "Amount should be greater than zero");
                View.m_tabs.SelectedIndex = 0;
                View.m_curAmount.Focus();
                View.m_curAmount.SelectAll();
                return false;
            }

            if (View.m_curAmount.Value.Value != Model.TotalExpences)
            {
                MessageDialog.Show(MessageDialogType.Information, "Amount should be equal to the expences total amount");
                View.m_tabs.SelectedIndex = 2;
                View.m_table.Focus();
                return false;
            }

            if (Model.IsContainsAPLine()
                && (View.m_cmbPayee.SelectedItem == null || !(View.m_cmbPayee.SelectedItem is Vendor)))
            {
                MessageDialog.Show(MessageDialogType.Information, "Vendor should be selected");
                View.m_tabs.SelectedIndex = 0;
                View.m_cmbPayee.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region IsChanged

        private bool IsChanged()
        {
            if (m_isReadOnly.HasValue && m_isReadOnly.Value)
                return false;

            if (m_incomingAccount != null)
                return false;

            if (View.m_cmbCreditCard.SelectedItem != null
                || View.m_cmbTransactionType.SelectedIndex != 0
                || View.m_cmbPayeeType.SelectedItem != null
                || View.m_cmbPayee.SelectedItem != null
                || View.m_txtRefNumber.Text != string.Empty
                || View.m_dtpDate.Value.Date != DateTime.Now.Date
                || View.m_curAmount.Value.Value != 0
                || View.m_txtNotes.Text != string.Empty
                || Model.GetRowCount() > 0)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region InitCreditCardsCombo

        private void InitCreditCardsCombo()
        {
            foreach (Account account in Model.CreditCards)
                View.m_cmbCreditCard.Items.Add(account);
        }

        #endregion

        #region UpdatePayeeCombo

        private void UpdatePayeeCombo(PayeeType payeeType)
        {
            View.m_cmbPayee.Items.Clear();

            if (payeeType == PayeeType.Vendor)
            {
                foreach (Vendor vendor in Model.Vendors)
                    View.m_cmbPayee.Items.Add(vendor);
            }
            else if (payeeType == PayeeType.Customer)
            {
                foreach (Customer customer in Model.Customers)
                    View.m_cmbPayee.Items.Add(customer);
            }
            else if (payeeType == PayeeType.Employee)
            {
                foreach (Employee employee in Model.Employees)
                    View.m_cmbPayee.Items.Add(employee);
            }
        }

        #endregion        

        #region PopulateIncomingInfo

        private void PopulateIncomingInfo()
        {
            if (m_enteredFrom != null && m_enteredFrom == EnteredFromEnum.ManageCreditCards
                && m_incomingAccount != null)
            {
                foreach (Account bankAccount in Model.CreditCards)
                {
                    if (bankAccount.AccountId == m_incomingAccount.AccountId)
                    {
                        View.m_cmbCreditCard.SelectedItem = bankAccount;
                        OnCreditCardChanged(this, EventArgs.Empty);
                        break;
                    }
                }

                if (m_enteredFrom != null && m_enteredFrom == EnteredFromEnum.ManageCreditCards)
                {
                    View.m_cmbCreditCard.Enabled = false;
                    View.m_lblCreditCard.Enabled = false;
                }
            }

            //Populate info
            if (m_incomingCard != null)
            {
                if (m_isReadOnly.HasValue)
                {
                    if (m_isReadOnly.HasValue && m_isReadOnly.Value)
                        View.Text = "View Credit Card Charge - Q-Agent";
                    else
                        View.Text = "Edit Credit Card Charge - Q-Agent";
                }

                if (m_incomingCard.PayeeQBEntityId != null)
                {
                    QBEntity entity
                        = QBEntity.FindByPrimaryKey(m_incomingCard.PayeeQBEntityId.Value);

                    if (entity.QBEntityType == QBEntityType.Vendor)
                    {
                        View.m_cmbPayeeType.SelectedIndex = 0;
                        OnPayeeTypeChanged(this, EventArgs.Empty);

                        foreach (Vendor vendor in Model.Vendors)
                            if (vendor.QuickBooksListId == m_incomingCard.PayeeQBEntityId)
                            {
                                View.m_cmbPayee.SelectedItem = vendor;
                                break;
                            }
                    }

                    else if (entity.QBEntityType == QBEntityType.Customer)
                    {
                        View.m_cmbPayeeType.SelectedIndex = 1;
                        OnPayeeTypeChanged(this, EventArgs.Empty);

                        foreach (Customer customer in Model.Customers)
                            if (customer.QuickBooksListId == m_incomingCard.PayeeQBEntityId)
                            {
                                View.m_cmbPayee.SelectedItem = customer;
                                break;
                            }
                    }

                    else if (entity.QBEntityType == QBEntityType.Employee)
                    {
                        View.m_cmbPayeeType.SelectedIndex = 2;
                        OnPayeeTypeChanged(this, EventArgs.Empty);

                        foreach (Employee employee in Model.Employees)
                            if (employee.QuickBooksListId == m_incomingCard.PayeeQBEntityId)
                            {
                                View.m_cmbPayee.SelectedItem = employee;
                                break;
                            }
                    }                    
                }

                if (m_incomingCard.CreditCardType == CreditCardType.Charge)
                    View.m_cmbTransactionType.SelectedIndex = 0;
                else
                    View.m_cmbTransactionType.SelectedIndex = 1;

                View.m_txtRefNumber.Text = m_incomingCard.RefNumber;
                View.m_dtpDate.Value = m_incomingCard.TxnDate ?? DateTime.Now;

                if (m_incomingCard.Amount.HasValue)
                    View.m_curAmount.Value = m_incomingCard.Amount;

                View.m_txtNotes.Text = m_incomingCard.Memo;
                Model.LoadExpenceLines(m_incomingCard);
            }

            if (m_isReadOnly.HasValue && m_isReadOnly.Value)
            {
                View.m_tabGeneral.Enabled = false;
                View.m_tabNotes.Enabled = false;
                View.m_tabExpences.Enabled = false;

                View.m_menuAddExpence.Enabled = false;
                View.m_menuDeleteExpence.Enabled = false;
                View.m_menuEditExpence.Enabled = false;
            }


        }

        #endregion
        
    }
}
