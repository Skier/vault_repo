
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.InvoiceItem;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _InvoiceItemDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new InvoiceItem();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.InvoiceItemDataMapper";
          }
          
      		public function load(invoiceItem:InvoiceItem, responder:Responder = null):InvoiceItem
          {
            
              if(!invoiceItem.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(invoiceItem.getURI()))
              {
                invoiceItem = InvoiceItem(IdentityMap.extract(invoiceItem.getURI()));
                
                if(invoiceItem.IsLoaded || invoiceItem.IsLoading)
                  return invoiceItem;
      
              } 
              else
               IdentityMap.add(invoiceItem);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                invoiceItem.InvoiceItemId),null,invoiceItem);
            
              return invoiceItem;
          }
          
      
          public function findByPrimaryKey(  invoiceItemId:int):InvoiceItem
          {
          
            var activeRecord:InvoiceItem = new InvoiceItem();
      
            
              activeRecord.InvoiceItemId = invoiceItemId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:InvoiceItem = InvoiceItem(activeRecord);
                   
        
         }
        }
      }
    