
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _BillItemComposition extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _billItemCompositionId: int;
      
        protected var _amount: Number = 0;
      
        protected var _description: String;
      
        // parent tables
        internal var _relatedBill: Bill
            = new Bill()
        ;
      
        // parent tables
        internal var _relatedBillItemType: BillItemType
            = new BillItemType()
        ;
      
            public function get  BillItemCompositionId(): int
            {
              return _billItemCompositionId;
            }

            public function set  BillItemCompositionId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _billItemCompositionId = value;
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
          
            public function get  Amount(): Number
            {
              return _amount;
            }

            public function set  Amount(value:Number):void
            {
            
            _amount = value;
            }
          
            public function get  Description(): String
            {
              return _description;
            }

            public function set  Description(value:String):void
            {
            
            _description = value;
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
            protected var _relatedBillItem:ActiveCollection;
            
            public function get RelatedBillItem():ActiveCollection
            {
              _relatedBillItem = onChildRelationRequest("relatedBillItem",_relatedBillItem);
              
              return _relatedBillItem;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedBill != null)
              RelatedBill.onChildChanged(this);
          
            
            if(RelatedBillItemType != null)
              RelatedBillItemType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:BillItemComposition = new BillItemComposition();
          
          
            object.BillItemCompositionId = this.BillItemCompositionId;
          
            object.BillId = this.BillId;
          
            object.BillItemTypeId = this.BillItemTypeId;
          
            object.Amount = this.Amount;
          
            object.Description = this.Description;
          
            if(cascade)
            {
              
                    
                      for each(var billItem :BillItem in _relatedBillItem)
                      {
                        if(billItem.IsDirty)
                        {
                           var billItemExtract:Object = billItem.extractRelevant(true);
                               billItemExtract.RelatedBillItemComposition = object;

                        object.RelatedBillItem.addItem(billItemExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedBillItem"])
                {
                  for each(var billItem :ActiveRecord in this["relatedBillItem"] as Array)
                    childs.push(billItem);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  BillItemCompositionId = object["BillItemCompositionId"];
              Amount = object["Amount"];
              Description = object["Description"];
              RelatedBill =
          object.RelatedBill;
        RelatedBillItemType =
          object.RelatedBillItemType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.BillItemComposition;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.BillItemComposition." 
            
              + BillItemCompositionId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    