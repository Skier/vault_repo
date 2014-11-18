using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.WriteCheck
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

    public enum EnteredFromEnum { ManageChecks }

    #endregion
    
    public class WriteCheckController : SingleFormController<WriteCheckModel, WriteCheckView>
    {
        #region Feilds

        private string m_latestCheckNumber;        
        private decimal m_currentAmountLeft;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region CreatedCheck

        private Check m_createdCheck;
        public Check CreatedCheck
        {
            get { return m_createdCheck; }
        }

        #endregion

        private EnteredFromEnum? m_enteredFrom;
        private Check m_incomingCheck;
        private bool? m_isReadOnly;
        private Account m_incomingAccount;

        #endregion
        
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_chkToBePrinted.CheckStateChanged += new EventHandler(OnToBePrintedChanged);
            View.m_cmbBankAccount.SelectedIndexChanged += new EventHandler(OnBankAccountChanged);
            View.m_cmbPayeeType.SelectedIndexChanged += new EventHandler(OnPayeeTypeChanged);
            View.m_cmbPayee.SelectedIndexChanged += new EventHandler(OnPayeeChanged);            
            View.m_table.LostFocus += new EventHandler(OnTableFocusChanged);
            View.m_table.GotFocus += new EventHandler(OnTableFocusChanged);
            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_table.Enter += new CellValueHandler(OnTableEnter);

            View.m_menuAddExpence.Click += new EventHandler(OnAddExpenceClick);
            View.m_menuEditExpence.Click += new EventHandler(OnEditExpenceClick);
            View.m_menuDeleteExpence.Click += new EventHandler(OnDeleteExpenceClick);
            
            View.m_tabs.SelectedIndexChanged += new EventHandler(OnTabChanged);
            Model.TotalExpencesChanged += new WriteCheckModel.TotalExpencesChangedHandler(OnTotalExpencesChanged);
            View.m_curAmount.TextChanged += new EventHandler(OnAmountChanged);
            
            
            InitBankAccountsCombo();
        }        

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {                        
            if (data.Length >= 1)
            {
                if (data[0] != null)
                    m_enteredFrom = (EnteredFromEnum)data[0];
            } else
                return;
                
                
            if (data.Length >= 2)
                 m_incomingCheck = (Check) data[1];
                
            if (data.Length >= 3)
                m_isReadOnly = (bool) data[2];                
            
            if (data.Length >= 4)
                m_incomingAccount = (Account) data[3]; 
            
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
                View.m_cmbBankAccount.Focus();
            
            IsDefaultActionExist = true;
            DefaultActionName = "Cancel";            

            PopulateIncomingInfo();
        }

        #endregion

        #region OnToBePrintedChanged

        private void OnToBePrintedChanged(object sender, EventArgs e)
        {
            if (View.m_chkToBePrinted.Checked)
            {
                m_latestCheckNumber = View.m_txtCheckNumber.Text;
                View.m_txtCheckNumber.Text = "To Print";
            }
            else
                View.m_txtCheckNumber.Text = m_latestCheckNumber;
            
            View.m_lblCheckNumber.Enabled = !View.m_chkToBePrinted.Checked;
            View.m_txtCheckNumber.Enabled = !View.m_chkToBePrinted.Checked;
        }

        #endregion

        #region OnBankAccountChanged

        private void OnBankAccountChanged(object sender, EventArgs e)
        {
            Account account = (Account) View.m_cmbBankAccount.SelectedItem;            
            
            if (account != null)
            {
                decimal balance = Account.GetBalance(account);
                View.m_lblBalance.Text = balance.ToString("0.00");
                if (balance < 0)
                    View.m_lblBalance.ForeColor = Color.Red;
                else
                    View.m_lblBalance.ForeColor = Color.Black;

                View.m_txtCheckNumber.Text = Check.FindNextCheckNumber(account);
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

        #region OnPayeeChanged

        private void OnPayeeChanged(object sender, EventArgs e)
        {            
            if (View.m_cmbPayee.SelectedItem != null)
            {
                if (View.m_cmbPayee.SelectedItem is Vendor)
                {
                    Vendor vendor = (Vendor) View.m_cmbPayee.SelectedItem;
                    View.m_lblPrintAs.Text = vendor.NameOnCheck;
                    View.m_lblAddress.Text =
                        FormatAddress(vendor.Name, vendor.CompanyName, vendor.Addr1, vendor.Addr2, 
                            vendor.Addr3, vendor.Addr4, vendor.City, vendor.State, 
                            vendor.PostalCode, vendor.Country);
                    
                } else if (View.m_cmbPayee.SelectedItem is Customer)
                {
                    Customer customer = (Customer) View.m_cmbPayee.SelectedItem;
                    View.m_lblPrintAs.Text = customer.PrintAs;

                    View.m_lblAddress.Text =
                        FormatAddress(customer.FullName, customer.CompanyName, customer.BillAddr1, 
                            customer.BillAddr2, customer.BillAddr3, customer.BillAddr4,
                            customer.BillCity, customer.BillState, customer.BillPostalCode, customer.BillCountry);                    
                    
                } else if (View.m_cmbPayee.SelectedItem is Employee)
                {
                    Employee employee = (Employee) View.m_cmbPayee.SelectedItem;
                    View.m_lblPrintAs.Text = employee.PrintAs;

                    View.m_lblAddress.Text =
                        FormatAddress(employee.Name, null, employee.Addr1, employee.Addr2,
                            employee.Addr3, employee.Addr4, employee.City, employee.State,
                            employee.PostalCode, employee.Country);                    
                }
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
                = SingleFormController.Prepare<ExpenceLineController>(Model);
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
                (CheckExpenceLine) Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));
            
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
                = SingleFormController.Prepare<ExpenceLineController>(Model, 
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
            } else
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
            } else if (Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
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
                        
            Check check = new Check();
            if (m_incomingCheck != null)
            {
                check.CheckId = m_incomingCheck.CheckId;
                check.EntityState = EntityState.Created;
            }
                
            check.Account = (Account) View.m_cmbBankAccount.SelectedItem;
            check.TxnDate = View.m_dtpDate.Value;

            if (View.m_cmbPayee.SelectedItem != null)
            {
                if (((PayeeType)View.m_cmbPayeeType.SelectedIndex) == PayeeType.Vendor)
                    check.PayeeQBEntityId = ((Vendor) View.m_cmbPayee.SelectedItem).QuickBooksListId;
                else if (((PayeeType)View.m_cmbPayeeType.SelectedIndex) == PayeeType.Customer)
                    check.PayeeQBEntityId = ((Customer)View.m_cmbPayee.SelectedItem).QuickBooksListId;
                else if (((PayeeType)View.m_cmbPayeeType.SelectedIndex) == PayeeType.Employee)
                    check.PayeeQBEntityId = ((Employee)View.m_cmbPayee.SelectedItem).QuickBooksListId;                    
            }

            if (!View.m_chkToBePrinted.Checked && View.m_txtCheckNumber.Text != string.Empty)
                check.RefNumber = View.m_txtCheckNumber.Text;

            check.Amount = View.m_curAmount.Value.Value;
            check.Memo = View.m_txtMemo.Text;
            check.IsToBePrinted = View.m_chkToBePrinted.Checked;                        
            
            Database.Begin();
            try
            {
                Model.Save(check, m_incomingCheck != null);
                Database.Commit();
                m_createdCheck = check;
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
            if (View.m_cmbBankAccount.SelectedItem == null)
            {
                MessageDialog.Show(MessageDialogType.Information, "Bank Account should be selected");
                View.m_tabs.SelectedIndex = 0;
                View.m_cmbBankAccount.Focus();
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

            if (m_incomingCheck == null && !View.m_chkToBePrinted.Checked && View.m_txtCheckNumber.Text != string.Empty
                && Check.IsCheckNumberExist(View.m_txtCheckNumber.Text, (Account) View.m_cmbBankAccount.SelectedItem))
            {
                MessageDialog.Show(MessageDialogType.Information, "Check with same number already exist");
                View.m_tabs.SelectedIndex = 0;
                View.m_txtCheckNumber.Focus();
                View.m_txtCheckNumber.SelectAll();
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
            
            if (View.m_cmbBankAccount.SelectedItem != null
                || View.m_cmbPayeeType.SelectedItem != null
                || View.m_cmbPayee.SelectedItem != null
                || View.m_txtCheckNumber.Text != string.Empty
                || View.m_dtpDate.Value.Date != DateTime.Now.Date
                || View.m_chkToBePrinted.Checked
                || View.m_curAmount.Value.Value != 0
                || View.m_txtMemo.Text != string.Empty
                || Model.GetRowCount() > 0)
            {
                return true;
            }
            
            return false;                
        }

        #endregion

        #region InitBankAccountsCombo

        private void InitBankAccountsCombo()
        {
            foreach (Account account in Model.BankAccounts)
                View.m_cmbBankAccount.Items.Add(account);
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

        #region FormatAddress

        private string FormatAddress(string name, string company, string addr1, 
             string addr2, string addr3, string addr4, string city, string state, 
             string postalCode, string country)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(name))
                result += name;

            if (!string.IsNullOrEmpty(company))
                if (result == string.Empty)
                    result += company;
                else
                    result += "\n" + company;
                        

            string thirdLine = string.Empty;            
            if (!string.IsNullOrEmpty(addr1))
                thirdLine += addr1;
            
            if (!string.IsNullOrEmpty(addr2))
                if (thirdLine == string.Empty)
                    thirdLine += addr2;
                else
                    thirdLine += " " + addr2;
            
            if (!string.IsNullOrEmpty(addr3))
                if (thirdLine == string.Empty)
                    thirdLine += addr3;
                else
                    thirdLine += " " + addr3;
            
            if (!string.IsNullOrEmpty(addr4))
                if (thirdLine == string.Empty)
                    thirdLine += addr4;
                else
                    thirdLine += " " + addr4;


            string fourthLine = string.Empty;
            if (!string.IsNullOrEmpty(city))
            {
                fourthLine += city;
                if (!string.IsNullOrEmpty(state) || !string.IsNullOrEmpty(postalCode)
                    || !string.IsNullOrEmpty(country))
                    fourthLine += ",";
            }

            if (!string.IsNullOrEmpty(state))
                if (fourthLine == string.Empty)
                    fourthLine += state;
                else
                    fourthLine += " " + state;
            
            if (!string.IsNullOrEmpty(postalCode))
                if (fourthLine == string.Empty)
                    fourthLine += postalCode;
                else
                    fourthLine += " " + postalCode;
            
            if (!string.IsNullOrEmpty(country))
                if (fourthLine == string.Empty)
                    fourthLine += country;
                else
                    fourthLine += " " + country;

            if (thirdLine != string.Empty)
                if (result == string.Empty)
                    result += thirdLine;
                else
                    result += "\n" + thirdLine;

            if (fourthLine != string.Empty)
                if (result == string.Empty)
                    result += fourthLine; 
                else
                    result += "\n" + fourthLine;

            return result;                                    
        }

        #endregion                

        #region PopulateIncomingInfo

        private void PopulateIncomingInfo()
        {
            if (m_enteredFrom != null && m_enteredFrom == EnteredFromEnum.ManageChecks
                && m_incomingAccount != null)
            {
                foreach (Account bankAccount in Model.BankAccounts)
                {
                    if (bankAccount.AccountId == m_incomingAccount.AccountId)
                    {
                        View.m_cmbBankAccount.SelectedItem = bankAccount;
                        OnBankAccountChanged(this, EventArgs.Empty);
                        break;
                    }
                }

                if (m_enteredFrom != null && m_enteredFrom == EnteredFromEnum.ManageChecks)
                {
                    View.m_cmbBankAccount.Enabled = false;
                    View.m_lblBankAccount.Enabled = false;
                }
            }            
            
            //Populate info
            if (m_incomingCheck != null)
            {
                if (m_isReadOnly.HasValue)
                {
                    if (m_isReadOnly.HasValue && m_isReadOnly.Value)
                        View.Text = "View Check - Q-Agent";
                    else
                        View.Text = "Edit Check - Q-Agent";
                }
                                
                if (m_incomingCheck.PayeeQBEntityId != null)
                {
                    QBEntity entity
                        = QBEntity.FindByPrimaryKey(m_incomingCheck.PayeeQBEntityId.Value);
                    
                    if (entity.QBEntityType == QBEntityType.Vendor)
                    {
                        View.m_cmbPayeeType.SelectedIndex = 0;
                        OnPayeeTypeChanged(this, EventArgs.Empty);

                        foreach (Vendor vendor in Model.Vendors)
                            if (vendor.QuickBooksListId == m_incomingCheck.PayeeQBEntityId)
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
                            if (customer.QuickBooksListId == m_incomingCheck.PayeeQBEntityId)
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
                            if (employee.QuickBooksListId == m_incomingCheck.PayeeQBEntityId)
                            {
                                View.m_cmbPayee.SelectedItem = employee;
                                break;
                            }                                                        
                    }
                    
                    OnPayeeChanged(this, EventArgs.Empty);                    
                }

                View.m_txtCheckNumber.Text = m_incomingCheck.RefNumber;
                View.m_dtpDate.Value = m_incomingCheck.TxnDate ?? DateTime.Now;
                View.m_chkToBePrinted.Checked = m_incomingCheck.IsToBePrinted;
                if (View.m_chkToBePrinted.Checked)
                {
                    View.m_txtCheckNumber.Text = "To Print";
                    View.m_txtCheckNumber.Enabled = false;
                    View.m_lblCheckNumber.Enabled = false;
                    m_latestCheckNumber = Check.FindNextCheckNumber(m_incomingAccount);
                }
                    

                if (m_incomingCheck.Amount.HasValue)
                    View.m_curAmount.Value = m_incomingCheck.Amount;

                View.m_txtMemo.Text = m_incomingCheck.Memo;
                Model.LoadExpenceLines(m_incomingCheck);                   
            }
            
            if (m_isReadOnly.HasValue && m_isReadOnly.Value)
            {
                View.m_tabGeneral.Enabled = false;
                View.m_tabAdditional.Enabled = false;
                View.m_tabExpences.Enabled = false;

                View.m_menuAddExpence.Enabled = false;
                View.m_menuDeleteExpence.Enabled = false;
                View.m_menuEditExpence.Enabled = false;
            }
                
            
        }

        #endregion
    }
}
