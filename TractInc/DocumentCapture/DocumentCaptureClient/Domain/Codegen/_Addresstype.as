
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Addresstype extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _addressTypeID: int;
      
        protected var _types: String;
      
            public function get  AddressTypeID(): int
            {
            return _addressTypeID;
            }

            public function set  AddressTypeID(value:int):void
            {
            
            _addressTypeID = value;
            }
          
            public function get  Types(): String
            {
            return _types;
            }

            public function set  Types(value:String):void
            {
            
            _types = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Addresstype = new Addresstype();
          
          
            object.AddressTypeID = this.AddressTypeID;
          
            object.Types = this.Types;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        AddressTypeID = object["AddressTypeID"];
        Types = object["Types"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Addresstype;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Addresstype." 
            
              + AddressTypeID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    