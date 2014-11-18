
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.PermissionAssignment;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _PermissionAssignmentDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new PermissionAssignment();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.PermissionAssignmentDataMapper";
          }
          
      		public function load(permissionAssignment:PermissionAssignment, responder:Responder = null):PermissionAssignment
          {
            
              if(!permissionAssignment.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(permissionAssignment.getURI()))
              {
                permissionAssignment = PermissionAssignment(IdentityMap.extract(permissionAssignment.getURI()));
                
                if(permissionAssignment.IsLoaded)
                  return permissionAssignment;
      
              } 
              else
               IdentityMap.add(permissionAssignment);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                permissionAssignment.PermissionAssignmentId),null,permissionAssignment);
            
              return permissionAssignment;
          }
          
      
          public function findByPrimaryKey(  permissionAssignmentId:int):PermissionAssignment
          {
            var activeRecord:PermissionAssignment = new PermissionAssignment();

            
              activeRecord.PermissionAssignmentId = permissionAssignmentId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:PermissionAssignment = PermissionAssignment(activeRecord);
                   
        
         }
        }
      }
    