using System;
using System.Collections.Generic;
using System.Text;

namespace AerSysCo.Entity
{
public class ShippingOption {
    public int shippingTypeId = 0;
    public ShippingType shippigType = null;
    public bool isApplicable = false;
    public decimal cost = 0m;
}

public class ShipmentShippingOptions {
    public int shoppingCartShipmentId = 0;
    public decimal liftGatePrice = 0;
    public List<ShippingOption> options = new List<ShippingOption>();
}


}
