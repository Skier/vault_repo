
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _Asset extends ActiveRecord
      {
      
        public var AssetId: int;
      
        public var AssetType: String;
      
        public var ChiefAssetId: int;
      
        public var BusinessName: String;
      
        public var FirstName: String;
      
        public var MiddleName: String;
      
        public var LastName: String;
      
        public var SSN: String;
      
        public var AssetGuid: String;
      

      public function _Asset()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      AssetId = object["AssetId"];
    AssetType = object["AssetType"];
    ChiefAssetId = object["ChiefAssetId"];
    BusinessName = object["BusinessName"];
    FirstName = object["FirstName"];
    MiddleName = object["MiddleName"];
    LastName = object["LastName"];
    SSN = object["SSN"];
    AssetGuid = object["AssetGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.Asset;
      }

      }

      }
    