using System;
using System.Collections.Generic;
using System.Text;

namespace AerSysCo.Entity
{
public class MACPACInventory
{
   public int macPac_Inventory_id;
   public string plant;
   public string brand;
   public string part;
   public string partDesc;
   public string altDesc;
   public string model;
   public string configuration;
   public string containerCode;
   public decimal height;
   public decimal depth;
   public decimal width;
   public decimal containerWeight;
   public decimal partweight;
   public decimal qtypercontainer;
   public decimal basePrice;
   public int onHand;
   public int allocated;
   public string partStatus;
   public DateTime macPacTimeStamp;
   public DateTime importTimeStamp;
   public string processedStatus;
   public string failReason;
}

public class MACPACMultiplier {
   public string brand;
   public string sku;
   public string marketingProgram;
   public string customerid;
   public decimal multiplier;
}

}
