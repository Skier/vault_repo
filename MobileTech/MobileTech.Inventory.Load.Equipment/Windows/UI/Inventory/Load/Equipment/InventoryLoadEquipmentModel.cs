using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Load.Equipment
{
    internal class InventoryLoadEquipmentModel:InventoryLoadItemModel
    {
        #region UpdateAdjustments

        protected override void UpdateAdjustments(List<RouteInventory> routeInventoryList)
        {
            RouteInventory.UpdateLoadAdjustment(routeInventoryList);
        }

        #endregion

        #region CreateDataInstance

        protected override Data CreateDataInstance(MobileTech.Domain.RouteInventory routeInventory)
        {
            return new EquipmentData(routeInventory);
        }

        #endregion

        #region GetItemType

        public override ItemTypeEnum GetItemType()
        {
            return ItemTypeEnum.Equipment;
        }

        #endregion

        #region Helper class

        class EquipmentData : Data
        {
            public EquipmentData(RouteInventory routeInventory)
            {
                RouteInventory = routeInventory;
                CurrentLoad = routeInventory.LoadQty;
            }

            public override int Total
            {
                get
                {
                    return RouteInventory.LoadAdjustmentQty + Adjustments + CurrentLoad + RouteInventory.StartQty - RouteInventory.SaleQty;
                }
            }

            public override int TruckQty
            {
                get { return RouteInventory.TruckQty; }
            }

            public override void ApplyAdjustment()
            {
                RouteInventory.LoadAdjustmentQty += Adjustments;
            }
        }

        #endregion
    }
}
