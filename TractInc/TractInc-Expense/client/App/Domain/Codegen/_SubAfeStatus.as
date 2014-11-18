
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _SubAfeStatus extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _subAFEStatus: String;
      
            public function get  SubAFEStatus(): String
            {
              return _subAFEStatus;
            }

            public function set  SubAFEStatus(value:String):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _subAFEStatus = value;
            }
          

            // one to many relation
            protected var _relatedSubAfe:ActiveCollection;
            
            public function get RelatedSubAfe():ActiveCollection
            {
              _relatedSubAfe = onChildRelationRequest("relatedSubAfe",_relatedSubAfe);
              
              return _relatedSubAfe;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:SubAfeStatus = new SubAfeStatus();
          
          
            object.SubAFEStatus = this.SubAFEStatus;
          
            if(cascade)
            {
              
                    
                      for each(var subAfe :SubAfe in _relatedSubAfe)
                      {
                        if(subAfe.IsDirty)
                        {
                           var subAfeExtract:Object = subAfe.extractRelevant(true);
                               subAfeExtract.RelatedSubAfeStatus = object;

                        object.RelatedSubAfe.addItem(subAfeExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedSubAfe"])
                {
                  for each(var subAfe :ActiveRecord in this["relatedSubAfe"] as Array)
                    childs.push(subAfe);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  SubAFEStatus = object["SubAFEStatus"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.SubAfeStatus;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.SubAfeStatus." 
            
              + SubAFEStatus.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    