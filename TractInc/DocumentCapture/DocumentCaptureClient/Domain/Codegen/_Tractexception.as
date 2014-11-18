
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Tractexception extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _tractExceptionsID: int;
      
        protected var _tractID: int;
      
        protected var _refName: String;
      
        protected var _calledAC: String;
      
            public function get  TractExceptionsID(): int
            {
            return _tractExceptionsID;
            }

            public function set  TractExceptionsID(value:int):void
            {
            
            _tractExceptionsID = value;
            }
          
            public function get  TractID(): int
            {
            return _tractID;
            }

            public function set  TractID(value:int):void
            {
            
            _tractID = value;
            }
          
            public function get  RefName(): String
            {
            return _refName;
            }

            public function set  RefName(value:String):void
            {
            
            _refName = value;
            }
          
            public function get  CalledAC(): String
            {
            return _calledAC;
            }

            public function set  CalledAC(value:String):void
            {
            
            _calledAC = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Tractexception = new Tractexception();
          
          
            object.TractExceptionsID = this.TractExceptionsID;
          
            object.TractID = this.TractID;
          
            object.RefName = this.RefName;
          
            object.CalledAC = this.CalledAC;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        TractExceptionsID = object["TractExceptionsID"];
        TractID = object["TractID"];
        RefName = object["RefName"];
        CalledAC = object["CalledAC"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Tractexception;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Tractexception." 
            
              + TractExceptionsID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    