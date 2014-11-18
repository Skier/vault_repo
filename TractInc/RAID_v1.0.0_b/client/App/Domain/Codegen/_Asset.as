
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Asset extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _assetId: int;
      
        protected var _chiefAssetId: int;
      
        protected var _businessName: String;
      
        protected var _firstName: String;
      
        protected var _middleName: String;
      
        protected var _lastName: String;
      
        protected var _sSN: String;
      
        protected var _deleted: Boolean;
      
        // parent tables
        internal var _relatedAssetType: AssetType
            = new AssetType()
        ;
      
            public function get  AssetId(): int
            {
              return _assetId;
            }

            public function set  AssetId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _assetId = value;
            }
          
            public function get  _Type(): String
            {
            

                  if(_relatedAssetType != null)
                  return _relatedAssetType._Type;

                
            
            
            return undefined;
            }
            protected function set _Type(value:String):void
            {

            

                  if(_relatedAssetType == null)
                      _relatedAssetType = new AssetType();

                  _relatedAssetType._Type = value;
                
            
            }
          
            public function get  ChiefAssetId(): int
            {
              return _chiefAssetId;
            }

            public function set  ChiefAssetId(value:int):void
            {
            
            _chiefAssetId = value;
            }
          
            public function get  BusinessName(): String
            {
              return _businessName;
            }

            public function set  BusinessName(value:String):void
            {
            
            _businessName = value;
            }
          
            public function get  FirstName(): String
            {
              return _firstName;
            }

            public function set  FirstName(value:String):void
            {
            
            _firstName = value;
            }
          
            public function get  MiddleName(): String
            {
              return _middleName;
            }

            public function set  MiddleName(value:String):void
            {
            
            _middleName = value;
            }
          
            public function get  LastName(): String
            {
              return _lastName;
            }

            public function set  LastName(value:String):void
            {
            
            _lastName = value;
            }
          
            public function get  SSN(): String
            {
              return _sSN;
            }

            public function set  SSN(value:String):void
            {
            
            _sSN = value;
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          
        
        public function get RelatedAssetType():AssetType
        {
        if(IsLoaded  && 
        
        !(_relatedAssetType.IsLoaded || _relatedAssetType.IsLoading))
        {
          _relatedAssetType = DataMapperRegistry.Instance.AssetType.load(_relatedAssetType);
          
          onParentChanged(_relatedAssetType);
        }

        return _relatedAssetType;
        }
        public function set RelatedAssetType(value:AssetType):void
        {
          _relatedAssetType = AssetType(IdentityMap.register( value ));

          onParentChanged(_relatedAssetType);
        }
      

            // one to many relation
            protected var _relatedBill:ActiveCollection;
            
            public function get RelatedBill():ActiveCollection
            {
              _relatedBill = onChildRelationRequest("relatedBill",_relatedBill);
              
              return _relatedBill;
            }
            
        

            // one to many relation
            protected var _relatedAssetAssignment:ActiveCollection;
            
            public function get RelatedAssetAssignment():ActiveCollection
            {
              _relatedAssetAssignment = onChildRelationRequest("relatedAssetAssignment",_relatedAssetAssignment);
              
              return _relatedAssetAssignment;
            }
            
        

            // one to many relation
            protected var _relatedUserAsset:ActiveCollection;
            
            public function get RelatedUserAsset():ActiveCollection
            {
              _relatedUserAsset = onChildRelationRequest("relatedUserAsset",_relatedUserAsset);
              
              return _relatedUserAsset;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedAssetType != null)
              RelatedAssetType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Asset = new Asset();
          
          
            object.AssetId = this.AssetId;
          
            object._Type = this._Type;
          
            object.ChiefAssetId = this.ChiefAssetId;
          
            object.BusinessName = this.BusinessName;
          
            object.FirstName = this.FirstName;
          
            object.MiddleName = this.MiddleName;
          
            object.LastName = this.LastName;
          
            object.SSN = this.SSN;
          
            object.Deleted = this.Deleted;
          
            if(cascade)
            {
              
                    
                      for each(var bill :Bill in _relatedBill)
                      {
                        if(bill.IsDirty)
                        {
                           var billExtract:Object = bill.extractRelevant(true);
                               billExtract.RelatedAsset = object;

                        object.RelatedBill.addItem(billExtract);
                        }
                    }
                  
                    
                      for each(var assetAssignment :AssetAssignment in _relatedAssetAssignment)
                      {
                        if(assetAssignment.IsDirty)
                        {
                           var assetAssignmentExtract:Object = assetAssignment.extractRelevant(true);
                               assetAssignmentExtract.RelatedAsset = object;

                        object.RelatedAssetAssignment.addItem(assetAssignmentExtract);
                        }
                    }
                  
                    
                      for each(var userAsset :UserAsset in _relatedUserAsset)
                      {
                        if(userAsset.IsDirty)
                        {
                           var userAssetExtract:Object = userAsset.extractRelevant(true);
                               userAssetExtract.RelatedAsset = object;

                        object.RelatedUserAsset.addItem(userAssetExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedBill"])
                {
                  for each(var bill :ActiveRecord in this["relatedBill"] as Array)
                    childs.push(bill);
                }
              
                if(this["relatedAssetAssignment"])
                {
                  for each(var assetAssignment :ActiveRecord in this["relatedAssetAssignment"] as Array)
                    childs.push(assetAssignment);
                }
              
                if(this["relatedUserAsset"])
                {
                  for each(var userAsset :ActiveRecord in this["relatedUserAsset"] as Array)
                    childs.push(userAsset);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  AssetId = object["AssetId"];
              ChiefAssetId = object["ChiefAssetId"];
              BusinessName = object["BusinessName"];
              FirstName = object["FirstName"];
              MiddleName = object["MiddleName"];
              LastName = object["LastName"];
              SSN = object["SSN"];
              Deleted = object["Deleted"];
              RelatedAssetType =
          object.RelatedAssetType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Asset;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.Asset." 
            
              + AssetId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    