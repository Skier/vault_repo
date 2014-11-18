using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Data;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Load.Commit
{
    internal class InventoryLoadCommitModel : InventoryLoadModel
    {
        #region AssignDocumentNumber

        public void AssignDocumentNumber()
        {
            BusinessTransaction.AssignDocumentNumber(m_transaction.BusinessTransaction);
        }

        #endregion

        #region Commit

        public void Commit()
        {
            try
            {

                List<RouteInventory> routeInventoryList = RouteInventory.FindBy(
                    StorageTypeEnum.Store, ItemTypeEnum.Product);

                List<InventoryTransactionDetail> transactions = new List<InventoryTransactionDetail>();

                foreach (RouteInventory routeInventory in routeInventoryList)
                {

                    if (routeInventory.LoadAdjustmentQty != 0)
                    {
                        InventoryTransactionDetail loadAdjustmetns
                            = new InventoryTransactionDetail(
                                    m_transaction,
                                    routeInventory,
                                    new InventoryTransactionDetailXRef(InventoryTransactionTypeEnum.Load,
                                    InventoryTransactionDetailTypeEnum.LoadAdjustments));

                        loadAdjustmetns.Quantity = routeInventory.LoadAdjustmentQty;

                        transactions.Add(loadAdjustmetns);

                    }

                    if (routeInventory.DmgLoadAdjustmentQty != 0)
                    {
                        InventoryTransactionDetail loadDamageAdjustmetns
                            = new InventoryTransactionDetail(
                                    m_transaction,
                                    routeInventory,
                                    new InventoryTransactionDetailXRef(InventoryTransactionTypeEnum.Load,
                                    InventoryTransactionDetailTypeEnum.LoadDamageAdjustments));


                        loadDamageAdjustmetns.Quantity = routeInventory.DmgLoadAdjustmentQty;

                        transactions.Add(loadDamageAdjustmetns);
                    }
                }

                routeInventoryList = RouteInventory.FindBy(
                    StorageTypeEnum.Store, ItemTypeEnum.Equipment);

                foreach (RouteInventory routeInventory in routeInventoryList)
                {

                    if (routeInventory.LoadAdjustmentQty != 0)
                    {
                        InventoryTransactionDetail loadAdjustmetns
                            = new InventoryTransactionDetail(
                                    m_transaction,
                                    routeInventory,
                                    new InventoryTransactionDetailXRef(InventoryTransactionTypeEnum.Load,
                                    InventoryTransactionDetailTypeEnum.LoadAdjustments));

                        loadAdjustmetns.Quantity = routeInventory.LoadAdjustmentQty;

                        transactions.Add(loadAdjustmetns);

                    }
                }

                InventoryTransactionDetail.Insert(transactions);

                m_transaction.BusinessTransaction.Status = BusinessTransactionStatusEnum.Commited;
                m_transaction.BusinessTransaction.DateCommited = DateTime.Now;

                BusinessTransaction.Update(m_transaction.BusinessTransaction);

                if (Route.Current.InventorySync)
                {
                    Route.ChangeStatus(RouteStatusEnum.LOAD_DONE);
                }


                Database.Commit();
            }
            catch (Exception e)
            {
                Database.Rollback();

                throw e;
            }
        }

        #endregion
    }
}
