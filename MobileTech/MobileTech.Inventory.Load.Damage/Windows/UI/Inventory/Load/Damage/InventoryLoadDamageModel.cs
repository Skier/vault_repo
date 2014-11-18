using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Load.Damage
{
    internal class InventoryLoadDamageModel:InventoryLoadItemModel
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
            return new DamageData(routeInventory);
        }

        #endregion

        #region UpdateAdjustments

        protected override void UpdateAdjustments(List<RouteInventory> routeInventoryList)
        {
            RouteInventory.UpdateDmgLoadAdjustment(routeInventoryList);
        }

        #endregion

        #region Helper class

        class DamageData : Data
        {

            public DamageData(RouteInventory routeInventory)
            {
                RouteInventory = routeInventory;
                CurrentLoad = routeInventory.DmgLoadQty;
            }

            public override int Total
            {
                get
                {
                    return RouteInventory.DmgLoadAdjustmentQty 
                        + Adjustments 
                        + CurrentLoad 
                        + RouteInventory.DmgStartQty 
                        - RouteInventory.DmgSaleQty;
                }
            }

            public override int TruckQty
            {
                get { return RouteInventory.TruckDmgQty; }
            }

            public override void ApplyAdjustment()
            {
                RouteInventory.DmgLoadAdjustmentQty += Adjustments;
            }
        }

        #endregion
    }
}
