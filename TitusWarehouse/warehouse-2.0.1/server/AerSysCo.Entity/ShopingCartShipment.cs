using System;
using System.Collections.Generic;

namespace AerSysCo.Entity {

public class ShoppingCartShipment : Traceable {
    public int shoppingCartShipmentId = 0;
    public int shoppingCartId = 0;
    public int warehouseId = 0;
    public Decimal shippingTotal = 0m;
    public int shippingTypeId = 0;
    public string PoNumber = "";
    public bool needLiftGate = false;
    public decimal liftGatePrice = 0m;

    public List<ShoppingCartDetail> details = new List<ShoppingCartDetail>();
    public Warehouse warehouse = null;
};

};