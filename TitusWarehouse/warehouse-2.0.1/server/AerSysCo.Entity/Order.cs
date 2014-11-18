using System;
using System.Collections.Generic;
using AerSysCo.CustomerCenter;

namespace AerSysCo.Entity {

public class Order : Traceable {
   public int orderId;
   public int shippingTypeId;
   public int orderStatusId;
   public int warehouseId;
   public int customerId;
   public int brandId;
   public string PONumber;
   public DateTime orderDate;
   public DateTime shippingDate;

   public string orderStatusStr;
   public string orderDateStr;
   public string shippingDateStr;

   public string MACPACOrderNumber;
   public string releaseNumber;
   public string trackingNumber;
   public string repAccountNo;
   public decimal total;
   public decimal shippingTotal;
   public decimal grandTotal;
   public string jobsiteContactPh;
   public string phone;
   public string fax;
   public string email;
   public string salesPerson;
   public string markOrder;
   public string deliveryRequest;
   public string MACPACXML;
   public string MACPACFileName;
   public int shopingCartShipmentId;
   public string soldName;
   public string soldAddress1;
   public string soldAddress2;
   public string soldCity;
   public string soldState;
   public string soldZip;
   public string soldCountry;
   public string shipName;
   public string shipAddress1;
   public string shipAddress2;
   public string shipCity;
   public string shipState;
   public string shipZip;
   public string shipCountry;
   public string marketingProgram;

   public List<OrderDetail> details;
   public Warehouse warehouse;
   public ShippingType shippingType;
   public Customer customer;
};

};