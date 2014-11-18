
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _BillItem extends ActiveRecord
      {
      
        public var BillItemId: int;
      
        public var BillId: int;
      
        public var AssetAssignmentId: int;
      
        public var BillingDate: Date;
      
        public var DayQty: int;
      
        public var BillRate: Number;
      
        public var TotalHourlyBilling: Number;
      
        public var Lodging: Number;
      
        public var Meals: Number;
      
        public var Phone: Number;
      
        public var Copies: Number;
      
        public var FilingFees: Number;
      
        public var Status: String;
      
        public var Notes: String;
      
        public var BillItemGuid: String;
      

      public function _BillItem()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      BillItemId = object["BillItemId"];
    BillId = object["BillId"];
    AssetAssignmentId = object["AssetAssignmentId"];
    BillingDate = object["BillingDate"];
    DayQty = object["DayQty"];
    BillRate = object["BillRate"];
    TotalHourlyBilling = object["TotalHourlyBilling"];
    Lodging = object["Lodging"];
    Meals = object["Meals"];
    Phone = object["Phone"];
    Copies = object["Copies"];
    FilingFees = object["FilingFees"];
    Status = object["Status"];
    Notes = object["Notes"];
    BillItemGuid = object["BillItemGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.BillItem;
      }

      }

      }
    