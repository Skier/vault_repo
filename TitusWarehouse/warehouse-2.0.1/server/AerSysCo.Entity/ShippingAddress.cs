using System;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Entity
{

public class ShippingAddress : Address {
    public int addressId = 0;
    public int customerId = 0;
    public string createdByUser = "";
    public DateTime lastUpdateDate = DateTime.Now;
    public DateTime dateCreated = DateTime.Now; 
};

};