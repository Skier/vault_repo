using System;
using System.Collections.Generic;
using System.Text;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Entity
{
public class ShoppingCart : Traceable {
   public int shoppingCartId = 0;
   public bool isActive = true;
   public int customerId = 0;
   public DateTime orderDate = DateTime.MinValue;
   public string repAccountNo = null;
   public int shippingAddressId = 0;
   public int brandId = 0;
   public string ipAddress = "127.0.0.1";
   public decimal total = 0;
   public decimal shippingTotalAllWarehouses = 0;
   public decimal grandTotal = 0;
   public string phone = null;
   public string fax = null;
   public string email = null;
   public string salesPerson = null;
   public string jobsiteContactPh = null;
   public string markOrder = null;
   public string deliveryRequest = null;
   public string acknFileName = null;
   public int version = 0;

   public List<ShoppingCartShipment> shipments = new List<ShoppingCartShipment>();
   public ShippingAddress shippingAddress;
   public Customer customer;
}
}
