
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _InvoiceStatus extends ActiveRecord
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
            protected var _relatedInvoice:ActiveCollection;
            
            public function get RelatedInvoice():ActiveCollection
            {
              _relatedInvoice = onChildRelationRequest("relatedInvoice",_relatedInvoice);
              
              return _relatedInvoice;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:InvoiceStatus = new InvoiceStatus();
          
          
            object.Status = this.Status;
          
            if(cascade)
            {
              
                    
                      for each(var invoice :Invoice in _relatedInvoice)
                      {
                        if(invoice.IsDirty)
                        {
                           var invoiceExtract:Object = invoice.extractRelevant(true);
                               invoiceExtract.RelatedInvoiceStatus = object;

                        object.RelatedInvoice.addItem(invoiceExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedInvoice"])
                {
                  for each(var invoice :ActiveRecord in this["relatedInvoice"] as Array)
                    childs.push(invoice);
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
          return DataMapperRegistry.Instance.InvoiceStatus;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.InvoiceStatus." 
            
              + Status.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    