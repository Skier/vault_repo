//*******************************************************************
//  <auto-generated>
//       FleXtense : www.flextense.net
//       Version:1.0 Beta Release
//
//
//       Changes to this file may cause incorrect behavior and will be lost if
//       the code is regenerated.
//  <auto-generated>
//*******************************************************************

 package AerSysCo.Server
{

  public class Order
  {
    private var orderIdField:int = 0;
    private var shippingTypeIdField:int = 0;
    private var orderStatusIdField:int = 0;
    private var warehouseIdField:int = 0;
    private var customerIdField:int = 0;
    private var brandIdField:int = 0;
    private var pONumberField:String="";
    private var orderDateField:Date;
    private var shippingDateField:Date;
    private var orderStatusStrField:String="";
    private var orderDateStrField:String="";
    private var shippingDateStrField:String="";
    private var mACPACOrderNumberField:String="";
    private var releaseNumberField:String="";
    private var trackingNumberField:String="";
    private var repAccountNoField:String="";
    private var totalField:Number = 0;
    private var shippingTotalField:Number = 0;
    private var grandTotalField:Number = 0;
    private var jobsiteContactPhField:String="";
    private var phoneField:String="";
    private var faxField:String="";
    private var emailField:String="";
    private var salesPersonField:String="";
    private var markOrderField:String="";
    private var deliveryRequestField:String="";
    private var mACPACXMLField:String="";
    private var mACPACFileNameField:String="";
    private var shopingCartShipmentIdField:int = 0;
    private var soldNameField:String="";
    private var soldAddress1Field:String="";
    private var soldAddress2Field:String="";
    private var soldCityField:String="";
    private var soldStateField:String="";
    private var soldZipField:String="";
    private var soldCountryField:String="";
    private var shipNameField:String="";
    private var shipAddress1Field:String="";
    private var shipAddress2Field:String="";
    private var shipCityField:String="";
    private var shipStateField:String="";
    private var shipZipField:String="";
    private var shipCountryField:String="";
    private var marketingProgramField:String="";
    private var detailsField:AerSysCo.Server.OrderDetailCollection;
    private var warehouseField:AerSysCo.Server.Warehouse;
    private var shippingTypeField:AerSysCo.Server.ShippingType;
    private var customerField:AerSysCo.Server.Customer;
    private var createdByUserField:String="";
    private var lastUpdateDateField:Date;
    private var dateCreatedField:Date;

    public function Order()
    {
      this.detailsField = new AerSysCo.Server.OrderDetailCollection();
      this.warehouseField = new AerSysCo.Server.Warehouse();
      this.shippingTypeField = new AerSysCo.Server.ShippingType();
      this.customerField = new AerSysCo.Server.Customer();

    }

    public function get orderId():int
    {
        return this.orderIdField;
    }
    public function set orderId(value:int):void
    {
       this.orderIdField = value;
    }
    public function get shippingTypeId():int
    {
        return this.shippingTypeIdField;
    }
    public function set shippingTypeId(value:int):void
    {
       this.shippingTypeIdField = value;
    }
    public function get orderStatusId():int
    {
        return this.orderStatusIdField;
    }
    public function set orderStatusId(value:int):void
    {
       this.orderStatusIdField = value;
    }
    public function get warehouseId():int
    {
        return this.warehouseIdField;
    }
    public function set warehouseId(value:int):void
    {
       this.warehouseIdField = value;
    }
    public function get customerId():int
    {
        return this.customerIdField;
    }
    public function set customerId(value:int):void
    {
       this.customerIdField = value;
    }
    public function get brandId():int
    {
        return this.brandIdField;
    }
    public function set brandId(value:int):void
    {
       this.brandIdField = value;
    }
    public function get PONumber():String
    {
        return this.pONumberField;
    }
    public function set PONumber(value:String):void
    {
       this.pONumberField = value;
    }
    public function get orderDate():Date
    {
        return this.orderDateField;
    }
    public function set orderDate(value:Date):void
    {
       this.orderDateField = value;
    }
    public function get shippingDate():Date
    {
        return this.shippingDateField;
    }
    public function set shippingDate(value:Date):void
    {
       this.shippingDateField = value;
    }
    public function get orderStatusStr():String
    {
        return this.orderStatusStrField;
    }
    public function set orderStatusStr(value:String):void
    {
       this.orderStatusStrField = value;
    }
    public function get orderDateStr():String
    {
        return this.orderDateStrField;
    }
    public function set orderDateStr(value:String):void
    {
       this.orderDateStrField = value;
    }
    public function get shippingDateStr():String
    {
        return this.shippingDateStrField;
    }
    public function set shippingDateStr(value:String):void
    {
       this.shippingDateStrField = value;
    }
    public function get MACPACOrderNumber():String
    {
        return this.mACPACOrderNumberField;
    }
    public function set MACPACOrderNumber(value:String):void
    {
       this.mACPACOrderNumberField = value;
    }
    public function get releaseNumber():String
    {
        return this.releaseNumberField;
    }
    public function set releaseNumber(value:String):void
    {
       this.releaseNumberField = value;
    }
    public function get trackingNumber():String
    {
        return this.trackingNumberField;
    }
    public function set trackingNumber(value:String):void
    {
       this.trackingNumberField = value;
    }
    public function get repAccountNo():String
    {
        return this.repAccountNoField;
    }
    public function set repAccountNo(value:String):void
    {
       this.repAccountNoField = value;
    }
    public function get total():Number
    {
        return this.totalField;
    }
    public function set total(value:Number):void
    {
       this.totalField = value;
    }
    public function get shippingTotal():Number
    {
        return this.shippingTotalField;
    }
    public function set shippingTotal(value:Number):void
    {
       this.shippingTotalField = value;
    }
    public function get grandTotal():Number
    {
        return this.grandTotalField;
    }
    public function set grandTotal(value:Number):void
    {
       this.grandTotalField = value;
    }
    public function get jobsiteContactPh():String
    {
        return this.jobsiteContactPhField;
    }
    public function set jobsiteContactPh(value:String):void
    {
       this.jobsiteContactPhField = value;
    }
    public function get phone():String
    {
        return this.phoneField;
    }
    public function set phone(value:String):void
    {
       this.phoneField = value;
    }
    public function get fax():String
    {
        return this.faxField;
    }
    public function set fax(value:String):void
    {
       this.faxField = value;
    }
    public function get email():String
    {
        return this.emailField;
    }
    public function set email(value:String):void
    {
       this.emailField = value;
    }
    public function get salesPerson():String
    {
        return this.salesPersonField;
    }
    public function set salesPerson(value:String):void
    {
       this.salesPersonField = value;
    }
    public function get markOrder():String
    {
        return this.markOrderField;
    }
    public function set markOrder(value:String):void
    {
       this.markOrderField = value;
    }
    public function get deliveryRequest():String
    {
        return this.deliveryRequestField;
    }
    public function set deliveryRequest(value:String):void
    {
       this.deliveryRequestField = value;
    }
    public function get MACPACXML():String
    {
        return this.mACPACXMLField;
    }
    public function set MACPACXML(value:String):void
    {
       this.mACPACXMLField = value;
    }
    public function get MACPACFileName():String
    {
        return this.mACPACFileNameField;
    }
    public function set MACPACFileName(value:String):void
    {
       this.mACPACFileNameField = value;
    }
    public function get shopingCartShipmentId():int
    {
        return this.shopingCartShipmentIdField;
    }
    public function set shopingCartShipmentId(value:int):void
    {
       this.shopingCartShipmentIdField = value;
    }
    public function get soldName():String
    {
        return this.soldNameField;
    }
    public function set soldName(value:String):void
    {
       this.soldNameField = value;
    }
    public function get soldAddress1():String
    {
        return this.soldAddress1Field;
    }
    public function set soldAddress1(value:String):void
    {
       this.soldAddress1Field = value;
    }
    public function get soldAddress2():String
    {
        return this.soldAddress2Field;
    }
    public function set soldAddress2(value:String):void
    {
       this.soldAddress2Field = value;
    }
    public function get soldCity():String
    {
        return this.soldCityField;
    }
    public function set soldCity(value:String):void
    {
       this.soldCityField = value;
    }
    public function get soldState():String
    {
        return this.soldStateField;
    }
    public function set soldState(value:String):void
    {
       this.soldStateField = value;
    }
    public function get soldZip():String
    {
        return this.soldZipField;
    }
    public function set soldZip(value:String):void
    {
       this.soldZipField = value;
    }
    public function get soldCountry():String
    {
        return this.soldCountryField;
    }
    public function set soldCountry(value:String):void
    {
       this.soldCountryField = value;
    }
    public function get shipName():String
    {
        return this.shipNameField;
    }
    public function set shipName(value:String):void
    {
       this.shipNameField = value;
    }
    public function get shipAddress1():String
    {
        return this.shipAddress1Field;
    }
    public function set shipAddress1(value:String):void
    {
       this.shipAddress1Field = value;
    }
    public function get shipAddress2():String
    {
        return this.shipAddress2Field;
    }
    public function set shipAddress2(value:String):void
    {
       this.shipAddress2Field = value;
    }
    public function get shipCity():String
    {
        return this.shipCityField;
    }
    public function set shipCity(value:String):void
    {
       this.shipCityField = value;
    }
    public function get shipState():String
    {
        return this.shipStateField;
    }
    public function set shipState(value:String):void
    {
       this.shipStateField = value;
    }
    public function get shipZip():String
    {
        return this.shipZipField;
    }
    public function set shipZip(value:String):void
    {
       this.shipZipField = value;
    }
    public function get shipCountry():String
    {
        return this.shipCountryField;
    }
    public function set shipCountry(value:String):void
    {
       this.shipCountryField = value;
    }
    public function get marketingProgram():String
    {
        return this.marketingProgramField;
    }
    public function set marketingProgram(value:String):void
    {
       this.marketingProgramField = value;
    }
    public function get details():AerSysCo.Server.OrderDetailCollection
    {
        return this.detailsField;
    }
    public function set details(value:AerSysCo.Server.OrderDetailCollection):void
    {
       this.detailsField = value;
    }
    public function get warehouse():AerSysCo.Server.Warehouse
    {
        return this.warehouseField;
    }
    public function set warehouse(value:AerSysCo.Server.Warehouse):void
    {
       this.warehouseField = value;
    }
    public function get shippingType():AerSysCo.Server.ShippingType
    {
        return this.shippingTypeField;
    }
    public function set shippingType(value:AerSysCo.Server.ShippingType):void
    {
       this.shippingTypeField = value;
    }
    public function get customer():AerSysCo.Server.Customer
    {
        return this.customerField;
    }
    public function set customer(value:AerSysCo.Server.Customer):void
    {
       this.customerField = value;
    }
    public function get createdByUser():String
    {
        return this.createdByUserField;
    }
    public function set createdByUser(value:String):void
    {
       this.createdByUserField = value;
    }
    public function get lastUpdateDate():Date
    {
        return this.lastUpdateDateField;
    }
    public function set lastUpdateDate(value:Date):void
    {
       this.lastUpdateDateField = value;
    }
    public function get dateCreated():Date
    {
        return this.dateCreatedField;
    }
    public function set dateCreated(value:Date):void
    {
       this.dateCreatedField = value;
    }

  }
}