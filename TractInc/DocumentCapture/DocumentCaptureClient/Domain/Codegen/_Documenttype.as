
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Documenttype extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _docTypeID: int;
      
        protected var _name: String;
      
        protected var _sellerRole: String;
      
        protected var _buyerRole: String;
      
            public function get  DocTypeID(): int
            {
            return _docTypeID;
            }

            public function set  DocTypeID(value:int):void
            {
            
            _docTypeID = value;
            }
          
            public function get  Name(): String
            {
            return _name;
            }

            public function set  Name(value:String):void
            {
            
            _name = value;
            }
          
            public function get  SellerRole(): String
            {
            return _sellerRole;
            }

            public function set  SellerRole(value:String):void
            {
            
            _sellerRole = value;
            }
          
            public function get  BuyerRole(): String
            {
            return _buyerRole;
            }

            public function set  BuyerRole(value:String):void
            {
            
            _buyerRole = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Documenttype = new Documenttype();
          
          
            object.DocTypeID = this.DocTypeID;
          
            object.Name = this.Name;
          
            object.SellerRole = this.SellerRole;
          
            object.BuyerRole = this.BuyerRole;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        DocTypeID = object["DocTypeID"];
        Name = object["Name"];
        SellerRole = object["SellerRole"];
        BuyerRole = object["BuyerRole"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Documenttype;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Documenttype." 
            
              + DocTypeID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    