
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _UserRole extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _userRoleId: int;
      
        // parent tables
        internal var _parentRole: Role
            = new Role()
        ;
      
        // parent tables
        internal var _parentUser: User
            = new User()
        ;
      
            public function get  UserRoleId(): int
            {
              return _userRoleId;
            }

            public function set  UserRoleId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _userRoleId = value;
            }
          
            public function get  UserId(): int
            {
            

                  if(_parentUser != null)
                  return _parentUser.UserId;

                
            
            
            return undefined;
            }
            public function set UserId(value:int):void
            {

            

                  if(_parentUser == null)
                      _parentUser = new User();

                  _parentUser.UserId = value;
                
            
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
      
        
        public function get ParentUser():User
        {
        if(IsLoaded  && 
        
        !(_parentUser.IsLoaded || _parentUser.IsLoading))
        {
          _parentUser = DataMapperRegistry.Instance.User.load(_parentUser);
          
          onParentChanged(_parentUser);
        }

        return _parentUser;
        }
        public function set ParentUser(value:User):void
        {
          _parentUser = User(IdentityMap.register( value ));

          onParentChanged(_parentUser);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(ParentRole != null)
              ParentRole.onChildChanged(this);
          
            
            if(ParentUser != null)
              ParentUser.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:UserRole = new UserRole();
          
          
            object.UserRoleId = this.UserRoleId;
          
            object.UserId = this.UserId;
          
            object.RoleId = this.RoleId;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        UserRoleId = object["UserRoleId"];
        UserId = object["UserId"];
        RoleId = object["RoleId"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.UserRole;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.UserRole." 
            
              + UserRoleId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    