using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.ManageCheck
{
    public class ManageCheckModel : ITableModel, IModel
    {
        #region Fields

        private List<Check> m_checks;

        #region CurrentAccount

        private Account m_currentAccount;
        public Account CurrentAccount
        {
            set { m_currentAccount = value; }
            get { return m_currentAccount; }
        }

        #endregion

        #endregion
                        
        #region Init

        public void Init()
        {
            m_checks = Check.FindBy(m_currentAccount);
        }

        #endregion

        #region ITableModel

        public int GetColumnCount()
        {
            return 3;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return string.Empty;
            else if (columnIndex == 1)
                return "Check #";
            return "Amount";
        }

        public Type GetColumnClass(int columnIndex)
        {
            return typeof (string);
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return GetEntityStateFirstLetter(m_checks[rowIndex].EntityState);
            else if (columnIndex == 1)
                return m_checks[rowIndex].RefNumber ?? string.Empty;

            return m_checks[rowIndex].Amount == null
                       ? string.Empty : m_checks[rowIndex].Amount.Value.ToString("0.00");
        }

        public int GetRowCount()
        {
            return m_checks.Count;
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_checks[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region Delete

        public void Delete(Check check)
        {                        
            Database.Begin();

            try
            {
                CheckExpenceLine.Delete(check.CheckId);
                Check.Delete(check);
                Database.Commit();
                m_checks.Remove(check);

                if (Change != null)
                    Change.Invoke();                            
            }
            catch (Exception ex)
            {
                Database.Rollback();
                throw ex;
            }                           
        }

        #endregion

        #region AddInList

        public void AddInList(Check check)
        {
            m_checks.Insert(0, check);
            if (Change != null)
                Change.Invoke();
        }

        #endregion

        #region ChangeInList

        public void ChangeInList(Check check)
        {
            for (int i = 0; i < m_checks.Count; i++)
            {
                if (m_checks[i].CheckId == check.CheckId)
                {
                    m_checks[i] = check;
                    
                    if (Change != null)
                        Change.Invoke();
                    return;
                }                
            }
            
        }

        #endregion

        #region GetEntityStateFirstLetter

        private string GetEntityStateFirstLetter(EntityState entityState)
        {
            if (entityState == EntityState.Created)
                return "C";
            else if (entityState == EntityState.Synchronized)
                return "S";
            else if (entityState == EntityState.Modified)
                return "M";
            else if (entityState == EntityState.Deleted)
                return "D";
            return string.Empty;
        }

        #endregion
    }
}
