
      package Domain.Codegen
      {
      import WDM.ActiveRecord;
      import WDM.DataMapper;
      import Domain.*;
      import mx.rpc.events.ResultEvent;


      [Bindable]
      public class _AssetAssignment extends ActiveRecord
      {
      
        public var AssetAssignmentId: int;
      
        public var AFE: String;
      
        public var SubAFE: String;
      
        public var AssetId: int;
      
        public var BillRate: Number;
      
        public var PayRate: Number;
      
        public var AssetAssignmentGuid: String;
      

      public function _AssetAssignment()
      {
        LinstenChanges();
      }

      public override function applyFields(object:Object):void
      {
      AssetAssignmentId = object["AssetAssignmentId"];
    AFE = object["AFE"];
    SubAFE = object["SubAFE"];
    AssetId = object["AssetId"];
    BillRate = object["BillRate"];
    PayRate = object["PayRate"];
    AssetAssignmentGuid = object["AssetAssignmentGuid"];
    
      }

      protected override function get dataMapper():DataMapper
      {
        return DataMapperRegistry.Instance.AssetAssignment;
      }

      }

      }
    