
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _InvoiceItemType extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _invoiceItemTypeId: int;
      
        protected var _name: String;
      
        protected var _isCountable: Boolean;
      
        protected var _isPresetRate: Boolean;
      
        protected var _isSingle: Boolean;
      
        protected var _deleted: Boolean;
      
            public function get  InvoiceItemTypeId(): int
            {
              return _invoiceItemTypeId;
            }

            public function set  InvoiceItemTypeId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _invoiceItemTypeId = value;
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
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          

            // one to many relation
            protected var _relatedBillItemType:ActiveCollection;
            
            public function get RelatedBillItemType():ActiveCollection
            {
              _relatedBillItemType = onChildRelationRequest("relatedBillItemType",_relatedBillItemType);
              
              return _relatedBillItemType;
            }
            
        

            // one to many relation
            protected var _relatedDefaultInvoiceRate:ActiveCollection;
            
            public function get RelatedDefaultInvoiceRate():ActiveCollection
            {
              _relatedDefaultInvoiceRate = onChildRelationRequest("relatedDefaultInvoiceRate",_relatedDefaultInvoiceRate);
              
              return _relatedDefaultInvoiceRate;
            }
            
        

            // one to many relation
            protected var _relatedInvoiceItem:ActiveCollection;
            
            public function get RelatedInvoiceItem():ActiveCollection
            {
              _relatedInvoiceItem = onChildRelationRequest("relatedInvoiceItem",_relatedInvoiceItem);
              
              return _relatedInvoiceItem;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:InvoiceItemType = new InvoiceItemType();
          
          
            object.InvoiceItemTypeId = this.InvoiceItemTypeId;
          
            object.Name = this.Name;
          
            object.IsCountable = this.IsCountable;
          
            object.IsPresetRate = this.IsPresetRate;
          
            object.IsSingle = this.IsSingle;
          
            object.Deleted = this.Deleted;
          
            if(cascade)
            {
              
                    
                      for each(var billItemType :BillItemType in _relatedBillItemType)
                      {
                        if(billItemType.IsDirty)
                        {
                           var billItemTypeExtract:Object = billItemType.extractRelevant(true);
                               billItemTypeExtract.RelatedInvoiceItemType = object;

                        object.RelatedBillItemType.addItem(billItemTypeExtract);
                        }
                    }
                  
                    
                      for each(var defaultInvoiceRate :DefaultInvoiceRate in _relatedDefaultInvoiceRate)
                      {
                        if(defaultInvoiceRate.IsDirty)
                        {
                           var defaultInvoiceRateExtract:Object = defaultInvoiceRate.extractRelevant(true);
                               defaultInvoiceRateExtract.RelatedInvoiceItemType = object;

                        object.RelatedDefaultInvoiceRate.addItem(defaultInvoiceRateExtract);
                        }
                    }
                  
                    
                      for each(var invoiceItem :InvoiceItem in _relatedInvoiceItem)
                      {
                        if(invoiceItem.IsDirty)
                        {
                           var invoiceItemExtract:Object = invoiceItem.extractRelevant(true);
                               invoiceItemExtract.RelatedInvoiceItemType = object;

                        object.RelatedInvoiceItem.addItem(invoiceItemExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedBillItemType"])
                {
                  for each(var billItemType :ActiveRecord in this["relatedBillItemType"] as Array)
                    childs.push(billItemType);
                }
              
                if(this["relatedDefaultInvoiceRate"])
                {
                  for each(var defaultInvoiceRate :ActiveRecord in this["relatedDefaultInvoiceRate"] as Array)
                    childs.push(defaultInvoiceRate);
                }
              
                if(this["relatedInvoiceItem"])
                {
                  for each(var invoiceItem :ActiveRecord in this["relatedInvoiceItem"] as Array)
                    childs.push(invoiceItem);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  InvoiceItemTypeId = object["InvoiceItemTypeId"];
              Name = object["Name"];
              IsCountable = object["IsCountable"];
              IsPresetRate = object["IsPresetRate"];
              IsSingle = object["IsSingle"];
              Deleted = object["Deleted"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.InvoiceItemType;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.InvoiceItemType." 
            
              + InvoiceItemTypeId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    