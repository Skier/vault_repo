
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.BillItemStatus;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _BillItemStatusDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new BillItemStatus();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillItemStatusDataMapper";
          }
          
      		public function load(billItemStatus:BillItemStatus, responder:Responder = null):BillItemStatus
          {
            
              if(!billItemStatus.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(billItemStatus.getURI()))
              {
                billItemStatus = BillItemStatus(IdentityMap.extract(billItemStatus.getURI()));
                
                if(billItemStatus.IsLoaded || billItemStatus.IsLoading)
                  return billItemStatus;
      
              } 
              else
               IdentityMap.add(billItemStatus);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                billItemStatus.Status),null,billItemStatus);
            
              return billItemStatus;
          }
          
      
          public function findByPrimaryKey(  status:String):BillItemStatus
          {
          
            var activeRecord:BillItemStatus = new BillItemStatus();
      
            
              activeRecord.Status = status;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:BillItemStatus = BillItemStatus(activeRecord);
                   
        
              
              if(relationName == "relatedBillItem")
              {
   
                DataMapperRegistry.Instance.BillItem.
                findByStatus(
                
                  item.Status, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    