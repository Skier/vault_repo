using System;
using System.Collections.Generic;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Entity {

public class FullOrder : Order {
    public List<OrderShipment> shipments = null;
};

}; 
