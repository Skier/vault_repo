
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
        internal var _relatedRole: Role
            = new Role()
        ;
      
        // parent tables
        internal var _relatedUser: User
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

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _userRoleId = value;
            }
          
            public function get  UserId(): int
            {
            

                  if(_relatedUser != null)
                  return _relatedUser.UserId;

                
            
            
            return undefined;
            }
            protected function set UserId(value:int):void
            {

            

                  if(_relatedUser == null)
                      _relatedUser = new User();

                  _relatedUser.UserId = value;
                
            
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
      
        
        public function get RelatedUser():User
        {
        if(IsLoaded  && 
        
        !(_relatedUser.IsLoaded || _relatedUser.IsLoading))
        {
          _relatedUser = DataMapperRegistry.Instance.User.load(_relatedUser);
          
          onParentChanged(_relatedUser);
        }

        return _relatedUser;
        }
        public function set RelatedUser(value:User):void
        {
          _relatedUser = User(IdentityMap.register( value ));

          onParentChanged(_relatedUser);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedRole != null)
              RelatedRole.onChildChanged(this);
          
            
            if(RelatedUser != null)
              RelatedUser.onChildChanged(this);
          
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
        
                if(!IsPrimaryKeyInitialized)
                  UserRoleId = object["UserRoleId"];
              RelatedRole =
          object.RelatedRole;
        RelatedUser =
          object.RelatedUser;
        
      
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
           _uri = "TractIncRAID.UserRole." 
            
              + UserRoleId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    