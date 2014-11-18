
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

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
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
            protected var _relatedPermissionAssignment:ActiveCollection;
            
            public function get RelatedPermissionAssignment():ActiveCollection
            {
              _relatedPermissionAssignment = onChildRelationRequest("relatedPermissionAssignment",_relatedPermissionAssignment);
              
              return _relatedPermissionAssignment;
            }
            
        

            // one to many relation
            protected var _relatedUserRole:ActiveCollection;
            
            public function get RelatedUserRole():ActiveCollection
            {
              _relatedUserRole = onChildRelationRequest("relatedUserRole",_relatedUserRole);
              
              return _relatedUserRole;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Role = new Role();
          
          
            object.RoleId = this.RoleId;
          
            object.Name = this.Name;
          
            if(cascade)
            {
              
                    
                      for each(var permissionAssignment :PermissionAssignment in _relatedPermissionAssignment)
                      {
                        if(permissionAssignment.IsDirty)
                        {
                           var permissionAssignmentExtract:Object = permissionAssignment.extractRelevant(true);
                               permissionAssignmentExtract.RelatedRole = object;

                        object.RelatedPermissionAssignment.addItem(permissionAssignmentExtract);
                        }
                    }
                  
                    
                      for each(var userRole :UserRole in _relatedUserRole)
                      {
                        if(userRole.IsDirty)
                        {
                           var userRoleExtract:Object = userRole.extractRelevant(true);
                               userRoleExtract.RelatedRole = object;

                        object.RelatedUserRole.addItem(userRoleExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedPermissionAssignment"])
                {
                  for each(var permissionAssignment :ActiveRecord in this["relatedPermissionAssignment"] as Array)
                    childs.push(permissionAssignment);
                }
              
                if(this["relatedUserRole"])
                {
                  for each(var userRole :ActiveRecord in this["relatedUserRole"] as Array)
                    childs.push(userRole);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
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
           _uri = "TractIncRAID.Role." 
            
              + RoleId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    