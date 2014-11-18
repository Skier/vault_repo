
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.WorkLog;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _WorkLogDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new WorkLog();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.WorkLogDataMapper";
          }
          
      		public function load(workLog:WorkLog, responder:Responder = null):WorkLog
          {
            
              if(!workLog.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(workLog.getURI()))
              {
                workLog = WorkLog(IdentityMap.extract(workLog.getURI()));
                
                if(workLog.IsLoaded || workLog.IsLoading)
                  return workLog;
      
              } 
              else
               IdentityMap.add(workLog);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                workLog.WorkLogId),null,workLog);
            
              return workLog;
          }
          
      
          public function findByPrimaryKey(  workLogId:int):WorkLog
          {
          
            var activeRecord:WorkLog = new WorkLog();
      
            
              activeRecord.WorkLogId = workLogId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:WorkLog = WorkLog(activeRecord);
                   
        
         }
        }
      }
    