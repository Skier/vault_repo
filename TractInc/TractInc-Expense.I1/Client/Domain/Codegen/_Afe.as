
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _Afe extends ActiveRecord
      {
      
        public var AFE: String;
      
        public var ClientId: int;
      
        public var AFEName: String;
      
        public var AFEStatus: String;
      
        public var AfeGuid: String;
      

      public function _Afe()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      AFE = object["AFE"];
    ClientId = object["ClientId"];
    AFEName = object["AFEName"];
    AFEStatus = object["AFEStatus"];
    AfeGuid = object["AfeGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.Afe;
      }

      }

      }
    