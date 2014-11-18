using System;
using System.Collections.Generic;

namespace AerSysCo.Entity
{

public class Inventory : Traceable {
   public int inventoryId;
   public int warehouseId;
   public int itemId;
   public int qty;
   public int MacPac_Inventory_id;
   public int qtyAllocated;
};

};
