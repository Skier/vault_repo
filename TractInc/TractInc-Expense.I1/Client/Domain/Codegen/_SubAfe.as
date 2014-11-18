
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _SubAfe extends ActiveRecord
      {
      
        public var SubAFE: String;
      
        public var AFE: String;
      
        public var SubAFEStatus: String;
      
        public var SubAfeGuid: String;
      

      public function _SubAfe()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      SubAFE = object["SubAFE"];
    AFE = object["AFE"];
    SubAFEStatus = object["SubAFEStatus"];
    SubAfeGuid = object["SubAfeGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.SubAfe;
      }

      }

      }
    