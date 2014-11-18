
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _InvoiceItemStatus extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _status: String;
      
            public function get  Status(): String
            {
              return _status;
            }

            public function set  Status(value:String):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _status = value;
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
          var object:InvoiceItemStatus = new InvoiceItemStatus();
          
          
            object.Status = this.Status;
          
            if(cascade)
            {
              
                    
                      for each(var invoiceItem :InvoiceItem in _relatedInvoiceItem)
                      {
                        if(invoiceItem.IsDirty)
                        {
                           var invoiceItemExtract:Object = invoiceItem.extractRelevant(true);
                               invoiceItemExtract.RelatedInvoiceItemStatus = object;

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
                  Status = object["Status"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.InvoiceItemStatus;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.InvoiceItemStatus." 
            
              + Status.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    