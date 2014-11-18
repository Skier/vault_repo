using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Unload.Damage
{
    internal class InventoryUnloadDamageModel:InventoryUnloadItemModel
    {
        protected override void Init(RouteInventory routeInventory)
        {
            int truckQty = routeInventory.TruckDmgQty > 0 ?
                routeInventory.TruckDmgQty : 0;

            if (Mode == UnloadEditMode.Truck)
                routeInventory.DmgEndQty = truckQty;
            else
                routeInventory.DmgUnloadQty = truckQty;
        }

        public override bool IsModeAllow(UnloadEditMode mode)
        {
            return mode != UnloadEditMode.Damage;
        }

        public override MobileTech.Domain.ItemTypeEnum GetItemType()
        {
            return MobileTech.Domain.ItemTypeEnum.Product;
        }

        #region GetValueAt

        public override object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_data[m_storageType][rowIndex].Item.Name;
            else
            {
              if(m_mode == UnloadEditMode.Unload)
                    return m_data[m_storageType][rowIndex].DmgUnloadQty;
                    return m_data[m_storageType][rowIndex].DmgEndQty;
            }
        }

        #endregion

        #region SetValueAt

        public override void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            if (m_mode == UnloadEditMode.Unload)
                m_data[m_storageType][rowIndex].DmgUnloadQty = (int)aValue;
            else
                m_data[m_storageType][rowIndex].DmgEndQty = (int)aValue;
        }

        #endregion

        protected override void SetNewItemQuantity(RouteInventory routeInventory, int quantity)
        {
            if (m_mode == UnloadEditMode.Truck)
                routeInventory.DmgEndQty = quantity;
            else
                routeInventory.DmgUnloadQty = quantity;
        }
    }
}
