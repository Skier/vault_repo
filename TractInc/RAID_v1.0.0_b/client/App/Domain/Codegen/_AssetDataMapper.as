
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Asset;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _AssetDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Asset();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.AssetDataMapper";
          }
          
      		public function load(asset:Asset, responder:Responder = null):Asset
          {
            
              if(!asset.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(asset.getURI()))
              {
                asset = Asset(IdentityMap.extract(asset.getURI()));
                
                if(asset.IsLoaded || asset.IsLoading)
                  return asset;
      
              } 
              else
               IdentityMap.add(asset);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                asset.AssetId),null,asset);
            
              return asset;
          }
          
      
          public function findByPrimaryKey(  assetId:int):Asset
          {
          
            var activeRecord:Asset = new Asset();
      
            
              activeRecord.AssetId = assetId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Asset = Asset(activeRecord);
                   
        
              
              if(relationName == "relatedBill")
              {
   
                DataMapperRegistry.Instance.Bill.
                findByAssetId(
                
                  item.AssetId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedAssetAssignment")
              {
   
                DataMapperRegistry.Instance.AssetAssignment.
                findByAssetId(
                
                  item.AssetId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedUserAsset")
              {
   
                DataMapperRegistry.Instance.UserAsset.
                findByAssetId(
                
                  item.AssetId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    