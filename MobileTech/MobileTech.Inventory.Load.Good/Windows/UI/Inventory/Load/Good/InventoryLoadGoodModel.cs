using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Load.Good
{
    internal class InventoryLoadGoodModel:InventoryLoadItemModel
    {
        #region GetItemType

        public override ItemTypeEnum GetItemType()
        {
            return ItemTypeEnum.Product;
        }

        #endregion

        #region CreateDataInstance

        protected override Data CreateDataInstance(RouteInventory routeInventory)
        {
            return new GoodData(routeInventory);
        }

        #endregion

        #region UpdateAdjustments

        protected override void UpdateAdjustments(List<RouteInventory> routeInventoryList)
        {
            RouteInventory.UpdateLoadAdjustment(routeInventoryList);
        }

        #endregion

        #region Helper class

        class GoodData : Data
        {

            public GoodData(RouteInventory routeInventory)
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
