
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _InvoiceItem extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _invoiceItemId: int;
      
        protected var _invoiceDate: String;
      
        protected var _qty: int;
      
        protected var _invoiceRate: Number = 0;
      
        protected var _notes: String;
      
        protected var _isSelected: Boolean;
      
        // parent tables
        internal var _relatedAssetAssignment: AssetAssignment
            = new AssetAssignment()
        ;
      
        // parent tables
        internal var _relatedBillItem: BillItem;
      
        // parent tables
        internal var _relatedInvoice: Invoice
            = new Invoice()
        ;
      
        // parent tables
        internal var _relatedInvoiceItemStatus: InvoiceItemStatus
            = new InvoiceItemStatus()
        ;
      
        // parent tables
        internal var _relatedInvoiceItemType: InvoiceItemType
            = new InvoiceItemType()
        ;
      
            public function get  InvoiceItemId(): int
            {
              return _invoiceItemId;
            }

            public function set  InvoiceItemId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _invoiceItemId = value;
            }
          
            public function get  InvoiceItemTypeId(): int
            {
            

                  if(_relatedInvoiceItemType != null)
                  return _relatedInvoiceItemType.InvoiceItemTypeId;

                
            
            
            return undefined;
            }
            protected function set InvoiceItemTypeId(value:int):void
            {

            

                  if(_relatedInvoiceItemType == null)
                      _relatedInvoiceItemType = new InvoiceItemType();

                  _relatedInvoiceItemType.InvoiceItemTypeId = value;
                
            
            }
          
            public function get  InvoiceId(): int
            {
            

                  if(_relatedInvoice != null)
                  return _relatedInvoice.InvoiceId;

                
            
            
            return undefined;
            }
            protected function set InvoiceId(value:int):void
            {

            

                  if(_relatedInvoice == null)
                      _relatedInvoice = new Invoice();

                  _relatedInvoice.InvoiceId = value;
                
            
            }
          
            public function get  BillItemId(): int
            {
            

                  if(_relatedBillItem != null)
                  return _relatedBillItem.BillItemId;

                
            
            
            return undefined;
            }
            protected function set BillItemId(value:int):void
            {

            

                  if(_relatedBillItem == null)
                      _relatedBillItem = new BillItem();

                  _relatedBillItem.BillItemId = value;
                
            
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
          
            public function get  InvoiceDate(): String
            {
              return _invoiceDate;
            }

            public function set  InvoiceDate(value:String):void
            {
            
            _invoiceDate = value;
            }
          
            public function get  Qty(): int
            {
              return _qty;
            }

            public function set  Qty(value:int):void
            {
            
            _qty = value;
            }
          
            public function get  InvoiceRate(): Number
            {
              return _invoiceRate;
            }

            public function set  InvoiceRate(value:Number):void
            {
            
            _invoiceRate = value;
            }
          
            public function get  Status(): String
            {
            

                  if(_relatedInvoiceItemStatus != null)
                  return _relatedInvoiceItemStatus.Status;

                
            
            
            return undefined;
            }
            protected function set Status(value:String):void
            {

            

                  if(_relatedInvoiceItemStatus == null)
                      _relatedInvoiceItemStatus = new InvoiceItemStatus();

                  _relatedInvoiceItemStatus.Status = value;
                
            
            }
          
            public function get  Notes(): String
            {
              return _notes;
            }

            public function set  Notes(value:String):void
            {
            
            _notes = value;
            }
          
            public function get  IsSelected(): Boolean
            {
              return _isSelected;
            }

            public function set  IsSelected(value:Boolean):void
            {
            
            _isSelected = value;
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
      
        
        public function get RelatedBillItem():BillItem
        {
        if(IsLoaded  && 
        _relatedBillItem  && 
        
        !(_relatedBillItem.IsLoaded || _relatedBillItem.IsLoading))
        {
          _relatedBillItem = DataMapperRegistry.Instance.BillItem.load(_relatedBillItem);
          
          onParentChanged(_relatedBillItem);
        }

        return _relatedBillItem;
        }
        public function set RelatedBillItem(value:BillItem):void
        {
          _relatedBillItem = BillItem(IdentityMap.register( value ));

          onParentChanged(_relatedBillItem);
        }
      
        
        public function get RelatedInvoice():Invoice
        {
        if(IsLoaded  && 
        
        !(_relatedInvoice.IsLoaded || _relatedInvoice.IsLoading))
        {
          _relatedInvoice = DataMapperRegistry.Instance.Invoice.load(_relatedInvoice);
          
          onParentChanged(_relatedInvoice);
        }

        return _relatedInvoice;
        }
        public function set RelatedInvoice(value:Invoice):void
        {
          _relatedInvoice = Invoice(IdentityMap.register( value ));

          onParentChanged(_relatedInvoice);
        }
      
        
        public function get RelatedInvoiceItemStatus():InvoiceItemStatus
        {
        if(IsLoaded  && 
        
        !(_relatedInvoiceItemStatus.IsLoaded || _relatedInvoiceItemStatus.IsLoading))
        {
          _relatedInvoiceItemStatus = DataMapperRegistry.Instance.InvoiceItemStatus.load(_relatedInvoiceItemStatus);
          
          onParentChanged(_relatedInvoiceItemStatus);
        }

        return _relatedInvoiceItemStatus;
        }
        public function set RelatedInvoiceItemStatus(value:InvoiceItemStatus):void
        {
          _relatedInvoiceItemStatus = InvoiceItemStatus(IdentityMap.register( value ));

          onParentChanged(_relatedInvoiceItemStatus);
        }
      
        
        public function get RelatedInvoiceItemType():InvoiceItemType
        {
        if(IsLoaded  && 
        
        !(_relatedInvoiceItemType.IsLoaded || _relatedInvoiceItemType.IsLoading))
        {
          _relatedInvoiceItemType = DataMapperRegistry.Instance.InvoiceItemType.load(_relatedInvoiceItemType);
          
          onParentChanged(_relatedInvoiceItemType);
        }

        return _relatedInvoiceItemType;
        }
        public function set RelatedInvoiceItemType(value:InvoiceItemType):void
        {
          _relatedInvoiceItemType = InvoiceItemType(IdentityMap.register( value ));

          onParentChanged(_relatedInvoiceItemType);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedAssetAssignment != null)
              RelatedAssetAssignment.onChildChanged(this);
          
            
            if(RelatedBillItem != null)
              RelatedBillItem.onChildChanged(this);
          
            
            if(RelatedInvoice != null)
              RelatedInvoice.onChildChanged(this);
          
            
            if(RelatedInvoiceItemStatus != null)
              RelatedInvoiceItemStatus.onChildChanged(this);
          
            
            if(RelatedInvoiceItemType != null)
              RelatedInvoiceItemType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:InvoiceItem = new InvoiceItem();
          
          
            object.InvoiceItemId = this.InvoiceItemId;
          
            object.InvoiceItemTypeId = this.InvoiceItemTypeId;
          
            object.InvoiceId = this.InvoiceId;
          
            object.BillItemId = this.BillItemId;
          
            object.AssetAssignmentId = this.AssetAssignmentId;
          
            object.InvoiceDate = this.InvoiceDate;
          
            object.Qty = this.Qty;
          
            object.InvoiceRate = this.InvoiceRate;
          
            object.Status = this.Status;
          
            object.Notes = this.Notes;
          
            object.IsSelected = this.IsSelected;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  InvoiceItemId = object["InvoiceItemId"];
              InvoiceDate = object["InvoiceDate"];
              Qty = object["Qty"];
              InvoiceRate = object["InvoiceRate"];
              Notes = object["Notes"];
              IsSelected = object["IsSelected"];
              RelatedAssetAssignment =
          object.RelatedAssetAssignment;
        RelatedBillItem =
          object.RelatedBillItem;
        RelatedInvoice =
          object.RelatedInvoice;
        RelatedInvoiceItemStatus =
          object.RelatedInvoiceItemStatus;
        RelatedInvoiceItemType =
          object.RelatedInvoiceItemType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.InvoiceItem;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.InvoiceItem." 
            
              + InvoiceItemId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    