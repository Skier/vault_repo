
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.SyncLog;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _SyncLogDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new SyncLog();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.SyncLogDataMapper";
          }
          
      		public function load(syncLog:SyncLog, responder:Responder = null):SyncLog
          {
            
              if(!syncLog.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(syncLog.getURI()))
              {
                syncLog = SyncLog(IdentityMap.extract(syncLog.getURI()));
                
                if(syncLog.IsLoaded || syncLog.IsLoading)
                  return syncLog;
      
              } 
              else
               IdentityMap.add(syncLog);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                syncLog.SyncLogId),null,syncLog);
            
              return syncLog;
          }
          
      
          public function findByPrimaryKey(  syncLogId:int):SyncLog
          {
          
            var activeRecord:SyncLog = new SyncLog();
      
            
              activeRecord.SyncLogId = syncLogId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:SyncLog = SyncLog(activeRecord);
                   
        
         }
        }
      }
    