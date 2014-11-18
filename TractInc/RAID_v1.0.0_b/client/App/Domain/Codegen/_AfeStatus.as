
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _AfeStatus extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _aFEStatus: String;
      
            public function get  AFEStatus(): String
            {
              return _aFEStatus;
            }

            public function set  AFEStatus(value:String):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _aFEStatus = value;
            }
          

            // one to many relation
            protected var _relatedAfe:ActiveCollection;
            
            public function get RelatedAfe():ActiveCollection
            {
              _relatedAfe = onChildRelationRequest("relatedAfe",_relatedAfe);
              
              return _relatedAfe;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:AfeStatus = new AfeStatus();
          
          
            object.AFEStatus = this.AFEStatus;
          
            if(cascade)
            {
              
                    
                      for each(var afe :Afe in _relatedAfe)
                      {
                        if(afe.IsDirty)
                        {
                           var afeExtract:Object = afe.extractRelevant(true);
                               afeExtract.RelatedAfeStatus = object;

                        object.RelatedAfe.addItem(afeExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedAfe"])
                {
                  for each(var afe :ActiveRecord in this["relatedAfe"] as Array)
                    childs.push(afe);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  AFEStatus = object["AFEStatus"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.AfeStatus;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.AfeStatus." 
            
              + AFEStatus.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    