
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _DefaultInvoiceRate extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _defaultInvoiceRateId: int;
      
        protected var _invoiceRate: Number = 0;
      
        // parent tables
        internal var _relatedClient: Client
            = new Client()
        ;
      
        // parent tables
        internal var _relatedInvoiceItemType: InvoiceItemType
            = new InvoiceItemType()
        ;
      
            public function get  DefaultInvoiceRateId(): int
            {
              return _defaultInvoiceRateId;
            }

            public function set  DefaultInvoiceRateId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _defaultInvoiceRateId = value;
            }
          
            public function get  ClientId(): int
            {
            

                  if(_relatedClient != null)
                  return _relatedClient.ClientId;

                
            
            
            return undefined;
            }
            protected function set ClientId(value:int):void
            {

            

                  if(_relatedClient == null)
                      _relatedClient = new Client();

                  _relatedClient.ClientId = value;
                
            
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
          
            public function get  InvoiceRate(): Number
            {
              return _invoiceRate;
            }

            public function set  InvoiceRate(value:Number):void
            {
            
            _invoiceRate = value;
            }
          
        
        public function get RelatedClient():Client
        {
        if(IsLoaded  && 
        
        !(_relatedClient.IsLoaded || _relatedClient.IsLoading))
        {
          _relatedClient = DataMapperRegistry.Instance.Client.load(_relatedClient);
          
          onParentChanged(_relatedClient);
        }

        return _relatedClient;
        }
        public function set RelatedClient(value:Client):void
        {
          _relatedClient = Client(IdentityMap.register( value ));

          onParentChanged(_relatedClient);
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
          
            
            if(RelatedClient != null)
              RelatedClient.onChildChanged(this);
          
            
            if(RelatedInvoiceItemType != null)
              RelatedInvoiceItemType.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:DefaultInvoiceRate = new DefaultInvoiceRate();
          
          
            object.DefaultInvoiceRateId = this.DefaultInvoiceRateId;
          
            object.ClientId = this.ClientId;
          
            object.InvoiceItemTypeId = this.InvoiceItemTypeId;
          
            object.InvoiceRate = this.InvoiceRate;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  DefaultInvoiceRateId = object["DefaultInvoiceRateId"];
              InvoiceRate = object["InvoiceRate"];
              RelatedClient =
          object.RelatedClient;
        RelatedInvoiceItemType =
          object.RelatedInvoiceItemType;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.DefaultInvoiceRate;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.DefaultInvoiceRate." 
            
              + DefaultInvoiceRateId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    