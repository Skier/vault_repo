
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _SubAfe extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _subAFE: String;
      
        protected var _shortName: String;
      
        protected var _deleted: Boolean;
      
        protected var _temporary: Boolean;
      
        // parent tables
        internal var _relatedAfe: Afe
            = new Afe()
        ;
      
        // parent tables
        internal var _relatedSubAfeStatus: SubAfeStatus
            = new SubAfeStatus()
        ;
      
            public function get  SubAFE(): String
            {
              return _subAFE;
            }

            public function set  SubAFE(value:String):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _subAFE = value;
            }
          
            public function get  AFE(): String
            {
            

                  if(_relatedAfe != null)
                  return _relatedAfe.AFE;

                
            
            
            return undefined;
            }
            protected function set AFE(value:String):void
            {

            

                  if(_relatedAfe == null)
                      _relatedAfe = new Afe();

                  _relatedAfe.AFE = value;
                
            
            }
          
            public function get  SubAFEStatus(): String
            {
            

                  if(_relatedSubAfeStatus != null)
                  return _relatedSubAfeStatus.SubAFEStatus;

                
            
            
            return undefined;
            }
            protected function set SubAFEStatus(value:String):void
            {

            

                  if(_relatedSubAfeStatus == null)
                      _relatedSubAfeStatus = new SubAfeStatus();

                  _relatedSubAfeStatus.SubAFEStatus = value;
                
            
            }
          
            public function get  ShortName(): String
            {
              return _shortName;
            }

            public function set  ShortName(value:String):void
            {
            
            _shortName = value;
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          
            public function get  Temporary(): Boolean
            {
              return _temporary;
            }

            public function set  Temporary(value:Boolean):void
            {
            
            _temporary = value;
            }
          
        
        public function get RelatedAfe():Afe
        {
        if(IsLoaded  && 
        
        !(_relatedAfe.IsLoaded || _relatedAfe.IsLoading))
        {
          _relatedAfe = DataMapperRegistry.Instance.Afe.load(_relatedAfe);
          
          onParentChanged(_relatedAfe);
        }

        return _relatedAfe;
        }
        public function set RelatedAfe(value:Afe):void
        {
          _relatedAfe = Afe(IdentityMap.register( value ));

          onParentChanged(_relatedAfe);
        }
      
        
        public function get RelatedSubAfeStatus():SubAfeStatus
        {
        if(IsLoaded  && 
        
        !(_relatedSubAfeStatus.IsLoaded || _relatedSubAfeStatus.IsLoading))
        {
          _relatedSubAfeStatus = DataMapperRegistry.Instance.SubAfeStatus.load(_relatedSubAfeStatus);
          
          onParentChanged(_relatedSubAfeStatus);
        }

        return _relatedSubAfeStatus;
        }
        public function set RelatedSubAfeStatus(value:SubAfeStatus):void
        {
          _relatedSubAfeStatus = SubAfeStatus(IdentityMap.register( value ));

          onParentChanged(_relatedSubAfeStatus);
        }
      

            // one to many relation
            protected var _relatedAssetAssignment:ActiveCollection;
            
            public function get RelatedAssetAssignment():ActiveCollection
            {
              _relatedAssetAssignment = onChildRelationRequest("relatedAssetAssignment",_relatedAssetAssignment);
              
              return _relatedAssetAssignment;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedAfe != null)
              RelatedAfe.onChildChanged(this);
          
            
            if(RelatedSubAfeStatus != null)
              RelatedSubAfeStatus.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:SubAfe = new SubAfe();
          
          
            object.SubAFE = this.SubAFE;
          
            object.AFE = this.AFE;
          
            object.SubAFEStatus = this.SubAFEStatus;
          
            object.ShortName = this.ShortName;
          
            object.Deleted = this.Deleted;
          
            object.Temporary = this.Temporary;
          
            if(cascade)
            {
              
                    
                      for each(var assetAssignment :AssetAssignment in _relatedAssetAssignment)
                      {
                        if(assetAssignment.IsDirty)
                        {
                           var assetAssignmentExtract:Object = assetAssignment.extractRelevant(true);
                               assetAssignmentExtract.RelatedSubAfe = object;

                        object.RelatedAssetAssignment.addItem(assetAssignmentExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedAssetAssignment"])
                {
                  for each(var assetAssignment :ActiveRecord in this["relatedAssetAssignment"] as Array)
                    childs.push(assetAssignment);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  SubAFE = object["SubAFE"];
              ShortName = object["ShortName"];
              Deleted = object["Deleted"];
              Temporary = object["Temporary"];
              RelatedAfe =
          object.RelatedAfe;
        RelatedSubAfeStatus =
          object.RelatedSubAfeStatus;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.SubAfe;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.SubAfe." 
            
              + SubAFE.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    