
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Invoice extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _invoiceId: int;
      
        protected var _invoiceNumber: String;
      
        protected var _clientName: String;
      
        protected var _clientAddress: String;
      
        protected var _clientActive: Boolean;
      
        protected var _notes: String;
      
        protected var _startDate: String;
      
        protected var _totalDailyAmt: int;
      
        protected var _dailyInvoiceAmt: Number = 0;
      
        protected var _otherInvoiceAmt: Number = 0;
      
        protected var _totalInvoiceAmt: Number = 0;
      
        // parent tables
        internal var _relatedClient: Client
            = new Client()
        ;
      
        // parent tables
        internal var _relatedInvoiceStatus: InvoiceStatus
            = new InvoiceStatus()
        ;
      
            public function get  InvoiceId(): int
            {
              return _invoiceId;
            }

            public function set  InvoiceId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _invoiceId = value;
            }
          
            public function get  InvoiceNumber(): String
            {
              return _invoiceNumber;
            }

            public function set  InvoiceNumber(value:String):void
            {
            
            _invoiceNumber = value;
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
          
            public function get  ClientActive(): Boolean
            {
              return _clientActive;
            }

            public function set  ClientActive(value:Boolean):void
            {
            
            _clientActive = value;
            }
          
            public function get  Status(): String
            {
            

                  if(_relatedInvoiceStatus != null)
                  return _relatedInvoiceStatus.Status;

                
            
            
            return undefined;
            }
            protected function set Status(value:String):void
            {

            

                  if(_relatedInvoiceStatus == null)
                      _relatedInvoiceStatus = new InvoiceStatus();

                  _relatedInvoiceStatus.Status = value;
                
            
            }
          
            public function get  Notes(): String
            {
              return _notes;
            }

            public function set  Notes(value:String):void
            {
            
            _notes = value;
            }
          
            public function get  StartDate(): String
            {
              return _startDate;
            }

            public function set  StartDate(value:String):void
            {
            
            _startDate = value;
            }
          
            public function get  TotalDailyAmt(): int
            {
              return _totalDailyAmt;
            }

            public function set  TotalDailyAmt(value:int):void
            {
            
            _totalDailyAmt = value;
            }
          
            public function get  DailyInvoiceAmt(): Number
            {
              return _dailyInvoiceAmt;
            }

            public function set  DailyInvoiceAmt(value:Number):void
            {
            
            _dailyInvoiceAmt = value;
            }
          
            public function get  OtherInvoiceAmt(): Number
            {
              return _otherInvoiceAmt;
            }

            public function set  OtherInvoiceAmt(value:Number):void
            {
            
            _otherInvoiceAmt = value;
            }
          
            public function get  TotalInvoiceAmt(): Number
            {
              return _totalInvoiceAmt;
            }

            public function set  TotalInvoiceAmt(value:Number):void
            {
            
            _totalInvoiceAmt = value;
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
      
        
        public function get RelatedInvoiceStatus():InvoiceStatus
        {
        if(IsLoaded  && 
        
        !(_relatedInvoiceStatus.IsLoaded || _relatedInvoiceStatus.IsLoading))
        {
          _relatedInvoiceStatus = DataMapperRegistry.Instance.InvoiceStatus.load(_relatedInvoiceStatus);
          
          onParentChanged(_relatedInvoiceStatus);
        }

        return _relatedInvoiceStatus;
        }
        public function set RelatedInvoiceStatus(value:InvoiceStatus):void
        {
          _relatedInvoiceStatus = InvoiceStatus(IdentityMap.register( value ));

          onParentChanged(_relatedInvoiceStatus);
        }
      

            // one to many relation
            protected var _relatedInvoiceItem:ActiveCollection;
            
            public function get RelatedInvoiceItem():ActiveCollection
            {
              _relatedInvoiceItem = onChildRelationRequest("relatedInvoiceItem",_relatedInvoiceItem);
              
              return _relatedInvoiceItem;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedClient != null)
              RelatedClient.onChildChanged(this);
          
            
            if(RelatedInvoiceStatus != null)
              RelatedInvoiceStatus.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Invoice = new Invoice();
          
          
            object.InvoiceId = this.InvoiceId;
          
            object.InvoiceNumber = this.InvoiceNumber;
          
            object.ClientId = this.ClientId;
          
            object.ClientName = this.ClientName;
          
            object.ClientAddress = this.ClientAddress;
          
            object.ClientActive = this.ClientActive;
          
            object.Status = this.Status;
          
            object.Notes = this.Notes;
          
            object.StartDate = this.StartDate;
          
            object.TotalDailyAmt = this.TotalDailyAmt;
          
            object.DailyInvoiceAmt = this.DailyInvoiceAmt;
          
            object.OtherInvoiceAmt = this.OtherInvoiceAmt;
          
            object.TotalInvoiceAmt = this.TotalInvoiceAmt;
          
            if(cascade)
            {
              
                    
                      for each(var invoiceItem :InvoiceItem in _relatedInvoiceItem)
                      {
                        if(invoiceItem.IsDirty)
                        {
                           var invoiceItemExtract:Object = invoiceItem.extractRelevant(true);
                               invoiceItemExtract.RelatedInvoice = object;

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
                  InvoiceId = object["InvoiceId"];
              InvoiceNumber = object["InvoiceNumber"];
              ClientName = object["ClientName"];
              ClientAddress = object["ClientAddress"];
              ClientActive = object["ClientActive"];
              Notes = object["Notes"];
              StartDate = object["StartDate"];
              TotalDailyAmt = object["TotalDailyAmt"];
              DailyInvoiceAmt = object["DailyInvoiceAmt"];
              OtherInvoiceAmt = object["OtherInvoiceAmt"];
              TotalInvoiceAmt = object["TotalInvoiceAmt"];
              RelatedClient =
          object.RelatedClient;
        RelatedInvoiceStatus =
          object.RelatedInvoiceStatus;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Invoice;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.Invoice." 
            
              + InvoiceId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    