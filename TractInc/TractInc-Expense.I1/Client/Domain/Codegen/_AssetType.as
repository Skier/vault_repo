
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _AssetType extends ActiveRecord
      {
      
        public var Type: String;
      

      public function _AssetType()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      Type = object["Type"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.AssetType;
      }

      }

      }
    