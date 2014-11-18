
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Role extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _roleId: int;
      
        protected var _name: String;
      
            public function get  RoleId(): int
            {
              return _roleId;
            }

            public function set  RoleId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _roleId = value;
            }
          
            public function get  Name(): String
            {
              return _name;
            }

            public function set  Name(value:String):void
            {
            
            _name = value;
            }
          

            // one to many relation
            protected var _userRoles:ActiveCollection;
            
            public function get UserRoles():ActiveCollection
            {
              _userRoles = onChildRelationRequest("userRoles",_userRoles);
              
              return _userRoles;
            }
            
        

            // one to many relation
            protected var _permissionAssignments:ActiveCollection;
            
            public function get PermissionAssignments():ActiveCollection
            {
              _permissionAssignments = onChildRelationRequest("permissionAssignments",_permissionAssignments);
              
              return _permissionAssignments;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Role = new Role();
          
          
            object.RoleId = this.RoleId;
          
            object.Name = this.Name;
          
            if(cascade)
            {
              
                    
                      for each(var userRole :UserRole in _userRoles)
                      {
                        if(userRole.IsDirty)
                        {
                           var userRoleExtract:Object = userRole.extractRelevant(true);
                               userRoleExtract.ParentRole = object;

                        object.UserRoles.addItem(userRoleExtract);
                        }
                    }
                  
                    
                      for each(var permissionAssignment :PermissionAssignment in _permissionAssignments)
                      {
                        if(permissionAssignment.IsDirty)
                        {
                           var permissionAssignmentExtract:Object = permissionAssignment.extractRelevant(true);
                               permissionAssignmentExtract.ParentRole = object;

                        object.PermissionAssignments.addItem(permissionAssignmentExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["userRoles"])
                {
                  for each(var userRole :ActiveRecord in this["userRoles"] as Array)
                    childs.push(userRole);
                }
              
                if(this["permissionAssignments"])
                {
                  for each(var permissionAssignment :ActiveRecord in this["permissionAssignments"] as Array)
                    childs.push(permissionAssignment);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        RoleId = object["RoleId"];
        Name = object["Name"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Role;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.Role." 
            
              + RoleId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    