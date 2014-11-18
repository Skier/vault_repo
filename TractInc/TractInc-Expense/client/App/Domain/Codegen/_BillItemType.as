
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _BillItemType extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _billItemTypeId: int;
      
        protected var _name: String;
      
        protected var _isCountable: Boolean;
      
        protected var _isPresetRate: Boolean;
      
        protected var _isSingle: Boolean;
      
        protected var _isAttachRequired: Boolean;
      
        protected var _deleted: Boolean;
      
        // parent tables
        internal var _relatedInvoiceItemType: InvoiceItemType
            = new InvoiceItemType()
        ;
      
            public function get  BillItemTypeId(): int
            {
              return _billItemTypeId;
            }

            public function set  BillItemTypeId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _billItemTypeId = value;
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
          
            public function get  Name(): String
            {
              return _name;
            }

            public function set  Name(value:String):void
            {
            
            _name = value;
            }
          
            public function get  IsCountable(): Boolean
            {
              return _isCountable;
            }

            public function set  IsCountable(value:Boolean):void
            {
            
            _isCountable = value;
            }
          
            public function get  IsPresetRate(): Boolean
            {
              return _isPresetRate;
            }

            public function set  IsPresetRate(value:Boolean):void
            {
            
            _isPresetRate = value;
            }
          
            public function get  IsSingle(): Boolean
            {
              return _isSingle;
            }

            public function set  IsSingle(value:Boolean):void
            {
            
            _isSingle = value;
            }
          
            public function get  IsAttachRequired(): Boolean
            {
              return _isAttachRequired;
            }

            public function set  IsAttachRequired(value:Boolean):void
            {
            
            _isAttachRequired = value;
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
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
      

            // one to many relation
            protected var _relatedBillItemComposition:ActiveCollection;
            
            public function get RelatedBillItemComposition():ActiveCollection
            {
              _relatedBillItemComposition = onChildRelationRequest("relatedBillItemComposition",_relatedBillItemComposition);
              
              return _relatedBillItemComposition;
            }
            
        

            // one to many relation
            protected var _relatedDefaultBillRate:ActiveCollection;
            
            public function get RelatedDefaultBillRate():ActiveCollection
            {
              _relatedDefaultBillRate = onChildRelationRequest("relatedDefaultBillRate",_relatedDefaultBillRate);
              
              return _relatedDefaultBillRate;
            }
            
        

            // one to many relation
            protected var _relatedBillItem:ActiveCollection;
            
            public function get RelatedBillItem():ActiveCollection
            {
              _relatedBillItem = onChildRelationRequest("relatedBillItem",_relatedBillItem);
              
              return _relatedBillItem;
            }
            
        

            // one to many relation
            protected var _relatedRateByAssignment:ActiveCollection;
            
            public function get RelatedRateByAssignment():ActiveCollection
            {
              _relatedRateByAssignment = onChildRelationRequest("relatedRateByAssignment",_relatedRateByAssignment);
              
              return _relatedRateByAssignment;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedInvoiceItemType != null)
              RelatedInvoiceItemType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:BillItemType = new BillItemType();
          
          
            object.BillItemTypeId = this.BillItemTypeId;
          
            object.InvoiceItemTypeId = this.InvoiceItemTypeId;
          
            object.Name = this.Name;
          
            object.IsCountable = this.IsCountable;
          
            object.IsPresetRate = this.IsPresetRate;
          
            object.IsSingle = this.IsSingle;
          
            object.IsAttachRequired = this.IsAttachRequired;
          
            object.Deleted = this.Deleted;
          
            if(cascade)
            {
              
                    
                      for each(var billItemComposition :BillItemComposition in _relatedBillItemComposition)
                      {
                        if(billItemComposition.IsDirty)
                        {
                           var billItemCompositionExtract:Object = billItemComposition.extractRelevant(true);
                               billItemCompositionExtract.RelatedBillItemType = object;

                        object.RelatedBillItemComposition.addItem(billItemCompositionExtract);
                        }
                    }
                  
                    
                      for each(var defaultBillRate :DefaultBillRate in _relatedDefaultBillRate)
                      {
                        if(defaultBillRate.IsDirty)
                        {
                           var defaultBillRateExtract:Object = defaultBillRate.extractRelevant(true);
                               defaultBillRateExtract.RelatedBillItemType = object;

                        object.RelatedDefaultBillRate.addItem(defaultBillRateExtract);
                        }
                    }
                  
                    
                      for each(var billItem :BillItem in _relatedBillItem)
                      {
                        if(billItem.IsDirty)
                        {
                           var billItemExtract:Object = billItem.extractRelevant(true);
                               billItemExtract.RelatedBillItemType = object;

                        object.RelatedBillItem.addItem(billItemExtract);
                        }
                    }
                  
                    
                      for each(var rateByAssignment :RateByAssignment in _relatedRateByAssignment)
                      {
                        if(rateByAssignment.IsDirty)
                        {
                           var rateByAssignmentExtract:Object = rateByAssignment.extractRelevant(true);
                               rateByAssignmentExtract.RelatedBillItemType = object;

                        object.RelatedRateByAssignment.addItem(rateByAssignmentExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedBillItemComposition"])
                {
                  for each(var billItemComposition :ActiveRecord in this["relatedBillItemComposition"] as Array)
                    childs.push(billItemComposition);
                }
              
                if(this["relatedDefaultBillRate"])
                {
                  for each(var defaultBillRate :ActiveRecord in this["relatedDefaultBillRate"] as Array)
                    childs.push(defaultBillRate);
                }
              
                if(this["relatedBillItem"])
                {
                  for each(var billItem :ActiveRecord in this["relatedBillItem"] as Array)
                    childs.push(billItem);
                }
              
                if(this["relatedRateByAssignment"])
                {
                  for each(var rateByAssignment :ActiveRecord in this["relatedRateByAssignment"] as Array)
                    childs.push(rateByAssignment);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  BillItemTypeId = object["BillItemTypeId"];
              Name = object["Name"];
              IsCountable = object["IsCountable"];
              IsPresetRate = object["IsPresetRate"];
              IsSingle = object["IsSingle"];
              IsAttachRequired = object["IsAttachRequired"];
              Deleted = object["Deleted"];
              RelatedInvoiceItemType =
          object.RelatedInvoiceItemType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.BillItemType;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.BillItemType." 
            
              + BillItemTypeId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    