
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.UserRole;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _UserRoleDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new UserRole();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.UserRoleDataMapper";
          }
          
      		public function load(userRole:UserRole, responder:Responder = null):UserRole
          {
            
              if(!userRole.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(userRole.getURI()))
              {
                userRole = UserRole(IdentityMap.extract(userRole.getURI()));
                
                if(userRole.IsLoaded)
                  return userRole;
      
              } 
              else
               IdentityMap.add(userRole);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                userRole.UserRoleId),null,userRole);
            
              return userRole;
          }
          
      
          public function findByPrimaryKey(  userRoleId:int):UserRole
          {
            var activeRecord:UserRole = new UserRole();

            
              activeRecord.UserRoleId = userRoleId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:UserRole = UserRole(activeRecord);
                   
        
         }
        }
      }
    