
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Permission extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _permissionId: int;
      
        protected var _description: String;
      
        protected var _code: String;
      
        // parent tables
        internal var _parentModule: Module
            = new Module()
        ;
      
            public function get  PermissionId(): int
            {
              return _permissionId;
            }

            public function set  PermissionId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _permissionId = value;
            }
          
            public function get  ModuleId(): int
            {
            

                  if(_parentModule != null)
                  return _parentModule.ModuleId;

                
            
            
            return undefined;
            }
            public function set ModuleId(value:int):void
            {

            

                  if(_parentModule == null)
                      _parentModule = new Module();

                  _parentModule.ModuleId = value;
                
            
            }
          
            public function get  Description(): String
            {
              return _description;
            }

            public function set  Description(value:String):void
            {
            
            _description = value;
            }
          
            public function get  Code(): String
            {
              return _code;
            }

            public function set  Code(value:String):void
            {
            
            _code = value;
            }
          
        
        public function get ParentModule():Module
        {
        if(IsLoaded  && 
        
        !(_parentModule.IsLoaded || _parentModule.IsLoading))
        {
          _parentModule = DataMapperRegistry.Instance.Module.load(_parentModule);
          
          onParentChanged(_parentModule);
        }

        return _parentModule;
        }
        public function set ParentModule(value:Module):void
        {
          _parentModule = Module(IdentityMap.register( value ));

          onParentChanged(_parentModule);
        }
      

            // one to many relation
            protected var _permissionAssignments:ActiveCollection;
            
            public function get PermissionAssignments():ActiveCollection
            {
              _permissionAssignments = onChildRelationRequest("permissionAssignments",_permissionAssignments);
              
              return _permissionAssignments;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(ParentModule != null)
              ParentModule.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Permission = new Permission();
          
          
            object.PermissionId = this.PermissionId;
          
            object.ModuleId = this.ModuleId;
          
            object.Description = this.Description;
          
            object.Code = this.Code;
          
            if(cascade)
            {
              
                    
                      for each(var permissionAssignment :PermissionAssignment in _permissionAssignments)
                      {
                        if(permissionAssignment.IsDirty)
                        {
                           var permissionAssignmentExtract:Object = permissionAssignment.extractRelevant(true);
                               permissionAssignmentExtract.ParentPermission = object;

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

          
                if(this["permissionAssignments"])
                {
                  for each(var permissionAssignment :ActiveRecord in this["permissionAssignments"] as Array)
                    childs.push(permissionAssignment);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        PermissionId = object["PermissionId"];
        ModuleId = object["ModuleId"];
        Description = object["Description"];
        Code = object["Code"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Permission;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.Permission." 
            
              + PermissionId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    