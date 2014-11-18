
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.AfeStatus;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _AfeStatusDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new AfeStatus();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.AfeStatusDataMapper";
          }
          
      		public function load(afeStatus:AfeStatus, responder:Responder = null):AfeStatus
          {
            
              if(!afeStatus.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(afeStatus.getURI()))
              {
                afeStatus = AfeStatus(IdentityMap.extract(afeStatus.getURI()));
                
                if(afeStatus.IsLoaded || afeStatus.IsLoading)
                  return afeStatus;
      
              } 
              else
               IdentityMap.add(afeStatus);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                afeStatus.AFEStatus),null,afeStatus);
            
              return afeStatus;
          }
          
      
          public function findByPrimaryKey(  aFEStatus:String):AfeStatus
          {
          
            var activeRecord:AfeStatus = new AfeStatus();
      
            
              activeRecord.AFEStatus = aFEStatus;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:AfeStatus = AfeStatus(activeRecord);
                   
        
              
              if(relationName == "relatedAfe")
              {
   
                DataMapperRegistry.Instance.Afe.
                findByAFEStatus(
                
                  item.AFEStatus, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    