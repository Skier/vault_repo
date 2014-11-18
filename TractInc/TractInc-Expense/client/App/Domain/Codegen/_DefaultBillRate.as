
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _DefaultBillRate extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _defaultBillRateId: int;
      
        protected var _billRate: Number = 0;
      
        // parent tables
        internal var _relatedAsset: Asset
            = new Asset()
        ;
      
        // parent tables
        internal var _relatedBillItemType: BillItemType
            = new BillItemType()
        ;
      
            public function get  DefaultBillRateId(): int
            {
              return _defaultBillRateId;
            }

            public function set  DefaultBillRateId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _defaultBillRateId = value;
            }
          
            public function get  AssetId(): int
            {
            

                  if(_relatedAsset != null)
                  return _relatedAsset.AssetId;

                
            
            
            return undefined;
            }
            protected function set AssetId(value:int):void
            {

            

                  if(_relatedAsset == null)
                      _relatedAsset = new Asset();

                  _relatedAsset.AssetId = value;
                
            
            }
          
            public function get  BillItemTypeId(): int
            {
            

                  if(_relatedBillItemType != null)
                  return _relatedBillItemType.BillItemTypeId;

                
            
            
            return undefined;
            }
            protected function set BillItemTypeId(value:int):void
            {

            

                  if(_relatedBillItemType == null)
                      _relatedBillItemType = new BillItemType();

                  _relatedBillItemType.BillItemTypeId = value;
                
            
            }
          
            public function get  BillRate(): Number
            {
              return _billRate;
            }

            public function set  BillRate(value:Number):void
            {
            
            _billRate = value;
            }
          
        
        public function get RelatedAsset():Asset
        {
        if(IsLoaded  && 
        
        !(_relatedAsset.IsLoaded || _relatedAsset.IsLoading))
        {
          _relatedAsset = DataMapperRegistry.Instance.Asset.load(_relatedAsset);
          
          onParentChanged(_relatedAsset);
        }

        return _relatedAsset;
        }
        public function set RelatedAsset(value:Asset):void
        {
          _relatedAsset = Asset(IdentityMap.register( value ));

          onParentChanged(_relatedAsset);
        }
      
        
        public function get RelatedBillItemType():BillItemType
        {
        if(IsLoaded  && 
        
        !(_relatedBillItemType.IsLoaded || _relatedBillItemType.IsLoading))
        {
          _relatedBillItemType = DataMapperRegistry.Instance.BillItemType.load(_relatedBillItemType);
          
          onParentChanged(_relatedBillItemType);
        }

        return _relatedBillItemType;
        }
        public function set RelatedBillItemType(value:BillItemType):void
        {
          _relatedBillItemType = BillItemType(IdentityMap.register( value ));

          onParentChanged(_relatedBillItemType);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedAsset != null)
              RelatedAsset.onChildChanged(this);
          
            
            if(RelatedBillItemType != null)
              RelatedBillItemType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:DefaultBillRate = new DefaultBillRate();
          
          
            object.DefaultBillRateId = this.DefaultBillRateId;
          
            object.AssetId = this.AssetId;
          
            object.BillItemTypeId = this.BillItemTypeId;
          
            object.BillRate = this.BillRate;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  DefaultBillRateId = object["DefaultBillRateId"];
              BillRate = object["BillRate"];
              RelatedAsset =
          object.RelatedAsset;
        RelatedBillItemType =
          object.RelatedBillItemType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.DefaultBillRate;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.DefaultBillRate." 
            
              + DefaultBillRateId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    