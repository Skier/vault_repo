using System;

namespace AerSysCo.Entity {

public class ShoppingCartDetail : Traceable {
   public int shoppingCartDetailId = 0;
   public int shoppingCartId = 0;
   public int shoppingCartShipmentId = 0;
   public int modelItemId = 0;
   public int lineItemNumber = 0;
   public int qtyOrdered = 0;
   public int qtyNeeded = 0;
   public Decimal price = 0m;
   public string sku = "";
   public ModelItem modelItem = null;
   public Inventory inventory = null;
   public string modelName = null;
};

};