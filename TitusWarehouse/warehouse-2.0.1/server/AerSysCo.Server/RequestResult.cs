using System;
using System.Collections.Generic;
using System.Text;
using AerSysCo.Entity;
using AerSysCo.CustomerCenter;


namespace AerSysCo.Server
{
public class RequestResult {
    public const string SUCCESSS = "SUCCESS";
    public const string ERROR = "ERROR";
    public const string FAIL = "FAIL";
    public const string VERSION = "VERSION";
    public string status = FAIL;
    public string message;
}

public class VersionResult {
    public RequestResult result = new RequestResult();
    public int shoppingCartId = 0;
    public int version = 0;
};

public class SalesRepResult {
    public RequestResult result = new RequestResult();
    public SalesRep salesRep = null;
}

public class CatalogPackageResult {
    public RequestResult result = new RequestResult();
    public CatalogPackage pack = null;
}

public class CatalogItemResult {
    public RequestResult result = new RequestResult();
    public List<CatalogItem> items = new List<CatalogItem>();
}

public class ShoppingCartResult {
    public RequestResult result = new RequestResult();
    public ShoppingCart cart = null;
}

public class CheckInResult {
    public RequestResult result = new RequestResult();
    public string acknowURL = "";
    public ShoppingCart cart = null;
    public List<string> errors = new List<string>();
}

public class ShoppingCartDetailResult  {
    public RequestResult result = new RequestResult();
    public ShoppingCartDetail detail= null;
    public int version = 0;
}

public class ShoppingCartDetailListResult {
    public RequestResult result = new RequestResult();
    public List<ShoppingCartDetail> details = new List<ShoppingCartDetail>();
    public int version = 0;
}

public class ShippingAddressResult {
    public RequestResult result = new RequestResult();
    public ShippingAddress address = null;
}

public class ShippingAddressListResult {
    public RequestResult result = new RequestResult();
    public List<ShippingAddress> addresses = new List<ShippingAddress>();
}

public class ShipmentShippingOptionsResult { 
    public RequestResult result = new RequestResult();
    public List<ShipmentShippingOptions> options = new List<ShipmentShippingOptions>();
}

public class ShoppingCartShipmentResult {
    public RequestResult result = new RequestResult();
    public ShoppingCartShipment shipment = null;
    public int version = 0;
}

public class ShoppingCartShipmentListResult {
    public RequestResult result = new RequestResult();
    public List<ShoppingCartShipment> shipments = new List<ShoppingCartShipment>();
    public int version = 0;
}

public class LoginResult {
    public RequestResult result = new RequestResult();
    public ASCUser user = null;
}

public class CustomerListResult {
    public RequestResult result = new RequestResult();
    public List<Customer> customers = new List<Customer>();
}

public class CustomerResult {
    public RequestResult result = new RequestResult();
    public Customer customer = null;
}

public class OrderListResult {
    public RequestResult result = new RequestResult();
    public List<Order> orders = new List<Order>();
}

public class OrderResult {
    public RequestResult result = new RequestResult();
    public Order order = null;
}

public class PONumberCheckResult {
    public RequestResult result = new RequestResult();
    public bool isUnique = false;
    public List<Order> orders = new List<Order>();
}

public class ZipCheckResult {
    public RequestResult result = new RequestResult();
    public string state = null;
    public List<string> cities = new List<string>();
}

public class URLResult {
    public RequestResult result = new RequestResult();
    public string url = null;
}


}
