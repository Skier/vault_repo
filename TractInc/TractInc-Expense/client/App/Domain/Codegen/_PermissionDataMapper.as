
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Permission;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _PermissionDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Permission();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.PermissionDataMapper";
          }
          
      		public function load(permission:Permission, responder:Responder = null):Permission
          {
            
              if(!permission.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(permission.getURI()))
              {
                permission = Permission(IdentityMap.extract(permission.getURI()));
                
                if(permission.IsLoaded || permission.IsLoading)
                  return permission;
      
              } 
              else
               IdentityMap.add(permission);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                permission.PermissionId),null,permission);
            
              return permission;
          }
          
      
          public function findByPrimaryKey(  permissionId:int):Permission
          {
          
            var activeRecord:Permission = new Permission();
      
            
              activeRecord.PermissionId = permissionId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Permission = Permission(activeRecord);
                   
        
              
              if(relationName == "relatedPermissionAssignment")
              {
   
                DataMapperRegistry.Instance.PermissionAssignment.
                findByPermissionId(
                
                  item.PermissionId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    