
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _WorkLog extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _workLogId: int;
      
        protected var _logMessage: String;
      
        // parent tables
        internal var _relatedBillItem: BillItem
            = new BillItem()
        ;
      
            public function get  WorkLogId(): int
            {
              return _workLogId;
            }

            public function set  WorkLogId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _workLogId = value;
            }
          
            public function get  BillItemId(): int
            {
            

                  if(_relatedBillItem != null)
                  return _relatedBillItem.BillItemId;

                
            
            
            return undefined;
            }
            protected function set BillItemId(value:int):void
            {

            

                  if(_relatedBillItem == null)
                      _relatedBillItem = new BillItem();

                  _relatedBillItem.BillItemId = value;
                
            
            }
          
            public function get  LogMessage(): String
            {
              return _logMessage;
            }

            public function set  LogMessage(value:String):void
            {
            
            _logMessage = value;
            }
          
        
        public function get RelatedBillItem():BillItem
        {
        if(IsLoaded  && 
        
        !(_relatedBillItem.IsLoaded || _relatedBillItem.IsLoading))
        {
          _relatedBillItem = DataMapperRegistry.Instance.BillItem.load(_relatedBillItem);
          
          onParentChanged(_relatedBillItem);
        }

        return _relatedBillItem;
        }
        public function set RelatedBillItem(value:BillItem):void
        {
          _relatedBillItem = BillItem(IdentityMap.register( value ));

          onParentChanged(_relatedBillItem);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedBillItem != null)
              RelatedBillItem.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:WorkLog = new WorkLog();
          
          
            object.WorkLogId = this.WorkLogId;
          
            object.BillItemId = this.BillItemId;
          
            object.LogMessage = this.LogMessage;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  WorkLogId = object["WorkLogId"];
              LogMessage = object["LogMessage"];
              RelatedBillItem =
          object.RelatedBillItem;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.WorkLog;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.WorkLog." 
            
              + WorkLogId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    