
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _SyncLog extends ActiveRecord
      {
      
        public var SyncLogId: int;
      
        public var AssetId: int;
      
        public var DeviceId: String;
      

      public function _SyncLog()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      SyncLogId = object["SyncLogId"];
    AssetId = object["AssetId"];
    DeviceId = object["DeviceId"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.SyncLog;
      }

      }

      }
    