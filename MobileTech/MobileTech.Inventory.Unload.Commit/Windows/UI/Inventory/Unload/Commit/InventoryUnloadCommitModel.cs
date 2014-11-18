using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;
using MobileTech.Data;

namespace MobileTech.Windows.UI.Inventory.Unload.Commit
{
    internal class InventoryUnloadCommitModel:InventoryUnloadModel
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
                    StorageTypeEnum.Store,ItemTypeEnum.Product);

                List<InventoryTransactionDetail> transactions = new List<InventoryTransactionDetail>();

                foreach (RouteInventory routeInventory in routeInventoryList)
                {

                        InventoryTransactionDetail unloadReturnStock
                            = new InventoryTransactionDetail(
                                    m_transaction,
                                    routeInventory,
                                    new InventoryTransactionDetailXRef(InventoryTransactionTypeEnum.Unload,
                                    InventoryTransactionDetailTypeEnum.UnloadReturnStock));


                        InventoryTransactionDetail unloadEndInventory
                            = new InventoryTransactionDetail(
                                    m_transaction,
                                    routeInventory,
                                    new InventoryTransactionDetailXRef(InventoryTransactionTypeEnum.Unload,
                                    InventoryTransactionDetailTypeEnum.UnloadEndInventory));


                        InventoryTransactionDetail unloadTruckDamaged
                            = new InventoryTransactionDetail(
                                    m_transaction,
                                    routeInventory,
                                    new InventoryTransactionDetailXRef(InventoryTransactionTypeEnum.Unload,
                                    InventoryTransactionDetailTypeEnum.UnloadTruckDamaged));


                        unloadReturnStock.Quantity = routeInventory.UnloadQty;
                        unloadEndInventory.Quantity = routeInventory.EndQty;
                        unloadTruckDamaged.Quantity = routeInventory.RouteDmgQty;

                        transactions.Add(unloadReturnStock);
                        transactions.Add(unloadEndInventory);
                        transactions.Add(unloadTruckDamaged);

                }



                InventoryTransactionDetail.Insert(transactions);

                RouteInventory.CreateNextPeriod();



                m_transaction.BusinessTransaction.Status = BusinessTransactionStatusEnum.Commited;
                m_transaction.BusinessTransaction.DateCommited = DateTime.Now;

                BusinessTransaction.Update(m_transaction.BusinessTransaction);

                if (Route.Current.InventorySync)
                {
                    Route.ChangeStatus(RouteStatusEnum.UNLOAD_DONE);
                }

                Database.Commit();
            }
            catch (Exception ex)
            {
                Database.Rollback();

                throw new MobileTechException("Enable to commit transaction",ex);
            }
        }

        #endregion
    }
}
