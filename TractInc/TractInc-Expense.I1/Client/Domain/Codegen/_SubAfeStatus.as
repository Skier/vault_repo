
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _SubAfeStatus extends ActiveRecord
      {
      
        public var SubAFEStatus: String;
      

      public function _SubAfeStatus()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      SubAFEStatus = object["SubAFEStatus"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.SubAfeStatus;
      }

      }

      }
    