
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.BillStatus;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _BillStatusDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new BillStatus();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillStatusDataMapper";
          }
          
      		public function load(billStatus:BillStatus, responder:Responder = null):BillStatus
          {
            
              if(!billStatus.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(billStatus.getURI()))
              {
                billStatus = BillStatus(IdentityMap.extract(billStatus.getURI()));
                
                if(billStatus.IsLoaded || billStatus.IsLoading)
                  return billStatus;
      
              } 
              else
               IdentityMap.add(billStatus);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                billStatus.Status),null,billStatus);
            
              return billStatus;
          }
          
      
          public function findByPrimaryKey(  status:String):BillStatus
          {
          
            var activeRecord:BillStatus = new BillStatus();
      
            
              activeRecord.Status = status;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:BillStatus = BillStatus(activeRecord);
                   
        
              
              if(relationName == "relatedBill")
              {
   
                DataMapperRegistry.Instance.Bill.
                findByStatus(
                
                  item.Status, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    