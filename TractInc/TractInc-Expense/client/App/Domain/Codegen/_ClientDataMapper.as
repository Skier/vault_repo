
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Client;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _ClientDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Client();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.ClientDataMapper";
          }
          
      		public function load(client:Client, responder:Responder = null):Client
          {
            
              if(!client.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(client.getURI()))
              {
                client = Client(IdentityMap.extract(client.getURI()));
                
                if(client.IsLoaded || client.IsLoading)
                  return client;
      
              } 
              else
               IdentityMap.add(client);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                client.ClientId),null,client);
            
              return client;
          }
          
      
          public function findByPrimaryKey(  clientId:int):Client
          {
          
            var activeRecord:Client = new Client();
      
            
              activeRecord.ClientId = clientId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Client = Client(activeRecord);
                   
        
              
              if(relationName == "relatedAfe")
              {
   
                DataMapperRegistry.Instance.Afe.
                findByClientId(
                
                  item.ClientId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedDefaultInvoiceRate")
              {
   
                DataMapperRegistry.Instance.DefaultInvoiceRate.
                findByClientId(
                
                  item.ClientId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedInvoice")
              {
   
                DataMapperRegistry.Instance.Invoice.
                findByClientId(
                
                  item.ClientId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    