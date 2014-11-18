
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.RateByAssignment;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _RateByAssignmentDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new RateByAssignment();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.RateByAssignmentDataMapper";
          }
          
      		public function load(rateByAssignment:RateByAssignment, responder:Responder = null):RateByAssignment
          {
            
              if(!rateByAssignment.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(rateByAssignment.getURI()))
              {
                rateByAssignment = RateByAssignment(IdentityMap.extract(rateByAssignment.getURI()));
                
                if(rateByAssignment.IsLoaded || rateByAssignment.IsLoading)
                  return rateByAssignment;
      
              } 
              else
               IdentityMap.add(rateByAssignment);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                rateByAssignment.RateByAssignmentId),null,rateByAssignment);
            
              return rateByAssignment;
          }
          
      
          public function findByPrimaryKey(  rateByAssignmentId:int):RateByAssignment
          {
          
            var activeRecord:RateByAssignment = new RateByAssignment();
      
            
              activeRecord.RateByAssignmentId = rateByAssignmentId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:RateByAssignment = RateByAssignment(activeRecord);
                   
        
         }
        }
      }
    