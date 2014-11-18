
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _BillItem extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _billItemId: int;
      
        protected var _billingDate: String;
      
        protected var _qty: int;
      
        protected var _billRate: Number = 0;
      
        protected var _notes: String;
      
        // parent tables
        internal var _relatedAssetAssignment: AssetAssignment
            = new AssetAssignment()
        ;
      
        // parent tables
        internal var _relatedBill: Bill
            = new Bill()
        ;
      
        // parent tables
        internal var _relatedBillItemComposition: BillItemComposition;
      
        // parent tables
        internal var _relatedBillItemStatus: BillItemStatus
            = new BillItemStatus()
        ;
      
        // parent tables
        internal var _relatedBillItemType: BillItemType
            = new BillItemType()
        ;
      
            public function get  BillItemId(): int
            {
              return _billItemId;
            }

            public function set  BillItemId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _billItemId = value;
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
          
            public function get  BillId(): int
            {
            

                  if(_relatedBill != null)
                  return _relatedBill.BillId;

                
            
            
            return undefined;
            }
            protected function set BillId(value:int):void
            {

            

                  if(_relatedBill == null)
                      _relatedBill = new Bill();

                  _relatedBill.BillId = value;
                
            
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
          
            public function get  BillingDate(): String
            {
              return _billingDate;
            }

            public function set  BillingDate(value:String):void
            {
            
            _billingDate = value;
            }
          
            public function get  Qty(): int
            {
              return _qty;
            }

            public function set  Qty(value:int):void
            {
            
            _qty = value;
            }
          
            public function get  BillRate(): Number
            {
              return _billRate;
            }

            public function set  BillRate(value:Number):void
            {
            
            _billRate = value;
            }
          
            public function get  Status(): String
            {
            

                  if(_relatedBillItemStatus != null)
                  return _relatedBillItemStatus.Status;

                
            
            
            return undefined;
            }
            protected function set Status(value:String):void
            {

            

                  if(_relatedBillItemStatus == null)
                      _relatedBillItemStatus = new BillItemStatus();

                  _relatedBillItemStatus.Status = value;
                
            
            }
          
            public function get  Notes(): String
            {
              return _notes;
            }

            public function set  Notes(value:String):void
            {
            
            _notes = value;
            }
          
            public function get  BillItemCompositionId(): int
            {
            

                  if(_relatedBillItemComposition != null)
                  return _relatedBillItemComposition.BillItemCompositionId;

                
            
            
            return undefined;
            }
            protected function set BillItemCompositionId(value:int):void
            {

            

                  if(_relatedBillItemComposition == null)
                      _relatedBillItemComposition = new BillItemComposition();

                  _relatedBillItemComposition.BillItemCompositionId = value;
                
            
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
      
        
        public function get RelatedBill():Bill
        {
        if(IsLoaded  && 
        
        !(_relatedBill.IsLoaded || _relatedBill.IsLoading))
        {
          _relatedBill = DataMapperRegistry.Instance.Bill.load(_relatedBill);
          
          onParentChanged(_relatedBill);
        }

        return _relatedBill;
        }
        public function set RelatedBill(value:Bill):void
        {
          _relatedBill = Bill(IdentityMap.register( value ));

          onParentChanged(_relatedBill);
        }
      
        
        public function get RelatedBillItemComposition():BillItemComposition
        {
        if(IsLoaded  && 
        _relatedBillItemComposition  && 
        
        !(_relatedBillItemComposition.IsLoaded || _relatedBillItemComposition.IsLoading))
        {
          _relatedBillItemComposition = DataMapperRegistry.Instance.BillItemComposition.load(_relatedBillItemComposition);
          
          onParentChanged(_relatedBillItemComposition);
        }

        return _relatedBillItemComposition;
        }
        public function set RelatedBillItemComposition(value:BillItemComposition):void
        {
          _relatedBillItemComposition = BillItemComposition(IdentityMap.register( value ));

          onParentChanged(_relatedBillItemComposition);
        }
      
        
        public function get RelatedBillItemStatus():BillItemStatus
        {
        if(IsLoaded  && 
        
        !(_relatedBillItemStatus.IsLoaded || _relatedBillItemStatus.IsLoading))
        {
          _relatedBillItemStatus = DataMapperRegistry.Instance.BillItemStatus.load(_relatedBillItemStatus);
          
          onParentChanged(_relatedBillItemStatus);
        }

        return _relatedBillItemStatus;
        }
        public function set RelatedBillItemStatus(value:BillItemStatus):void
        {
          _relatedBillItemStatus = BillItemStatus(IdentityMap.register( value ));

          onParentChanged(_relatedBillItemStatus);
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
      

            // one to many relation
            protected var _relatedBillItemAttachment:ActiveCollection;
            
            public function get RelatedBillItemAttachment():ActiveCollection
            {
              _relatedBillItemAttachment = onChildRelationRequest("relatedBillItemAttachment",_relatedBillItemAttachment);
              
              return _relatedBillItemAttachment;
            }
            
        

            // one to many relation
            protected var _relatedInvoiceItem:ActiveCollection;
            
            public function get RelatedInvoiceItem():ActiveCollection
            {
              _relatedInvoiceItem = onChildRelationRequest("relatedInvoiceItem",_relatedInvoiceItem);
              
              return _relatedInvoiceItem;
            }
            
        

            // one to many relation
            protected var _relatedWorkLog:ActiveCollection;
            
            public function get RelatedWorkLog():ActiveCollection
            {
              _relatedWorkLog = onChildRelationRequest("relatedWorkLog",_relatedWorkLog);
              
              return _relatedWorkLog;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedAssetAssignment != null)
              RelatedAssetAssignment.onChildChanged(this);
          
            
            if(RelatedBill != null)
              RelatedBill.onChildChanged(this);
          
            
            if(RelatedBillItemComposition != null)
              RelatedBillItemComposition.onChildChanged(this);
          
            
            if(RelatedBillItemStatus != null)
              RelatedBillItemStatus.onChildChanged(this);
          
            
            if(RelatedBillItemType != null)
              RelatedBillItemType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:BillItem = new BillItem();
          
          
            object.BillItemId = this.BillItemId;
          
            object.BillItemTypeId = this.BillItemTypeId;
          
            object.BillId = this.BillId;
          
            object.AssetAssignmentId = this.AssetAssignmentId;
          
            object.BillingDate = this.BillingDate;
          
            object.Qty = this.Qty;
          
            object.BillRate = this.BillRate;
          
            object.Status = this.Status;
          
            object.Notes = this.Notes;
          
            object.BillItemCompositionId = this.BillItemCompositionId;
          
            if(cascade)
            {
              
                    
                      for each(var billItemAttachment :BillItemAttachment in _relatedBillItemAttachment)
                      {
                        if(billItemAttachment.IsDirty)
                        {
                           var billItemAttachmentExtract:Object = billItemAttachment.extractRelevant(true);
                               billItemAttachmentExtract.RelatedBillItem = object;

                        object.RelatedBillItemAttachment.addItem(billItemAttachmentExtract);
                        }
                    }
                  
                    
                      for each(var invoiceItem :InvoiceItem in _relatedInvoiceItem)
                      {
                        if(invoiceItem.IsDirty)
                        {
                           var invoiceItemExtract:Object = invoiceItem.extractRelevant(true);
                               invoiceItemExtract.RelatedBillItem = object;

                        object.RelatedInvoiceItem.addItem(invoiceItemExtract);
                        }
                    }
                  
                    
                      for each(var workLog :WorkLog in _relatedWorkLog)
                      {
                        if(workLog.IsDirty)
                        {
                           var workLogExtract:Object = workLog.extractRelevant(true);
                               workLogExtract.RelatedBillItem = object;

                        object.RelatedWorkLog.addItem(workLogExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedBillItemAttachment"])
                {
                  for each(var billItemAttachment :ActiveRecord in this["relatedBillItemAttachment"] as Array)
                    childs.push(billItemAttachment);
                }
              
                if(this["relatedInvoiceItem"])
                {
                  for each(var invoiceItem :ActiveRecord in this["relatedInvoiceItem"] as Array)
                    childs.push(invoiceItem);
                }
              
                if(this["relatedWorkLog"])
                {
                  for each(var workLog :ActiveRecord in this["relatedWorkLog"] as Array)
                    childs.push(workLog);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  BillItemId = object["BillItemId"];
              BillingDate = object["BillingDate"];
              Qty = object["Qty"];
              BillRate = object["BillRate"];
              Notes = object["Notes"];
              RelatedAssetAssignment =
          object.RelatedAssetAssignment;
        RelatedBill =
          object.RelatedBill;
        RelatedBillItemComposition =
          object.RelatedBillItemComposition;
        RelatedBillItemStatus =
          object.RelatedBillItemStatus;
        RelatedBillItemType =
          object.RelatedBillItemType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.BillItem;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.BillItem." 
            
              + BillItemId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    