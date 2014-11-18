
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
        internal var _parentPermission: Permission
            = new Permission()
        ;
      
        // parent tables
        internal var _parentRole: Role
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
            
            _permissionAssignmentId = value;
            }
          
            public function get  PermissionId(): int
            {
            

                  if(_parentPermission != null)
                  return _parentPermission.PermissionId;

                
            
            
            return undefined;
            }
            public function set PermissionId(value:int):void
            {

            

                  if(_parentPermission == null)
                      _parentPermission = new Permission();

                  _parentPermission.PermissionId = value;
                
            
            }
          
            public function get  RoleId(): int
            {
            

                  if(_parentRole != null)
                  return _parentRole.RoleId;

                
            
            
            return undefined;
            }
            public function set RoleId(value:int):void
            {

            

                  if(_parentRole == null)
                      _parentRole = new Role();

                  _parentRole.RoleId = value;
                
            
            }
          
        
        public function get ParentPermission():Permission
        {
        if(IsLoaded  && 
        
        !(_parentPermission.IsLoaded || _parentPermission.IsLoading))
        {
          _parentPermission = DataMapperRegistry.Instance.Permission.load(_parentPermission);
          
          onParentChanged(_parentPermission);
        }

        return _parentPermission;
        }
        public function set ParentPermission(value:Permission):void
        {
          _parentPermission = Permission(IdentityMap.register( value ));

          onParentChanged(_parentPermission);
        }
      
        
        public function get ParentRole():Role
        {
        if(IsLoaded  && 
        
        !(_parentRole.IsLoaded || _parentRole.IsLoading))
        {
          _parentRole = DataMapperRegistry.Instance.Role.load(_parentRole);
          
          onParentChanged(_parentRole);
        }

        return _parentRole;
        }
        public function set ParentRole(value:Role):void
        {
          _parentRole = Role(IdentityMap.register( value ));

          onParentChanged(_parentRole);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(ParentPermission != null)
              ParentPermission.onChildChanged(this);
          
            
            if(ParentRole != null)
              ParentRole.onChildChanged(this);
          
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
        PermissionAssignmentId = object["PermissionAssignmentId"];
        PermissionId = object["PermissionId"];
        RoleId = object["RoleId"];
        
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
           _uri = "TractInc.PermissionAssignment." 
            
              + PermissionAssignmentId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    