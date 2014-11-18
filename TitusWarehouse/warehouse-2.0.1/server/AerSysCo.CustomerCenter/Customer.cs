using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data;

namespace AerSysCo.CustomerCenter
{
public class Customer
{
    public int customerId;
    public int defaultWarehouseId = 0;
    public string MACPACCustonerNumber;
    public string salesRepCompanyName;
    public bool creditStatus = false;
    public Decimal maxOrderTotal = Decimal.MaxValue;
    public string createdByUser = "";
    public DateTime lastUpdateDate = DateTime.Now;
    public DateTime dateCreated = DateTime.Now; 
    public int brandId;
    public string brandName;
    public Decimal dayBalance;

    public Address address;
    public string phoneNumber = "";
    public string fax = "";
    public string email = "";
}
}
