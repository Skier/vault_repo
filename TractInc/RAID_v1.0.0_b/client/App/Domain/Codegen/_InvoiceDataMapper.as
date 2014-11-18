
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Invoice;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _InvoiceDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Invoice();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.InvoiceDataMapper";
          }
          
      		public function load(invoice:Invoice, responder:Responder = null):Invoice
          {
            
              if(!invoice.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(invoice.getURI()))
              {
                invoice = Invoice(IdentityMap.extract(invoice.getURI()));
                
                if(invoice.IsLoaded || invoice.IsLoading)
                  return invoice;
      
              } 
              else
               IdentityMap.add(invoice);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                invoice.InvoiceId),null,invoice);
            
              return invoice;
          }
          
      
          public function findByPrimaryKey(  invoiceId:int):Invoice
          {
          
            var activeRecord:Invoice = new Invoice();
      
            
              activeRecord.InvoiceId = invoiceId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Invoice = Invoice(activeRecord);
                   
        
              
              if(relationName == "relatedInvoiceItem")
              {
   
                DataMapperRegistry.Instance.InvoiceItem.
                findByInvoiceId(
                
                  item.InvoiceId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    