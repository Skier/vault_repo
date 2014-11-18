using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;
using MobileTech.Data;
using MobileTech.ServiceLayer;
using MobileTech.Windows.UI.SelectItem;


namespace MobileTech.Windows.UI.Inventory.Load
{

    public abstract class InventoryLoadModel : IModel
    {
        #region Fields

        protected InventoryTransaction m_transaction;

        #endregion

        #region Rollback
        public void Rollback()
        {
            Database.Rollback();
        }
        #endregion

        #region Init

        public virtual void Init()
        {
            List<InventoryTransaction> transactions = InventoryTransaction.FindUncommited(
                InventoryTransactionTypeEnum.Load);

            if (transactions.Count > 1)
                throw new MobileTechException("Transaction load error");
            else if (transactions.Count == 1)
                m_transaction = transactions[0];


            if (!Database.UnderTransaction)
                Database.Begin();
        }

        #endregion

        #region IsTransactionExists

        public bool IsTransactionExists
        {
            get
            {
                return m_transaction != null;
            }
        }
        #endregion

        #region AddTransaction

        protected void AddTransaction()
        {

            if (IsTransactionExists)
                throw new MobileTechException("Transaction  already exists");

            m_transaction = InventoryTransaction.Prepare(InventoryTransactionTypeEnum.Load);

            Counter.Assign(m_transaction.BusinessTransaction);

            m_transaction.BusinessTransactionId =
                m_transaction.BusinessTransaction.BusinessTransactionId;

            BusinessTransaction.Insert(m_transaction.BusinessTransaction);

            InventoryTransaction.Insert(m_transaction);
        }

        #endregion

        #region CleanUp

        public virtual void CleanUp()
        {
            m_transaction = null;
        }

        #endregion
    }
}
