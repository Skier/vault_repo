
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _Rate extends ActiveRecord
      {
      
        public var AssetId: int;
      
        public var ClientId: int;
      
        public var DateRate: Number;
      
        public var MilageRate: Number;
      
        public var RateGuid: String;
      

      public function _Rate()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      AssetId = object["AssetId"];
    ClientId = object["ClientId"];
    DateRate = object["DateRate"];
    MilageRate = object["MilageRate"];
    RateGuid = object["RateGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.Rate;
      }

      }

      }
    