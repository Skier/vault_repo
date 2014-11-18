
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.BillItemComposition;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _BillItemCompositionDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new BillItemComposition();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillItemCompositionDataMapper";
          }
          
      		public function load(billItemComposition:BillItemComposition, responder:Responder = null):BillItemComposition
          {
            
              if(!billItemComposition.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(billItemComposition.getURI()))
              {
                billItemComposition = BillItemComposition(IdentityMap.extract(billItemComposition.getURI()));
                
                if(billItemComposition.IsLoaded || billItemComposition.IsLoading)
                  return billItemComposition;
      
              } 
              else
               IdentityMap.add(billItemComposition);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                billItemComposition.BillItemCompositionId),null,billItemComposition);
            
              return billItemComposition;
          }
          
      
          public function findByPrimaryKey(  billItemCompositionId:int):BillItemComposition
          {
          
            var activeRecord:BillItemComposition = new BillItemComposition();
      
            
              activeRecord.BillItemCompositionId = billItemCompositionId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:BillItemComposition = BillItemComposition(activeRecord);
                   
        
              
              if(relationName == "relatedBillItem")
              {
   
                DataMapperRegistry.Instance.BillItem.
                findByBillItemCompositionId(
                
                  item.BillItemCompositionId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    