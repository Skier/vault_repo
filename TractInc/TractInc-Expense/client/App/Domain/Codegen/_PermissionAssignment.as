
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _PermissionAssignment extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _permissionAssignmentId: int;
      
        // parent tables
        internal var _relatedPermission: Permission
            = new Permission()
        ;
      
        // parent tables
        internal var _relatedRole: Role
            = new Role()
        ;
      
            public function get  PermissionAssignmentId(): int
            {
              return _permissionAssignmentId;
            }

            public function set  PermissionAssignmentId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _permissionAssignmentId = value;
            }
          
            public function get  PermissionId(): int
            {
            

                  if(_relatedPermission != null)
                  return _relatedPermission.PermissionId;

                
            
            
            return undefined;
            }
            protected function set PermissionId(value:int):void
            {

            

                  if(_relatedPermission == null)
                      _relatedPermission = new Permission();

                  _relatedPermission.PermissionId = value;
                
            
            }
          
            public function get  RoleId(): int
            {
            

                  if(_relatedRole != null)
                  return _relatedRole.RoleId;

                
            
            
            return undefined;
            }
            protected function set RoleId(value:int):void
            {

            

                  if(_relatedRole == null)
                      _relatedRole = new Role();

                  _relatedRole.RoleId = value;
                
            
            }
          
        
        public function get RelatedPermission():Permission
        {
        if(IsLoaded  && 
        
        !(_relatedPermission.IsLoaded || _relatedPermission.IsLoading))
        {
          _relatedPermission = DataMapperRegistry.Instance.Permission.load(_relatedPermission);
          
          onParentChanged(_relatedPermission);
        }

        return _relatedPermission;
        }
        public function set RelatedPermission(value:Permission):void
        {
          _relatedPermission = Permission(IdentityMap.register( value ));

          onParentChanged(_relatedPermission);
        }
      
        
        public function get RelatedRole():Role
        {
        if(IsLoaded  && 
        
        !(_relatedRole.IsLoaded || _relatedRole.IsLoading))
        {
          _relatedRole = DataMapperRegistry.Instance.Role.load(_relatedRole);
          
          onParentChanged(_relatedRole);
        }

        return _relatedRole;
        }
        public function set RelatedRole(value:Role):void
        {
          _relatedRole = Role(IdentityMap.register( value ));

          onParentChanged(_relatedRole);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedPermission != null)
              RelatedPermission.onChildChanged(this);
          
            
            if(RelatedRole != null)
              RelatedRole.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:PermissionAssignment = new PermissionAssignment();
          
          
            object.PermissionAssignmentId = this.PermissionAssignmentId;
          
            object.PermissionId = this.PermissionId;
          
            object.RoleId = this.RoleId;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  PermissionAssignmentId = object["PermissionAssignmentId"];
              RelatedPermission =
          object.RelatedPermission;
        RelatedRole =
          object.RelatedRole;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.PermissionAssignment;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.PermissionAssignment." 
            
              + PermissionAssignmentId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    