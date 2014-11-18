
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.DefaultBillRate;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _DefaultBillRateDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new DefaultBillRate();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.DefaultBillRateDataMapper";
          }
          
      		public function load(defaultBillRate:DefaultBillRate, responder:Responder = null):DefaultBillRate
          {
            
              if(!defaultBillRate.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(defaultBillRate.getURI()))
              {
                defaultBillRate = DefaultBillRate(IdentityMap.extract(defaultBillRate.getURI()));
                
                if(defaultBillRate.IsLoaded || defaultBillRate.IsLoading)
                  return defaultBillRate;
      
              } 
              else
               IdentityMap.add(defaultBillRate);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                defaultBillRate.DefaultBillRateId),null,defaultBillRate);
            
              return defaultBillRate;
          }
          
      
          public function findByPrimaryKey(  defaultBillRateId:int):DefaultBillRate
          {
          
            var activeRecord:DefaultBillRate = new DefaultBillRate();
      
            
              activeRecord.DefaultBillRateId = defaultBillRateId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:DefaultBillRate = DefaultBillRate(activeRecord);
                   
        
         }
        }
      }
    