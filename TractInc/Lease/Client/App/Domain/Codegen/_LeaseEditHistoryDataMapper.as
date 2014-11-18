
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.LeaseEditHistory;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _LeaseEditHistoryDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new LeaseEditHistory();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.LeaseEditHistoryDataMapper";
          }
          
      		public function load(leaseEditHistory:LeaseEditHistory, responder:Responder = null):LeaseEditHistory
          {
            
              if(!leaseEditHistory.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(leaseEditHistory.getURI()))
              {
                leaseEditHistory = LeaseEditHistory(IdentityMap.extract(leaseEditHistory.getURI()));
                
                if(leaseEditHistory.IsLoaded)
                  return leaseEditHistory;
      
              } 
              else
               IdentityMap.add(leaseEditHistory);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                leaseEditHistory.EditHistoryId),null,leaseEditHistory);
            
              return leaseEditHistory;
          }
          
      
          public function findByPrimaryKey(  editHistoryId:int):LeaseEditHistory
          {
            var activeRecord:LeaseEditHistory = new LeaseEditHistory();

            
              activeRecord.EditHistoryId = editHistoryId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:LeaseEditHistory = LeaseEditHistory(activeRecord);
                   
        
         }
        }
      }
    