using System;
using System.Collections.Generic;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.CreditCardCharges
{
    public class CreditCardModel : IModel, ITableModel
    {
        internal delegate void TotalExpencesChangedHandler(decimal totalExpences);
        internal event TotalExpencesChangedHandler TotalExpencesChanged;

        #region Feilds

        #region CreditCards

        private List<Account> m_creditCards;
        public List<Account> CreditCards
        {
            get { return m_creditCards; }
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

        private List<CreditCardExpenceLine> m_expenceLines;

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
            m_creditCards = Account.FindBy(AccountType.CreditCard, EntityState.Synchronized);
            m_vendors = Vendor.FindBy(EntityState.Synchronized);
            m_customers = Customer.FindBy(EntityState.Synchronized);
            m_employees = Employee.FindBy(EntityState.Synchronized);
            m_expenceLines = new List<CreditCardExpenceLine>();

            //Init accounts (all)
            m_accounts = new Dictionary<int, Account>();
            List<Account> accounts = Account.FindBy(EntityState.Synchronized);
            foreach (Account account in accounts)
                m_accounts.Add(account.AccountId, account);
        }

        #endregion

        #region LoadExpenceLines

        public void LoadExpenceLines(CreditCard creditCard)
        {
            m_expenceLines = CreditCardExpenceLine.FindBy(creditCard);
            creditCard.ExpenceLines = m_expenceLines;
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
            return typeof(string);
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

        internal void AddExpenceLine(CreditCardExpenceLine cardExpenceLine)
        {
            m_expenceLines.Insert(0, cardExpenceLine);
            UpdateTable();
        }

        #endregion

        #region DeleteExpenceLine

        internal void DeleteExpenceLine(CreditCardExpenceLine cardExpenceLine)
        {
            m_expenceLines.Remove(cardExpenceLine);
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
            foreach (CreditCardExpenceLine expenceLine in m_expenceLines)
                m_totalExpences += expenceLine.Amount ?? decimal.Zero;

            if (TotalExpencesChanged != null)
                TotalExpencesChanged.Invoke(m_totalExpences);
        }

        #endregion

        #region IsContainsAPLine

        public bool IsContainsAPLine()
        {
            foreach (CreditCardExpenceLine line in m_expenceLines)
                if (Accounts[line.Account.AccountId].AccountType
                    == AccountType.AccountsPayable)
                    return true;

            return false;
        }

        #endregion

        #region Save

        public void Save(CreditCard creditCard, bool IsUpdate)
        {
            if (IsUpdate)
            {
                CreditCardExpenceLine.Delete(creditCard.CreditCardId);
                CreditCard.Update(creditCard);
            }
            else
            {
                Counter.Assign(creditCard);
                creditCard.EntityState = EntityState.Created;
                CreditCard.Insert(creditCard);
            }

            foreach (CreditCardExpenceLine expenceLine in m_expenceLines)
            {
                Counter.Assign(expenceLine);
                expenceLine.CreditCard = creditCard;
                CreditCardExpenceLine.Insert(expenceLine);
            }
        }

        #endregion        
    }
}
