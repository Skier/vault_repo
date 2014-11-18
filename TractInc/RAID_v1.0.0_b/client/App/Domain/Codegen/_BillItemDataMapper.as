
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.BillItem;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _BillItemDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new BillItem();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillItemDataMapper";
          }
          
      		public function load(billItem:BillItem, responder:Responder = null):BillItem
          {
            
              if(!billItem.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(billItem.getURI()))
              {
                billItem = BillItem(IdentityMap.extract(billItem.getURI()));
                
                if(billItem.IsLoaded || billItem.IsLoading)
                  return billItem;
      
              } 
              else
               IdentityMap.add(billItem);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                billItem.BillItemId),null,billItem);
            
              return billItem;
          }
          
      
          public function findByPrimaryKey(  billItemId:int):BillItem
          {
          
            var activeRecord:BillItem = new BillItem();
      
            
              activeRecord.BillItemId = billItemId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:BillItem = BillItem(activeRecord);
                   
        
              
              if(relationName == "relatedBillItemAttachment")
              {
   
                DataMapperRegistry.Instance.BillItemAttachment.
                findByBillItemId(
                
                  item.BillItemId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedInvoiceItem")
              {
   
                DataMapperRegistry.Instance.InvoiceItem.
                findByBillItemId(
                
                  item.BillItemId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    