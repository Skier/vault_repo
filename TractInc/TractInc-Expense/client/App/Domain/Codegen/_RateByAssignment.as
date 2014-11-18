
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _RateByAssignment extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _rateByAssignmentId: int;
      
        protected var _billRate: Number = 0;
      
        protected var _invoiceRate: Number = 0;
      
        protected var _shouldNotExceedRate: Boolean;
      
        protected var _deleted: Boolean;
      
        // parent tables
        internal var _relatedAssetAssignment: AssetAssignment
            = new AssetAssignment()
        ;
      
        // parent tables
        internal var _relatedBillItemType: BillItemType
            = new BillItemType()
        ;
      
            public function get  RateByAssignmentId(): int
            {
              return _rateByAssignmentId;
            }

            public function set  RateByAssignmentId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _rateByAssignmentId = value;
            }
          
            public function get  AssetAssignmentId(): int
            {
            

                  if(_relatedAssetAssignment != null)
                  return _relatedAssetAssignment.AssetAssignmentId;

                
            
            
            return undefined;
            }
            protected function set AssetAssignmentId(value:int):void
            {

            

                  if(_relatedAssetAssignment == null)
                      _relatedAssetAssignment = new AssetAssignment();

                  _relatedAssetAssignment.AssetAssignmentId = value;
                
            
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
          
            public function get  InvoiceRate(): Number
            {
              return _invoiceRate;
            }

            public function set  InvoiceRate(value:Number):void
            {
            
            _invoiceRate = value;
            }
          
            public function get  ShouldNotExceedRate(): Boolean
            {
              return _shouldNotExceedRate;
            }

            public function set  ShouldNotExceedRate(value:Boolean):void
            {
            
            _shouldNotExceedRate = value;
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          
        
        public function get RelatedAssetAssignment():AssetAssignment
        {
        if(IsLoaded  && 
        
        !(_relatedAssetAssignment.IsLoaded || _relatedAssetAssignment.IsLoading))
        {
          _relatedAssetAssignment = DataMapperRegistry.Instance.AssetAssignment.load(_relatedAssetAssignment);
          
          onParentChanged(_relatedAssetAssignment);
        }

        return _relatedAssetAssignment;
        }
        public function set RelatedAssetAssignment(value:AssetAssignment):void
        {
          _relatedAssetAssignment = AssetAssignment(IdentityMap.register( value ));

          onParentChanged(_relatedAssetAssignment);
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
          
            
            if(RelatedAssetAssignment != null)
              RelatedAssetAssignment.onChildChanged(this);
          
            
            if(RelatedBillItemType != null)
              RelatedBillItemType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:RateByAssignment = new RateByAssignment();
          
          
            object.RateByAssignmentId = this.RateByAssignmentId;
          
            object.AssetAssignmentId = this.AssetAssignmentId;
          
            object.BillItemTypeId = this.BillItemTypeId;
          
            object.BillRate = this.BillRate;
          
            object.InvoiceRate = this.InvoiceRate;
          
            object.ShouldNotExceedRate = this.ShouldNotExceedRate;
          
            object.Deleted = this.Deleted;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  RateByAssignmentId = object["RateByAssignmentId"];
              BillRate = object["BillRate"];
              InvoiceRate = object["InvoiceRate"];
              ShouldNotExceedRate = object["ShouldNotExceedRate"];
              Deleted = object["Deleted"];
              RelatedAssetAssignment =
          object.RelatedAssetAssignment;
        RelatedBillItemType =
          object.RelatedBillItemType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.RateByAssignment;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.RateByAssignment." 
            
              + RateByAssignmentId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    