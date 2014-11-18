using System;

namespace MobileTech.Domain
{
    public partial class InventoryTransactionDetail
    {
        #region Constructors
        public InventoryTransactionDetail()
        {

        }

        public InventoryTransactionDetail(InventoryTransaction inventoryTransaction,
            RouteInventory routeInventory,
            InventoryTransactionDetailXRef type)
        {
            m_routeNumber = routeInventory.RouteNumber;
            m_locationId = routeInventory.LocationId;
            m_sessionId = routeInventory.SessionId;
            m_storageTypeId = routeInventory.StorageTypeId;
            m_inventoryPeriodId = routeInventory.InventoryPeriodId;
            m_itemNumber = routeInventory.ItemNumber;
            m_dateCreated = DateTime.Now;

            m_businessTransactionId = inventoryTransaction.BusinessTransactionId;

            m_inventoryTransactionDetailTypeId = type.InventoryTransactionDetailTypeId;
            m_inventoryTransactionTypeId = type.InventoryTransactionTypeId;
        }

        #endregion

        #region Type


        public InventoryTransactionTypeEnum InventoryTransactionType
        {
            get
            {
                return (InventoryTransactionTypeEnum)m_inventoryTransactionTypeId;
            }
            set
            {
                m_inventoryTransactionTypeId = (int)value;
            }
        }


        public InventoryTransactionDetailTypeEnum InventoryTransactionDetailType
        {
            get
            {
                return (InventoryTransactionDetailTypeEnum)m_inventoryTransactionDetailTypeId;
            }
            set
            {
                m_inventoryTransactionDetailTypeId = (int)value;
            }
        }

        #endregion
    }
}
