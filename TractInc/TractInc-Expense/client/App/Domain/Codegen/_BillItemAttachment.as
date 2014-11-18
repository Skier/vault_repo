
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _BillItemAttachment extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _billItemAttachmentId: int;
      
        protected var _fileName: String;
      
        protected var _originalFileName: String;
      
        // parent tables
        internal var _relatedBillItem: BillItem
            = new BillItem()
        ;
      
            public function get  BillItemAttachmentId(): int
            {
              return _billItemAttachmentId;
            }

            public function set  BillItemAttachmentId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _billItemAttachmentId = value;
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
          
            public function get  FileName(): String
            {
              return _fileName;
            }

            public function set  FileName(value:String):void
            {
            
            _fileName = value;
            }
          
            public function get  OriginalFileName(): String
            {
              return _originalFileName;
            }

            public function set  OriginalFileName(value:String):void
            {
            
            _originalFileName = value;
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
          var object:BillItemAttachment = new BillItemAttachment();
          
          
            object.BillItemAttachmentId = this.BillItemAttachmentId;
          
            object.BillItemId = this.BillItemId;
          
            object.FileName = this.FileName;
          
            object.OriginalFileName = this.OriginalFileName;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  BillItemAttachmentId = object["BillItemAttachmentId"];
              FileName = object["FileName"];
              OriginalFileName = object["OriginalFileName"];
              RelatedBillItem =
          object.RelatedBillItem;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.BillItemAttachment;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.BillItemAttachment." 
            
              + BillItemAttachmentId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    