
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _TermUnit extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _termUnitId: int;
      
        protected var _name: String;
      
            public function get  TermUnitId(): int
            {
              return _termUnitId;
            }

            public function set  TermUnitId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _termUnitId = value;
            }
          
            public function get  Name(): String
            {
              return _name;
            }

            public function set  Name(value:String):void
            {
            
            _name = value;
            }
          

            // one to many relation
            protected var _leases:ActiveCollection;
            
            public function get Leases():ActiveCollection
            {
              _leases = onChildRelationRequest("leases",_leases);
              
              return _leases;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:TermUnit = new TermUnit();
          
          
            object.TermUnitId = this.TermUnitId;
          
            object.Name = this.Name;
          
            if(cascade)
            {
              
                    
                      for each(var lease :Lease in _leases)
                      {
                        if(lease.IsDirty)
                        {
                           var leaseExtract:Object = lease.extractRelevant(true);
                               leaseExtract.ParentTermUnit = object;

                        object.Leases.addItem(leaseExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["leases"])
                {
                  for each(var lease :ActiveRecord in this["leases"] as Array)
                    childs.push(lease);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        TermUnitId = object["TermUnitId"];
        Name = object["Name"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.TermUnit;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.TermUnit." 
            
              + TermUnitId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    