
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _AfeStatus extends ActiveRecord
      {
      
        public var AFEStatus: String;
      

      public function _AfeStatus()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      AFEStatus = object["AFEStatus"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.AfeStatus;
      }

      }

      }
    