
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Afe extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _aFE: String;
      
        protected var _aFEName: String;
      
        protected var _deleted: Boolean;
      
        // parent tables
        internal var _relatedAfeStatus: AfeStatus
            = new AfeStatus()
        ;
      
        // parent tables
        internal var _relatedClient: Client
            = new Client()
        ;
      
            public function get  AFE(): String
            {
              return _aFE;
            }

            public function set  AFE(value:String):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _aFE = value;
            }
          
            public function get  ClientId(): int
            {
            

                  if(_relatedClient != null)
                  return _relatedClient.ClientId;

                
            
            
            return undefined;
            }
            protected function set ClientId(value:int):void
            {

            

                  if(_relatedClient == null)
                      _relatedClient = new Client();

                  _relatedClient.ClientId = value;
                
            
            }
          
            public function get  AFEName(): String
            {
              return _aFEName;
            }

            public function set  AFEName(value:String):void
            {
            
            _aFEName = value;
            }
          
            public function get  AFEStatus(): String
            {
            

                  if(_relatedAfeStatus != null)
                  return _relatedAfeStatus.AFEStatus;

                
            
            
            return undefined;
            }
            protected function set AFEStatus(value:String):void
            {

            

                  if(_relatedAfeStatus == null)
                      _relatedAfeStatus = new AfeStatus();

                  _relatedAfeStatus.AFEStatus = value;
                
            
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          
        
        public function get RelatedAfeStatus():AfeStatus
        {
        if(IsLoaded  && 
        
        !(_relatedAfeStatus.IsLoaded || _relatedAfeStatus.IsLoading))
        {
          _relatedAfeStatus = DataMapperRegistry.Instance.AfeStatus.load(_relatedAfeStatus);
          
          onParentChanged(_relatedAfeStatus);
        }

        return _relatedAfeStatus;
        }
        public function set RelatedAfeStatus(value:AfeStatus):void
        {
          _relatedAfeStatus = AfeStatus(IdentityMap.register( value ));

          onParentChanged(_relatedAfeStatus);
        }
      
        
        public function get RelatedClient():Client
        {
        if(IsLoaded  && 
        
        !(_relatedClient.IsLoaded || _relatedClient.IsLoading))
        {
          _relatedClient = DataMapperRegistry.Instance.Client.load(_relatedClient);
          
          onParentChanged(_relatedClient);
        }

        return _relatedClient;
        }
        public function set RelatedClient(value:Client):void
        {
          _relatedClient = Client(IdentityMap.register( value ));

          onParentChanged(_relatedClient);
        }
      

            // one to many relation
            protected var _relatedSubAfe:ActiveCollection;
            
            public function get RelatedSubAfe():ActiveCollection
            {
              _relatedSubAfe = onChildRelationRequest("relatedSubAfe",_relatedSubAfe);
              
              return _relatedSubAfe;
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
          
            
            if(RelatedAfeStatus != null)
              RelatedAfeStatus.onChildChanged(this);
          
            
            if(RelatedClient != null)
              RelatedClient.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Afe = new Afe();
          
          
            object.AFE = this.AFE;
          
            object.ClientId = this.ClientId;
          
            object.AFEName = this.AFEName;
          
            object.AFEStatus = this.AFEStatus;
          
            object.Deleted = this.Deleted;
          
            if(cascade)
            {
              
                    
                      for each(var subAfe :SubAfe in _relatedSubAfe)
                      {
                        if(subAfe.IsDirty)
                        {
                           var subAfeExtract:Object = subAfe.extractRelevant(true);
                               subAfeExtract.RelatedAfe = object;

                        object.RelatedSubAfe.addItem(subAfeExtract);
                        }
                    }
                  
                    
                      for each(var assetAssignment :AssetAssignment in _relatedAssetAssignment)
                      {
                        if(assetAssignment.IsDirty)
                        {
                           var assetAssignmentExtract:Object = assetAssignment.extractRelevant(true);
                               assetAssignmentExtract.RelatedAfe = object;

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

          
                if(this["relatedSubAfe"])
                {
                  for each(var subAfe :ActiveRecord in this["relatedSubAfe"] as Array)
                    childs.push(subAfe);
                }
              
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
                  AFE = object["AFE"];
              AFEName = object["AFEName"];
              Deleted = object["Deleted"];
              RelatedAfeStatus =
          object.RelatedAfeStatus;
        RelatedClient =
          object.RelatedClient;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Afe;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.Afe." 
            
              + AFE.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    