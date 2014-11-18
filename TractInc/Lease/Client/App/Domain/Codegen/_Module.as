
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Module extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _moduleId: int;
      
        protected var _description: String;
      
            public function get  ModuleId(): int
            {
              return _moduleId;
            }

            public function set  ModuleId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _moduleId = value;
            }
          
            public function get  Description(): String
            {
              return _description;
            }

            public function set  Description(value:String):void
            {
            
            _description = value;
            }
          

            // one to many relation
            protected var _permissions:ActiveCollection;
            
            public function get Permissions():ActiveCollection
            {
              _permissions = onChildRelationRequest("permissions",_permissions);
              
              return _permissions;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Module = new Module();
          
          
            object.ModuleId = this.ModuleId;
          
            object.Description = this.Description;
          
            if(cascade)
            {
              
                    
                      for each(var permission :Permission in _permissions)
                      {
                        if(permission.IsDirty)
                        {
                           var permissionExtract:Object = permission.extractRelevant(true);
                               permissionExtract.ParentModule = object;

                        object.Permissions.addItem(permissionExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["permissions"])
                {
                  for each(var permission :ActiveRecord in this["permissions"] as Array)
                    childs.push(permission);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        ModuleId = object["ModuleId"];
        Description = object["Description"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Module;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.Module." 
            
              + ModuleId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    