using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.WriteCheck
{
    public class WriteCheckModel : IModel, ITableModel
    {
        internal delegate void TotalExpencesChangedHandler(decimal totalExpences);
        internal event TotalExpencesChangedHandler TotalExpencesChanged;
        
        #region Feilds

        #region BankAccounts

        private List<Account> m_bankAccounts;
        public List<Account> BankAccounts
        {
            get { return m_bankAccounts; }
        }

        #endregion

        #region Vendors

        private List<Vendor> m_vendors;
        public List<Vendor> Vendors
        {
            get { return m_vendors; }
        }

        #endregion

        #region Customers

        private List<Customer> m_customers;
        public List<Customer> Customers
        {
            get { return m_customers; }
        }

        #endregion

        #region Employees

        private List<Employee> m_employees;
        public List<Employee> Employees
        {
            get { return m_employees; }
        }

        #endregion

        #region ExpenceLines

        private List<CheckExpenceLine> m_expenceLines;

        #endregion

        #region AmountLeft

        private decimal m_amountLeft;
        public decimal AmountLeft
        {
            get { return m_amountLeft; }
            set { m_amountLeft = value; }
        }

        #endregion

        #region Accounts

        private Dictionary<int, Account> m_accounts;
        internal Dictionary<int, Account> Accounts
        {
            get { return m_accounts; }
        }

        #endregion

        #region TotalExpences

        private decimal m_totalExpences;
        public decimal TotalExpences
        {
            get { return m_totalExpences; }
        }

        #endregion

        #endregion

        #region Init

        public void Init()
        {
            m_bankAccounts = Account.FindBy(AccountType.Bank, EntityState.Synchronized);
            m_vendors = Vendor.FindBy(EntityState.Synchronized);
            m_customers = Customer.FindBy(EntityState.Synchronized);
            m_employees = Employee.FindBy(EntityState.Synchronized);
            m_expenceLines = new List<CheckExpenceLine>();
            
            //Init accounts (all)
            m_accounts = new Dictionary<int, Account>();
            List<Account> accounts = Account.FindBy(EntityState.Synchronized);
            foreach (Account account in accounts)
                m_accounts.Add(account.AccountId, account);
        }

        #endregion

        #region LoadExpenceLines

        public void LoadExpenceLines(Check check)
        {
            m_expenceLines = CheckExpenceLine.FindBy(check);
            check.ExpenceLines = m_expenceLines;
            UpdateTable();            
        }

        #endregion

        #region ITableModel

        public int GetRowCount()
        {
            return m_expenceLines.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Total";
            return m_totalExpences.ToString("0.00");
        }

        public Type GetColumnClass(int columnIndex)
        {
            return typeof (string);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_accounts[m_expenceLines[rowIndex].Account.AccountId].Name;
            return m_expenceLines[rowIndex].Amount.HasValue ?
                   m_expenceLines[rowIndex].Amount.Value.ToString("0.00")
                       : string.Empty;
            
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_expenceLines[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region AddExpenceLine

        internal void AddExpenceLine(CheckExpenceLine checkExpenceLine)
        {            
            m_expenceLines.Insert(0, checkExpenceLine);
            UpdateTable();
        }

        #endregion

        #region DeleteExpenceLine

        internal void DeleteExpenceLine(CheckExpenceLine checkExpenceLine)
        {
            m_expenceLines.Remove(checkExpenceLine);
            UpdateTable();
        }

        #endregion

        #region UpdateTable

        public void UpdateTable()
        {
            UpdateTotalExpences();
            if (Change != null)
                Change.Invoke();            
        }

        #endregion

        #region UpdateTotalExpences

        private void UpdateTotalExpences()
        {
            m_totalExpences = 0;
            foreach (CheckExpenceLine expenceLine in m_expenceLines)
                m_totalExpences += expenceLine.Amount ?? decimal.Zero;
            
            if (TotalExpencesChanged != null)
                TotalExpencesChanged.Invoke(m_totalExpences);
        }

        #endregion

        #region IsContainsAPLine

        public bool IsContainsAPLine()
        {
            foreach (CheckExpenceLine line in m_expenceLines)
                if (Accounts[line.Account.AccountId].AccountType
                    == AccountType.AccountsPayable)
                    return true;
                            
            return false;
        }

        #endregion

        #region Save

        public void Save(Check check, bool IsUpdate)
        {
            if (IsUpdate)
            {
                CheckExpenceLine.Delete(check.CheckId);                                
                Check.Update(check);
            } else
            {
                Counter.Assign(check);
                check.EntityState = EntityState.Created;
                Check.Insert(check);                
            }                            

            foreach (CheckExpenceLine expenceLine in m_expenceLines)
            {
                Counter.Assign(expenceLine);
                expenceLine.Check = check;
                CheckExpenceLine.Insert(expenceLine);
            }
        }

        #endregion
    }
}
