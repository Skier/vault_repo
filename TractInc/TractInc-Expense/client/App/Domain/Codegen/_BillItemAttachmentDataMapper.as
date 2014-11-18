
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.BillItemAttachment;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _BillItemAttachmentDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new BillItemAttachment();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillItemAttachmentDataMapper";
          }
          
      		public function load(billItemAttachment:BillItemAttachment, responder:Responder = null):BillItemAttachment
          {
            
              if(!billItemAttachment.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(billItemAttachment.getURI()))
              {
                billItemAttachment = BillItemAttachment(IdentityMap.extract(billItemAttachment.getURI()));
                
                if(billItemAttachment.IsLoaded || billItemAttachment.IsLoading)
                  return billItemAttachment;
      
              } 
              else
               IdentityMap.add(billItemAttachment);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                billItemAttachment.BillItemAttachmentId),null,billItemAttachment);
            
              return billItemAttachment;
          }
          
      
          public function findByPrimaryKey(  billItemAttachmentId:int):BillItemAttachment
          {
          
            var activeRecord:BillItemAttachment = new BillItemAttachment();
      
            
              activeRecord.BillItemAttachmentId = billItemAttachmentId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:BillItemAttachment = BillItemAttachment(activeRecord);
                   
        
         }
        }
      }
    