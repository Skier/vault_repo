
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.AssetAssignment;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _AssetAssignmentDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new AssetAssignment();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.AssetAssignmentDataMapper";
          }
          
      		public function load(assetAssignment:AssetAssignment, responder:Responder = null):AssetAssignment
          {
            
              if(!assetAssignment.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(assetAssignment.getURI()))
              {
                assetAssignment = AssetAssignment(IdentityMap.extract(assetAssignment.getURI()));
                
                if(assetAssignment.IsLoaded || assetAssignment.IsLoading)
                  return assetAssignment;
      
              } 
              else
               IdentityMap.add(assetAssignment);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                assetAssignment.AssetAssignmentId),null,assetAssignment);
            
              return assetAssignment;
          }
          
      
          public function findByPrimaryKey(  assetAssignmentId:int):AssetAssignment
          {
          
            var activeRecord:AssetAssignment = new AssetAssignment();
      
            
              activeRecord.AssetAssignmentId = assetAssignmentId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:AssetAssignment = AssetAssignment(activeRecord);
                   
        
              
              if(relationName == "relatedBillItem")
              {
   
                DataMapperRegistry.Instance.BillItem.
                findByAssetAssignmentId(
                
                  item.AssetAssignmentId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedRateByAssignment")
              {
   
                DataMapperRegistry.Instance.RateByAssignment.
                findByAssetAssignmentId(
                
                  item.AssetAssignmentId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedInvoiceItem")
              {
   
                DataMapperRegistry.Instance.InvoiceItem.
                findByAssetAssignmentId(
                
                  item.AssetAssignmentId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    