
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Participanttype extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _typeID: int;
      
        protected var _name: String;
      
            public function get  TypeID(): int
            {
            return _typeID;
            }

            public function set  TypeID(value:int):void
            {
            
            _typeID = value;
            }
          
            public function get  Name(): String
            {
            return _name;
            }

            public function set  Name(value:String):void
            {
            
            _name = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Participanttype = new Participanttype();
          
          
            object.TypeID = this.TypeID;
          
            object.Name = this.Name;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        TypeID = object["TypeID"];
        Name = object["Name"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Participanttype;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Participanttype." 
            
              + TypeID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    