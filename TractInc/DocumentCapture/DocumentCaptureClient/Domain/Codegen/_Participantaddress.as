
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Participantaddress extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _addressID: int;
      
        protected var _participantlID: int;
      
        protected var _addressTypeID: int;
      
        protected var _line1: String;
      
        protected var _line2: String;
      
        protected var _city: String;
      
        protected var _state: String;
      
        protected var _zip: String;
      
        protected var _incareof: String;
      
            public function get  AddressID(): int
            {
            return _addressID;
            }

            public function set  AddressID(value:int):void
            {
            
            _addressID = value;
            }
          
            public function get  ParticipantlID(): int
            {
            return _participantlID;
            }

            public function set  ParticipantlID(value:int):void
            {
            
            _participantlID = value;
            }
          
            public function get  AddressTypeID(): int
            {
            return _addressTypeID;
            }

            public function set  AddressTypeID(value:int):void
            {
            
            _addressTypeID = value;
            }
          
            public function get  Line1(): String
            {
            return _line1;
            }

            public function set  Line1(value:String):void
            {
            
            _line1 = value;
            }
          
            public function get  Line2(): String
            {
            return _line2;
            }

            public function set  Line2(value:String):void
            {
            
            _line2 = value;
            }
          
            public function get  City(): String
            {
            return _city;
            }

            public function set  City(value:String):void
            {
            
            _city = value;
            }
          
            public function get  State(): String
            {
            return _state;
            }

            public function set  State(value:String):void
            {
            
            _state = value;
            }
          
            public function get  Zip(): String
            {
            return _zip;
            }

            public function set  Zip(value:String):void
            {
            
            _zip = value;
            }
          
            public function get  Incareof(): String
            {
            return _incareof;
            }

            public function set  Incareof(value:String):void
            {
            
            _incareof = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Participantaddress = new Participantaddress();
          
          
            object.AddressID = this.AddressID;
          
            object.ParticipantlID = this.ParticipantlID;
          
            object.AddressTypeID = this.AddressTypeID;
          
            object.Line1 = this.Line1;
          
            object.Line2 = this.Line2;
          
            object.City = this.City;
          
            object.State = this.State;
          
            object.Zip = this.Zip;
          
            object.Incareof = this.Incareof;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        AddressID = object["AddressID"];
        ParticipantlID = object["ParticipantlID"];
        AddressTypeID = object["AddressTypeID"];
        Line1 = object["Line1"];
        Line2 = object["Line2"];
        City = object["City"];
        State = object["State"];
        Zip = object["Zip"];
        Incareof = object["Incareof"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Participantaddress;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Participantaddress." 
            
              + AddressID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    