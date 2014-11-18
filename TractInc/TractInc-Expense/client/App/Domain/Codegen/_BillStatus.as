
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _BillStatus extends ActiveRecord
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
            protected var _relatedBill:ActiveCollection;
            
            public function get RelatedBill():ActiveCollection
            {
              _relatedBill = onChildRelationRequest("relatedBill",_relatedBill);
              
              return _relatedBill;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:BillStatus = new BillStatus();
          
          
            object.Status = this.Status;
          
            if(cascade)
            {
              
                    
                      for each(var bill :Bill in _relatedBill)
                      {
                        if(bill.IsDirty)
                        {
                           var billExtract:Object = bill.extractRelevant(true);
                               billExtract.RelatedBillStatus = object;

                        object.RelatedBill.addItem(billExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedBill"])
                {
                  for each(var bill :ActiveRecord in this["relatedBill"] as Array)
                    childs.push(bill);
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
          return DataMapperRegistry.Instance.BillStatus;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.BillStatus." 
            
              + Status.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    