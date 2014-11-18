
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Bill extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _billId: int;
      
        protected var _notes: String;
      
        protected var _startDate: String;
      
        protected var _totalDailyBill: int;
      
        protected var _dailyBillAmt: Number = 0;
      
        protected var _otherBillAmt: Number = 0;
      
        protected var _totalBillAmt: Number = 0;
      
        // parent tables
        internal var _relatedAsset: Asset
            = new Asset()
        ;
      
        // parent tables
        internal var _relatedBillStatus: BillStatus
            = new BillStatus()
        ;
      
            public function get  BillId(): int
            {
              return _billId;
            }

            public function set  BillId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _billId = value;
            }
          
            public function get  Status(): String
            {
            

                  if(_relatedBillStatus != null)
                  return _relatedBillStatus.Status;

                
            
            
            return undefined;
            }
            protected function set Status(value:String):void
            {

            

                  if(_relatedBillStatus == null)
                      _relatedBillStatus = new BillStatus();

                  _relatedBillStatus.Status = value;
                
            
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
          
            public function get  AssetId(): int
            {
            

                  if(_relatedAsset != null)
                  return _relatedAsset.AssetId;

                
            
            
            return undefined;
            }
            protected function set AssetId(value:int):void
            {

            

                  if(_relatedAsset == null)
                      _relatedAsset = new Asset();

                  _relatedAsset.AssetId = value;
                
            
            }
          
            public function get  TotalDailyBill(): int
            {
              return _totalDailyBill;
            }

            public function set  TotalDailyBill(value:int):void
            {
            
            _totalDailyBill = value;
            }
          
            public function get  DailyBillAmt(): Number
            {
              return _dailyBillAmt;
            }

            public function set  DailyBillAmt(value:Number):void
            {
            
            _dailyBillAmt = value;
            }
          
            public function get  OtherBillAmt(): Number
            {
              return _otherBillAmt;
            }

            public function set  OtherBillAmt(value:Number):void
            {
            
            _otherBillAmt = value;
            }
          
            public function get  TotalBillAmt(): Number
            {
              return _totalBillAmt;
            }

            public function set  TotalBillAmt(value:Number):void
            {
            
            _totalBillAmt = value;
            }
          
        
        public function get RelatedAsset():Asset
        {
        if(IsLoaded  && 
        
        !(_relatedAsset.IsLoaded || _relatedAsset.IsLoading))
        {
          _relatedAsset = DataMapperRegistry.Instance.Asset.load(_relatedAsset);
          
          onParentChanged(_relatedAsset);
        }

        return _relatedAsset;
        }
        public function set RelatedAsset(value:Asset):void
        {
          _relatedAsset = Asset(IdentityMap.register( value ));

          onParentChanged(_relatedAsset);
        }
      
        
        public function get RelatedBillStatus():BillStatus
        {
        if(IsLoaded  && 
        
        !(_relatedBillStatus.IsLoaded || _relatedBillStatus.IsLoading))
        {
          _relatedBillStatus = DataMapperRegistry.Instance.BillStatus.load(_relatedBillStatus);
          
          onParentChanged(_relatedBillStatus);
        }

        return _relatedBillStatus;
        }
        public function set RelatedBillStatus(value:BillStatus):void
        {
          _relatedBillStatus = BillStatus(IdentityMap.register( value ));

          onParentChanged(_relatedBillStatus);
        }
      

            // one to many relation
            protected var _relatedBillItemComposition:ActiveCollection;
            
            public function get RelatedBillItemComposition():ActiveCollection
            {
              _relatedBillItemComposition = onChildRelationRequest("relatedBillItemComposition",_relatedBillItemComposition);
              
              return _relatedBillItemComposition;
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
          
            
            if(RelatedAsset != null)
              RelatedAsset.onChildChanged(this);
          
            
            if(RelatedBillStatus != null)
              RelatedBillStatus.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Bill = new Bill();
          
          
            object.BillId = this.BillId;
          
            object.Status = this.Status;
          
            object.Notes = this.Notes;
          
            object.StartDate = this.StartDate;
          
            object.AssetId = this.AssetId;
          
            object.TotalDailyBill = this.TotalDailyBill;
          
            object.DailyBillAmt = this.DailyBillAmt;
          
            object.OtherBillAmt = this.OtherBillAmt;
          
            object.TotalBillAmt = this.TotalBillAmt;
          
            if(cascade)
            {
              
                    
                      for each(var billItemComposition :BillItemComposition in _relatedBillItemComposition)
                      {
                        if(billItemComposition.IsDirty)
                        {
                           var billItemCompositionExtract:Object = billItemComposition.extractRelevant(true);
                               billItemCompositionExtract.RelatedBill = object;

                        object.RelatedBillItemComposition.addItem(billItemCompositionExtract);
                        }
                    }
                  
                    
                      for each(var billItem :BillItem in _relatedBillItem)
                      {
                        if(billItem.IsDirty)
                        {
                           var billItemExtract:Object = billItem.extractRelevant(true);
                               billItemExtract.RelatedBill = object;

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

          
                if(this["relatedBillItemComposition"])
                {
                  for each(var billItemComposition :ActiveRecord in this["relatedBillItemComposition"] as Array)
                    childs.push(billItemComposition);
                }
              
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
                  BillId = object["BillId"];
              Notes = object["Notes"];
              StartDate = object["StartDate"];
              TotalDailyBill = object["TotalDailyBill"];
              DailyBillAmt = object["DailyBillAmt"];
              OtherBillAmt = object["OtherBillAmt"];
              TotalBillAmt = object["TotalBillAmt"];
              RelatedAsset =
          object.RelatedAsset;
        RelatedBillStatus =
          object.RelatedBillStatus;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Bill;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.Bill." 
            
              + BillId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    