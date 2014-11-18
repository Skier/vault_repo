
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Afe;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _AfeDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Afe();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.AfeDataMapper";
          }
          
      		public function load(afe:Afe, responder:Responder = null):Afe
          {
            
              if(!afe.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(afe.getURI()))
              {
                afe = Afe(IdentityMap.extract(afe.getURI()));
                
                if(afe.IsLoaded || afe.IsLoading)
                  return afe;
      
              } 
              else
               IdentityMap.add(afe);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                afe.AFE),null,afe);
            
              return afe;
          }
          
      
          public function findByPrimaryKey(  aFE:String):Afe
          {
          
            var activeRecord:Afe = new Afe();
      
            
              activeRecord.AFE = aFE;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Afe = Afe(activeRecord);
                   
        
              
              if(relationName == "relatedSubAfe")
              {
   
                DataMapperRegistry.Instance.SubAfe.
                findByAFE(
                
                  item.AFE, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedAssetAssignment")
              {
   
                DataMapperRegistry.Instance.AssetAssignment.
                findByAFE(
                
                  item.AFE, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    