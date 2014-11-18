
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.DefaultInvoiceRate;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _DefaultInvoiceRateDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new DefaultInvoiceRate();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.DefaultInvoiceRateDataMapper";
          }
          
      		public function load(defaultInvoiceRate:DefaultInvoiceRate, responder:Responder = null):DefaultInvoiceRate
          {
            
              if(!defaultInvoiceRate.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(defaultInvoiceRate.getURI()))
              {
                defaultInvoiceRate = DefaultInvoiceRate(IdentityMap.extract(defaultInvoiceRate.getURI()));
                
                if(defaultInvoiceRate.IsLoaded || defaultInvoiceRate.IsLoading)
                  return defaultInvoiceRate;
      
              } 
              else
               IdentityMap.add(defaultInvoiceRate);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                defaultInvoiceRate.DefaultInvoiceRateId),null,defaultInvoiceRate);
            
              return defaultInvoiceRate;
          }
          
      
          public function findByPrimaryKey(  defaultInvoiceRateId:int):DefaultInvoiceRate
          {
          
            var activeRecord:DefaultInvoiceRate = new DefaultInvoiceRate();
      
            
              activeRecord.DefaultInvoiceRateId = defaultInvoiceRateId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:DefaultInvoiceRate = DefaultInvoiceRate(activeRecord);
                   
        
         }
        }
      }
    