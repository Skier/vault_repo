
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.BillItemType;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _BillItemTypeDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new BillItemType();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillItemTypeDataMapper";
          }
          
      		public function load(billItemType:BillItemType, responder:Responder = null):BillItemType
          {
            
              if(!billItemType.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(billItemType.getURI()))
              {
                billItemType = BillItemType(IdentityMap.extract(billItemType.getURI()));
                
                if(billItemType.IsLoaded || billItemType.IsLoading)
                  return billItemType;
      
              } 
              else
               IdentityMap.add(billItemType);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                billItemType.BillItemTypeId),null,billItemType);
            
              return billItemType;
          }
          
      
          public function findByPrimaryKey(  billItemTypeId:int):BillItemType
          {
          
            var activeRecord:BillItemType = new BillItemType();
      
            
              activeRecord.BillItemTypeId = billItemTypeId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:BillItemType = BillItemType(activeRecord);
                   
        
              
              if(relationName == "relatedBillItem")
              {
   
                DataMapperRegistry.Instance.BillItem.
                findByBillItemTypeId(
                
                  item.BillItemTypeId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedRateByAssignment")
              {
   
                DataMapperRegistry.Instance.RateByAssignment.
                findByBillItemTypeId(
                
                  item.BillItemTypeId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    