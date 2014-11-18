
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

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
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
            protected var _relatedPermission:ActiveCollection;
            
            public function get RelatedPermission():ActiveCollection
            {
              _relatedPermission = onChildRelationRequest("relatedPermission",_relatedPermission);
              
              return _relatedPermission;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Module = new Module();
          
          
            object.ModuleId = this.ModuleId;
          
            object.Description = this.Description;
          
            if(cascade)
            {
              
                    
                      for each(var permission :Permission in _relatedPermission)
                      {
                        if(permission.IsDirty)
                        {
                           var permissionExtract:Object = permission.extractRelevant(true);
                               permissionExtract.RelatedModule = object;

                        object.RelatedPermission.addItem(permissionExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedPermission"])
                {
                  for each(var permission :ActiveRecord in this["relatedPermission"] as Array)
                    childs.push(permission);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
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
           _uri = "TractIncRAID.Module." 
            
              + ModuleId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    