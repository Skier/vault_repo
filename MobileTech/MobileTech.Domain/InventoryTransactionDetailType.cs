using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Domain
{
    public enum InventoryTransactionDetailTypeEnum
    {
        Load = 1,
        ReloadOffLoadProduct = 2,
        ReloadAddOnProducts = 3,
        ReloadDamagedReturns = 4,
        UnloadEndInventory = 5,
        UnloadReturnStock = 6,
        UnloadDamagedReturns = 7,
        LoadRequest = 8,
        UnloadVariance = 9,
        UnloadDamagedReturnsVariance = 10,
        UnloadTruckDamaged = 11,
        UnloadEndDamagedReturns = 12,
        ReloadTruckDamaged = 13,
        RtnstkReturnStock = 14,
        RtnstkTruckDamaged = 15,
        RtnstkDamagedReturns = 16,
        LoadAdjustments = 17,
        LoadDamageAdjustments = 18
    }


    public partial class InventoryTransactionDetailType
    {
        public InventoryTransactionDetailType() { }
    }
}
