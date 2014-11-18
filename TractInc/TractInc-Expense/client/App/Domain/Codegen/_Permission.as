
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
        internal var _relatedModule: Module
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

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _permissionId = value;
            }
          
            public function get  ModuleId(): int
            {
            

                  if(_relatedModule != null)
                  return _relatedModule.ModuleId;

                
            
            
            return undefined;
            }
            protected function set ModuleId(value:int):void
            {

            

                  if(_relatedModule == null)
                      _relatedModule = new Module();

                  _relatedModule.ModuleId = value;
                
            
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
          
        
        public function get RelatedModule():Module
        {
        if(IsLoaded  && 
        
        !(_relatedModule.IsLoaded || _relatedModule.IsLoading))
        {
          _relatedModule = DataMapperRegistry.Instance.Module.load(_relatedModule);
          
          onParentChanged(_relatedModule);
        }

        return _relatedModule;
        }
        public function set RelatedModule(value:Module):void
        {
          _relatedModule = Module(IdentityMap.register( value ));

          onParentChanged(_relatedModule);
        }
      

            // one to many relation
            protected var _relatedPermissionAssignment:ActiveCollection;
            
            public function get RelatedPermissionAssignment():ActiveCollection
            {
              _relatedPermissionAssignment = onChildRelationRequest("relatedPermissionAssignment",_relatedPermissionAssignment);
              
              return _relatedPermissionAssignment;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedModule != null)
              RelatedModule.onChildChanged(this);
          
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
              
                    
                      for each(var permissionAssignment :PermissionAssignment in _relatedPermissionAssignment)
                      {
                        if(permissionAssignment.IsDirty)
                        {
                           var permissionAssignmentExtract:Object = permissionAssignment.extractRelevant(true);
                               permissionAssignmentExtract.RelatedPermission = object;

                        object.RelatedPermissionAssignment.addItem(permissionAssignmentExtract);
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
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  PermissionId = object["PermissionId"];
              Description = object["Description"];
              Code = object["Code"];
              RelatedModule =
          object.RelatedModule;
        
      
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
           _uri = "TractIncRAID.Permission." 
            
              + PermissionId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    