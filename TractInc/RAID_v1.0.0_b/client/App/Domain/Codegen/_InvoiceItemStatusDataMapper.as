
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.InvoiceItemStatus;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _InvoiceItemStatusDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new InvoiceItemStatus();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.InvoiceItemStatusDataMapper";
          }
          
      		public function load(invoiceItemStatus:InvoiceItemStatus, responder:Responder = null):InvoiceItemStatus
          {
            
              if(!invoiceItemStatus.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(invoiceItemStatus.getURI()))
              {
                invoiceItemStatus = InvoiceItemStatus(IdentityMap.extract(invoiceItemStatus.getURI()));
                
                if(invoiceItemStatus.IsLoaded || invoiceItemStatus.IsLoading)
                  return invoiceItemStatus;
      
              } 
              else
               IdentityMap.add(invoiceItemStatus);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                invoiceItemStatus.Status),null,invoiceItemStatus);
            
              return invoiceItemStatus;
          }
          
      
          public function findByPrimaryKey(  status:String):InvoiceItemStatus
          {
          
            var activeRecord:InvoiceItemStatus = new InvoiceItemStatus();
      
            
              activeRecord.Status = status;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:InvoiceItemStatus = InvoiceItemStatus(activeRecord);
                   
        
              
              if(relationName == "relatedInvoiceItem")
              {
   
                DataMapperRegistry.Instance.InvoiceItem.
                findByStatus(
                
                  item.Status, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    