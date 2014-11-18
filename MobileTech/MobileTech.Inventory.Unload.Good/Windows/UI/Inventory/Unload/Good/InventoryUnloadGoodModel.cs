using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;
using MobileTech.Windows.UI.SelectItem;

namespace MobileTech.Windows.UI.Inventory.Unload.Good
{
    internal class InventoryUnloadGoodModel : InventoryUnloadItemModel
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

        public override ItemTypeEnum GetItemType()
        {
            return ItemTypeEnum.Product;
        }

        public override bool IsModeAllow(UnloadEditMode mode)
        {
            return true;
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
                else if(m_mode == UnloadEditMode.Truck)
                    return m_data[m_storageType][rowIndex].EndQty;

                return m_data[m_storageType][rowIndex].RouteDmgQty;
            }
        }

        #endregion

        #region SetValueAt

        public override void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            if (m_mode == UnloadEditMode.Unload)
                m_data[m_storageType][rowIndex].UnloadQty = (int)aValue;
            else if(m_mode == UnloadEditMode.Truck)
                m_data[m_storageType][rowIndex].EndQty = (int)aValue;
            else
                m_data[m_storageType][rowIndex].RouteDmgQty = (int)aValue;
        }

        #endregion

        #endregion

        protected override void SetNewItemQuantity(RouteInventory routeInventory, int quantity)
        {
            if (m_mode == UnloadEditMode.Truck)
                routeInventory.EndQty = quantity;
            else if(m_mode == UnloadEditMode.Unload)
                routeInventory.UnloadQty = quantity;
            else
                routeInventory.RouteDmgQty = quantity;

        }
    }
}
