
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.UserAsset;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _UserAssetDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new UserAsset();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.UserAssetDataMapper";
          }
          
      		public function load(userAsset:UserAsset, responder:Responder = null):UserAsset
          {
            
              if(!userAsset.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(userAsset.getURI()))
              {
                userAsset = UserAsset(IdentityMap.extract(userAsset.getURI()));
                
                if(userAsset.IsLoaded || userAsset.IsLoading)
                  return userAsset;
      
              } 
              else
               IdentityMap.add(userAsset);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                userAsset.UserAssetId),null,userAsset);
            
              return userAsset;
          }
          
      
          public function findByPrimaryKey(  userAssetId:int):UserAsset
          {
          
            var activeRecord:UserAsset = new UserAsset();
      
            
              activeRecord.UserAssetId = userAssetId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:UserAsset = UserAsset(activeRecord);
                   
        
         }
        }
      }
    