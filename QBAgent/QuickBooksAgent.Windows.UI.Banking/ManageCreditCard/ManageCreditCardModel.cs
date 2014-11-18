using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.ManageCreditCard
{
    public class ManageCreditCardModel : ITableModel, IModel
    {
        #region Fields

        private List<CreditCard> m_cards;

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
            m_cards = CreditCard.FindBy(m_currentAccount);
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
                return "Ref #";
            return "Amount";
        }

        public Type GetColumnClass(int columnIndex)
        {
            return typeof(string);
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return GetEntityStateFirstLetter(m_cards[rowIndex].EntityState);
            else if (columnIndex == 1)
                return m_cards[rowIndex].RefNumber ?? string.Empty;

            return m_cards[rowIndex].Amount == null
                       ? string.Empty : m_cards[rowIndex].Amount.Value.ToString("0.00");
        }

        public int GetRowCount()
        {
            return m_cards.Count;
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
            return m_cards[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region Delete

        public void Delete(CreditCard card)
        {
            Database.Begin();

            try
            {
                CreditCardExpenceLine.Delete(card.CreditCardId);
                CreditCard.Delete(card);
                Database.Commit();
                m_cards.Remove(card);

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

        public void AddInList(CreditCard card)
        {
            m_cards.Insert(0, card);
            if (Change != null)
                Change.Invoke();
        }

        #endregion

        #region ChangeInList

        public void ChangeInList(CreditCard card)
        {
            for (int i = 0; i < m_cards.Count; i++)
            {
                if (m_cards[i].CreditCardId == card.CreditCardId)
                {
                    m_cards[i] = card;

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
