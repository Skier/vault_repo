
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _Client extends ActiveRecord
      {
      
        public var ClientId: int;
      
        public var ClientName: String;
      
        public var Active: Boolean;
      
        public var ClientGuid: String;
      

      public function _Client()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      ClientId = object["ClientId"];
    ClientName = object["ClientName"];
    Active = object["Active"];
    ClientGuid = object["ClientGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.Client;
      }

      }

      }
    