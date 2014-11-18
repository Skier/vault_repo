
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.InvoiceStatus;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _InvoiceStatusDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new InvoiceStatus();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.InvoiceStatusDataMapper";
          }
          
      		public function load(invoiceStatus:InvoiceStatus, responder:Responder = null):InvoiceStatus
          {
            
              if(!invoiceStatus.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(invoiceStatus.getURI()))
              {
                invoiceStatus = InvoiceStatus(IdentityMap.extract(invoiceStatus.getURI()));
                
                if(invoiceStatus.IsLoaded || invoiceStatus.IsLoading)
                  return invoiceStatus;
      
              } 
              else
               IdentityMap.add(invoiceStatus);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                invoiceStatus.Status),null,invoiceStatus);
            
              return invoiceStatus;
          }
          
      
          public function findByPrimaryKey(  status:String):InvoiceStatus
          {
          
            var activeRecord:InvoiceStatus = new InvoiceStatus();
      
            
              activeRecord.Status = status;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:InvoiceStatus = InvoiceStatus(activeRecord);
                   
        
              
              if(relationName == "relatedInvoice")
              {
   
                DataMapperRegistry.Instance.Invoice.
                findByStatus(
                
                  item.Status, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    