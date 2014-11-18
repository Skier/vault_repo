
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.User;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _UserDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new User();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.UserDataMapper";
          }
          
      		public function load(user:User, responder:Responder = null):User
          {
            
              if(!user.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(user.getURI()))
              {
                user = User(IdentityMap.extract(user.getURI()));
                
                if(user.IsLoaded || user.IsLoading)
                  return user;
      
              } 
              else
               IdentityMap.add(user);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                user.UserId),null,user);
            
              return user;
          }
          
      
          public function findByPrimaryKey(  userId:int):User
          {
          
            var activeRecord:User = new User();
      
            
              activeRecord.UserId = userId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:User = User(activeRecord);
                   
        
              
              if(relationName == "relatedNote")
              {
   
                DataMapperRegistry.Instance.Note.
                findBySenderId(
                
                  item.UserId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedMessage")
              {
   
                DataMapperRegistry.Instance.Message.
                findByReceiverUserIdAndSenderUserId(
                
                  item.UserId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedUserAsset")
              {
   
                DataMapperRegistry.Instance.UserAsset.
                findByUserId(
                
                  item.UserId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "relatedUserRole")
              {
   
                DataMapperRegistry.Instance.UserRole.
                findByUserId(
                
                  item.UserId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    