using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Data;
using MobileTech.Domain;
using MobileTech.ServiceLayer;

namespace MobileTech.Windows.UI.Inventory.Unload
{
    /*public enum UnloadTransactionType
    {
        Good,
        Damage,
        Equipment
    }*/

    public abstract class InventoryUnloadModel:IModel
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
                InventoryTransactionTypeEnum.Unload);

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

            m_transaction = InventoryTransaction.Prepare(InventoryTransactionTypeEnum.Unload);

            Counter.Assign(m_transaction.BusinessTransaction);

            m_transaction.BusinessTransactionId =
                m_transaction.BusinessTransaction.BusinessTransactionId;

            // Null date load will be raise error
            m_transaction.BusinessTransaction.DateCommited = DateTime.Now;

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
