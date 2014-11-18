using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Unload.Equipment
{
    internal class InventoryUnloadEquipmentModel:InventoryUnloadItemModel
    {
        protected override void Init(RouteInventory routeInventory)
        {
            int truckQty = routeInventory.TruckQty > 0 ?
                routeInventory.TruckQty : 0;

            if (Mode == UnloadEditMode.Truck)
                routeInventory.EndQty = truckQty;
            else
                routeInventory.UnloadQty = truckQty;
        }

        public override bool IsModeAllow(UnloadEditMode mode)
        {
            return mode != UnloadEditMode.Damage;
        }

        public override MobileTech.Domain.ItemTypeEnum GetItemType()
        {
            return MobileTech.Domain.ItemTypeEnum.Equipment;
        }

        #region ITable

        #region GetValueAt

        public override object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_data[m_storageType][rowIndex].Item.Name;
            else
            {
                if (m_mode == UnloadEditMode.Unload)
                    return m_data[m_storageType][rowIndex].UnloadQty;

                return m_data[m_storageType][rowIndex].EndQty;
            }
        }

        #endregion

        #region SetValueAt

        public override void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            if (m_mode == UnloadEditMode.Unload)
                m_data[m_storageType][rowIndex].UnloadQty = (int)aValue;
            else
                m_data[m_storageType][rowIndex].EndQty = (int)aValue;
        }

        #endregion

        #endregion

        protected override void SetNewItemQuantity(RouteInventory routeInventory, int quantity)
        {
            if (m_mode == UnloadEditMode.Truck)
                routeInventory.EndQty = quantity;
            else
                routeInventory.UnloadQty = quantity;
        }
    }
}
