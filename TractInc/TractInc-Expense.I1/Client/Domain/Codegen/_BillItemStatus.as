
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _BillItemStatus extends ActiveRecord
      {
      
        public var Status: String;
      

      public function _BillItemStatus()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      Status = object["Status"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.BillItemStatus;
      }

      }

      }
    