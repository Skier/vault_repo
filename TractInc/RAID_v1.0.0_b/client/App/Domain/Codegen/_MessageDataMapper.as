
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Message;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _MessageDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Message();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.MessageDataMapper";
          }
          
      		public function load(message:Message, responder:Responder = null):Message
          {
            
              if(!message.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(message.getURI()))
              {
                message = Message(IdentityMap.extract(message.getURI()));
                
                if(message.IsLoaded || message.IsLoading)
                  return message;
      
              } 
              else
               IdentityMap.add(message);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                message.MessageId),null,message);
            
              return message;
          }
          
      
          public function findByPrimaryKey(  messageId:int):Message
          {
          
            var activeRecord:Message = new Message();
      
            
              activeRecord.MessageId = messageId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Message = Message(activeRecord);
                   
        
         }
        }
      }
    