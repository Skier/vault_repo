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

  public class Customer
  {
    private var customerIdField:int = 0;
    private var defaultWarehouseIdField:int = 0;
    private var mACPACCustonerNumberField:String="";
    private var salesRepCompanyNameField:String="";
    private var creditStatusField:Boolean;
    private var maxOrderTotalField:Number = 0;
    private var createdByUserField:String="";
    private var lastUpdateDateField:Date;
    private var dateCreatedField:Date;
    private var brandIdField:int = 0;
    private var brandNameField:String="";
    private var dayBalanceField:Number = 0;
    private var addressField:AerSysCo.Server.Address;
    private var phoneNumberField:String="";
    private var faxField:String="";
    private var emailField:String="";

    public function Customer()
    {
      this.addressField = new AerSysCo.Server.Address();

    }

    public function get customerId():int
    {
        return this.customerIdField;
    }
    public function set customerId(value:int):void
    {
       this.customerIdField = value;
    }
    public function get defaultWarehouseId():int
    {
        return this.defaultWarehouseIdField;
    }
    public function set defaultWarehouseId(value:int):void
    {
       this.defaultWarehouseIdField = value;
    }
    public function get MACPACCustonerNumber():String
    {
        return this.mACPACCustonerNumberField;
    }
    public function set MACPACCustonerNumber(value:String):void
    {
       this.mACPACCustonerNumberField = value;
    }
    public function get salesRepCompanyName():String
    {
        return this.salesRepCompanyNameField;
    }
    public function set salesRepCompanyName(value:String):void
    {
       this.salesRepCompanyNameField = value;
    }
    public function get creditStatus():Boolean
    {
        return this.creditStatusField;
    }
    public function set creditStatus(value:Boolean):void
    {
       this.creditStatusField = value;
    }
    public function get maxOrderTotal():Number
    {
        return this.maxOrderTotalField;
    }
    public function set maxOrderTotal(value:Number):void
    {
       this.maxOrderTotalField = value;
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
    public function get brandId():int
    {
        return this.brandIdField;
    }
    public function set brandId(value:int):void
    {
       this.brandIdField = value;
    }
    public function get brandName():String
    {
        return this.brandNameField;
    }
    public function set brandName(value:String):void
    {
       this.brandNameField = value;
    }
    public function get dayBalance():Number
    {
        return this.dayBalanceField;
    }
    public function set dayBalance(value:Number):void
    {
       this.dayBalanceField = value;
    }
    public function get address():AerSysCo.Server.Address
    {
        return this.addressField;
    }
    public function set address(value:AerSysCo.Server.Address):void
    {
       this.addressField = value;
    }
    public function get phoneNumber():String
    {
        return this.phoneNumberField;
    }
    public function set phoneNumber(value:String):void
    {
       this.phoneNumberField = value;
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

  }
}