
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.AssetType;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _AssetTypeDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new AssetType();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.AssetTypeDataMapper";
          }
          
      		public function load(assetType:AssetType, responder:Responder = null):AssetType
          {
            
              if(!assetType.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(assetType.getURI()))
              {
                assetType = AssetType(IdentityMap.extract(assetType.getURI()));
                
                if(assetType.IsLoaded || assetType.IsLoading)
                  return assetType;
      
              } 
              else
               IdentityMap.add(assetType);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                assetType._Type),null,assetType);
            
              return assetType;
          }
          
      
          public function findByPrimaryKey(  type:String):AssetType
          {
          
            var activeRecord:AssetType = new AssetType();
      
            
              activeRecord._Type = type;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:AssetType = AssetType(activeRecord);
                   
        
              
              if(relationName == "relatedAsset")
              {
   
                DataMapperRegistry.Instance.Asset.
                findBy_Type(
                
                  item._Type, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    