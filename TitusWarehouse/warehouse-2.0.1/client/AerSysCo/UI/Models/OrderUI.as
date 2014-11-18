package AerSysCo.UI.Models
{
	import AerSysCo.Server.Order;
	import AerSysCo.Server.OrderDetail;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class OrderUI
	{
	    public var orderId:int;
	    public var shippingTypeId:int;
	    public var orderStatusId:int;
	    public var orderStatusStr:String;
	    public var warehouseId:int;
	    public var customerId:int;
	    public var brandId:int;
	    public var PONumber:String;
	    public var orderDate:Date;
	    public var shippingDate:Date;
	    public var MACPACOrderNumber:String;
	    public var releaseNumber:String;
	    public var trackingNumber:String;
	    public var repAccountNo:String;
	    public var total:Number;
	    public var shippingTotal:Number;
	    public var grandTotal:Number;
	    public var jobsiteContactPh:String;
	    public var phone:String;
	    public var fax:String;
	    public var email:String;
	    public var salesPerson:String;
	    public var markOrder:String;
	    public var deliveryRequest:String;
	    public var MACPACXML:String;
	    public var MACPACFileName:String;
	    public var shopingCartShipmentId:int;
	    public var soldName:String;
	    public var soldAddress1:String;
	    public var soldAddress2:String;
	    public var soldCity:String;
	    public var soldState:String;
	    public var soldZip:String;
	    public var soldCountry:String;
	    public var shipName:String;
	    public var shipAddress1:String;
	    public var shipAddress2:String;
	    public var shipCity:String;
	    public var shipState:String;
	    public var shipZip:String;
	    public var shipCountry:String;
	    public var details:ArrayCollection = new ArrayCollection();
	    public var warehouse:WarehouseUI = new WarehouseUI();
	    public var shippingType:ShippingTypeUI = new ShippingTypeUI();
	    public var customer:CustomerUI = new CustomerUI();
	    public var createdByUser:String;
	    public var lastUpdateDate:Date;
	    public var dateCreated:Date;
	    
	    public var isLoading:Boolean = false;
	    
	    public function get orderDateString():String 
	    {
	    	return orderDate.toLocaleDateString();
	    }
	    
	    public function get orderTimeString():String 
	    {
	    	return orderDate.toLocaleTimeString();
	    }
	    
	    public function get totalString():String 
	    {
	    	return "$" + total.toFixed(2);
	    }
	    
	    public function get shippingTotalString():String 
	    {
	    	return shippingTotal.toFixed(2);
	    }
	    
	    public function get grandTotalString():String 
	    {
	    	return "$" + grandTotal.toFixed(2);
	    }
	    
	    public function populateFromOrder(value:Order):void 
	    {
		    this.orderId = value.orderId;
		    this.shippingTypeId = value.shippingTypeId;
		    this.orderStatusId = value.orderStatusId;
		    this.orderStatusStr = value.orderStatusStr;
		    this.warehouseId = value.warehouseId;
		    this.customerId = value.customerId;
		    this.brandId = value.brandId;
		    this.PONumber = value.PONumber;
		    this.orderDate = new Date(Date.parse(value.orderDateStr));
		    this.shippingDate = new Date(Date.parse(value.shippingDateStr));
		    this.MACPACOrderNumber = value.MACPACOrderNumber;
		    this.releaseNumber = value.releaseNumber;
		    this.trackingNumber = value.trackingNumber;
		    this.repAccountNo = value.repAccountNo;
		    this.total = value.total;
		    this.shippingTotal = value.shippingTotal;
		    this.grandTotal = value.grandTotal;
		    this.jobsiteContactPh = value.jobsiteContactPh;
		    this.phone = value.phone;
		    this.fax = value.fax;
		    this.email = value.email;
		    this.salesPerson = value.salesPerson;
		    this.markOrder = value.markOrder;
		    this.deliveryRequest = value.deliveryRequest;
		    this.MACPACXML = value.MACPACXML;
		    this.MACPACFileName = value.MACPACFileName;
		    this.shopingCartShipmentId = value.shopingCartShipmentId;
		    this.soldName = value.soldName;
		    this.soldAddress1 = value.soldAddress1;
		    this.soldAddress2 = value.soldAddress2;
		    this.soldCity = value.soldCity;
		    this.soldState = value.soldState;
		    this.soldZip = value.soldZip;
		    this.soldCountry = value.soldCountry;
		    this.shipName = value.shipName;
		    this.shipAddress1 = value.shipAddress1;
		    this.shipAddress2 = value.shipAddress2;
		    this.shipCity = value.shipCity;
		    this.shipState = value.shipState;
		    this.shipZip = value.shipZip;
		    this.shipCountry = value.shipCountry;
		
		    this.createdByUser = value.createdByUser;
		    this.lastUpdateDate = value.lastUpdateDate;
		    this.dateCreated = value.dateCreated;
		    
		    if (value.details) 
		    {
		    	this.details.removeAll();
		    	for each (var od:OrderDetail in value.details.toArray()) 
		    	{
		    		var d:OrderDetailUI = new OrderDetailUI();
		    		d.populateFromOrderDetail(od);
		    		this.details.addItem(d);
		    	}
		    }
		    
		    if (value.warehouse) 
		    {
		    	this.warehouse.populateFromWarehouse(value.warehouse);
		    }
		    
		    if (value.shippingType) 
		    {
		    	this.shippingType.populateFromShippingType(value.shippingType);
		    }
		    
		    if (value.customer) 
		    {
		    	this.customer.populateFromCustomer(value.customer);
		    }
		    
	    }
	}
}