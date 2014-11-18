
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Client extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _clientId: int;
      
        protected var _clientName: String;
      
        protected var _clientAddress: String;
      
        protected var _active: Boolean;
      
        protected var _deleted: Boolean;
      
            public function get  ClientId(): int
            {
              return _clientId;
            }

            public function set  ClientId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _clientId = value;
            }
          
            public function get  ClientName(): String
            {
              return _clientName;
            }

            public function set  ClientName(value:String):void
            {
            
            _clientName = value;
            }
          
            public function get  ClientAddress(): String
            {
              return _clientAddress;
            }

            public function set  ClientAddress(value:String):void
            {
            
            _clientAddress = value;
            }
          
            public function get  Active(): Boolean
            {
              return _active;
            }

            public function set  Active(value:Boolean):void
            {
            
            _active = value;
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
            protected var _relatedAfe:ActiveCollection;
            
            public function get RelatedAfe():ActiveCollection
            {
              _relatedAfe = onChildRelationRequest("relatedAfe",_relatedAfe);
              
              return _relatedAfe;
            }
            
        

            // one to many relation
            protected var _relatedDefaultInvoiceRate:ActiveCollection;
            
            public function get RelatedDefaultInvoiceRate():ActiveCollection
            {
              _relatedDefaultInvoiceRate = onChildRelationRequest("relatedDefaultInvoiceRate",_relatedDefaultInvoiceRate);
              
              return _relatedDefaultInvoiceRate;
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
          var object:Client = new Client();
          
          
            object.ClientId = this.ClientId;
          
            object.ClientName = this.ClientName;
          
            object.ClientAddress = this.ClientAddress;
          
            object.Active = this.Active;
          
            object.Deleted = this.Deleted;
          
            if(cascade)
            {
              
                    
                      for each(var afe :Afe in _relatedAfe)
                      {
                        if(afe.IsDirty)
                        {
                           var afeExtract:Object = afe.extractRelevant(true);
                               afeExtract.RelatedClient = object;

                        object.RelatedAfe.addItem(afeExtract);
                        }
                    }
                  
                    
                      for each(var defaultInvoiceRate :DefaultInvoiceRate in _relatedDefaultInvoiceRate)
                      {
                        if(defaultInvoiceRate.IsDirty)
                        {
                           var defaultInvoiceRateExtract:Object = defaultInvoiceRate.extractRelevant(true);
                               defaultInvoiceRateExtract.RelatedClient = object;

                        object.RelatedDefaultInvoiceRate.addItem(defaultInvoiceRateExtract);
                        }
                    }
                  
                    
                      for each(var invoice :Invoice in _relatedInvoice)
                      {
                        if(invoice.IsDirty)
                        {
                           var invoiceExtract:Object = invoice.extractRelevant(true);
                               invoiceExtract.RelatedClient = object;

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

          
                if(this["relatedAfe"])
                {
                  for each(var afe :ActiveRecord in this["relatedAfe"] as Array)
                    childs.push(afe);
                }
              
                if(this["relatedDefaultInvoiceRate"])
                {
                  for each(var defaultInvoiceRate :ActiveRecord in this["relatedDefaultInvoiceRate"] as Array)
                    childs.push(defaultInvoiceRate);
                }
              
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
                  ClientId = object["ClientId"];
              ClientName = object["ClientName"];
              ClientAddress = object["ClientAddress"];
              Active = object["Active"];
              Deleted = object["Deleted"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Client;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.Client." 
            
              + ClientId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    