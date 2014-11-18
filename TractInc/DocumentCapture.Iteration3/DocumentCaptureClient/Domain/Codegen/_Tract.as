
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Tract extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _tractID: int;
      
        protected var _docID: int;
      
        protected var _refName: String;
      
        protected var _calledAC: Number;
      
        protected var _scopePlotUrl: String;
      
            public function get  TractID(): int
            {
            return _tractID;
            }

            public function set  TractID(value:int):void
            {
            
            _tractID = value;
            }
          
            public function get  DocID(): int
            {
            return _docID;
            }

            public function set  DocID(value:int):void
            {
            
            _docID = value;
            }
          
            public function get  RefName(): String
            {
            return _refName;
            }

            public function set  RefName(value:String):void
            {
            
            _refName = value;
            }
          
            public function get  CalledAC(): Number
            {
            return _calledAC;
            }

            public function set  CalledAC(value:Number):void
            {
            
            _calledAC = value;
            }
          
            public function get  ScopePlotUrl(): String
            {
            return _scopePlotUrl;
            }

            public function set  ScopePlotUrl(value:String):void
            {
            
            _scopePlotUrl = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Tract = new Tract();
          
          
            object.TractID = this.TractID;
          
            object.DocID = this.DocID;
          
            object.RefName = this.RefName;
          
            object.CalledAC = this.CalledAC;
          
            object.ScopePlotUrl = this.ScopePlotUrl;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        TractID = object["TractID"];
        DocID = object["DocID"];
        RefName = object["RefName"];
        CalledAC = object["CalledAC"];
        ScopePlotUrl = object["ScopePlotUrl"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Tract;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Tract." 
            
              + TractID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    