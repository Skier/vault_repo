
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _Bill extends ActiveRecord
      {
      
        public var BillId: int;
      
        public var BillStatus: String;
      
        public var Notes: String;
      
        public var BillGuid: String;
      

      public function _Bill()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      BillId = object["BillId"];
    BillStatus = object["BillStatus"];
    Notes = object["Notes"];
    BillGuid = object["BillGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.Bill;
      }

      }

      }
    