
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.InvoiceItemType;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _InvoiceItemTypeDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new InvoiceItemType();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.InvoiceItemTypeDataMapper";
          }
          
      		public function load(invoiceItemType:InvoiceItemType, responder:Responder = null):InvoiceItemType
          {
            
              if(!invoiceItemType.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(invoiceItemType.getURI()))
              {
                invoiceItemType = InvoiceItemType(IdentityMap.extract(invoiceItemType.getURI()));
                
                if(invoiceItemType.IsLoaded || invoiceItemType.IsLoading)
                  return invoiceItemType;
      
              } 
              else
               IdentityMap.add(invoiceItemType);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                invoiceItemType.InvoiceItemTypeId),null,invoiceItemType);
            
              return invoiceItemType;
          }
          
      
          public function findByPrimaryKey(  invoiceItemTypeId:int):InvoiceItemType
          {
          
            var activeRecord:InvoiceItemType = new InvoiceItemType();
      
            
              activeRecord.InvoiceItemTypeId = invoiceItemTypeId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:InvoiceItemType = InvoiceItemType(activeRecord);
                   
        
              
              if(relationName == "relatedBillItemType")
              {
   
                DataMapperRegistry.Instance.BillItemType.
                findByInvoiceItemTypeId(
                
                  item.InvoiceItemTypeId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedDefaultInvoiceRate")
              {
   
                DataMapperRegistry.Instance.DefaultInvoiceRate.
                findByInvoiceItemTypeId(
                
                  item.InvoiceItemTypeId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedInvoiceItem")
              {
   
                DataMapperRegistry.Instance.InvoiceItem.
                findByInvoiceItemTypeId(
                
                  item.InvoiceItemTypeId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    