
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Role;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _RoleDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Role();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.RoleDataMapper";
          }
          
      		public function load(role:Role, responder:Responder = null):Role
          {
            
              if(!role.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(role.getURI()))
              {
                role = Role(IdentityMap.extract(role.getURI()));
                
                if(role.IsLoaded)
                  return role;
      
              } 
              else
               IdentityMap.add(role);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                role.RoleId),null,role);
            
              return role;
          }
          
      
          public function findByPrimaryKey(  roleId:int):Role
          {
            var activeRecord:Role = new Role();

            
              activeRecord.RoleId = roleId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:Role = Role(activeRecord);
                   
        
              
              if(relationName == "userRoles")
              {
   
                DataMapperRegistry.Instance.UserRole.
                findByRoleId(
                
                  item.RoleId, activeCollection)
              ;
                
                return;
              }
            
              
              if(relationName == "permissionAssignments")
              {
   
                DataMapperRegistry.Instance.PermissionAssignment.
                findByRoleId(
                
                  item.RoleId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    