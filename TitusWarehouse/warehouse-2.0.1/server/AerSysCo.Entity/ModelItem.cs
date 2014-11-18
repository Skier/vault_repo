using System;

namespace AerSysCo.Entity {

public class ModelItem : Traceable {
   public int modelItemId = 0;
   public int modelId = 0;
   public int itemId = 0;
   public string configuration = "";
   public Decimal price = 0m;
   public bool isActive = false;
   public string imageURL = "";
   public string xmlBullerDescription = "";
   public int MACPACInventoryId;
   public Item item = null;
}; 

};