
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Bill;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _BillDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Bill();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillDataMapper";
          }
          
      		public function load(bill:Bill, responder:Responder = null):Bill
          {
            
              if(!bill.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(bill.getURI()))
              {
                bill = Bill(IdentityMap.extract(bill.getURI()));
                
                if(bill.IsLoaded || bill.IsLoading)
                  return bill;
      
              } 
              else
               IdentityMap.add(bill);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                bill.BillId),null,bill);
            
              return bill;
          }
          
      
          public function findByPrimaryKey(  billId:int):Bill
          {
          
            var activeRecord:Bill = new Bill();
      
            
              activeRecord.BillId = billId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Bill = Bill(activeRecord);
                   
        
              
              if(relationName == "relatedBillItemComposition")
              {
   
                DataMapperRegistry.Instance.BillItemComposition.
                findByBillId(
                
                  item.BillId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedBillItem")
              {
   
                DataMapperRegistry.Instance.BillItem.
                findByBillId(
                
                  item.BillId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    