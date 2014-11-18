
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Participantrole extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _docRoleID: int;
      
        protected var _roleName: String;
      
        protected var _docTypeID: int;
      
        protected var _isSeller: Boolean;
      
            public function get  DocRoleID(): int
            {
            return _docRoleID;
            }

            public function set  DocRoleID(value:int):void
            {
            
            _docRoleID = value;
            }
          
            public function get  RoleName(): String
            {
            return _roleName;
            }

            public function set  RoleName(value:String):void
            {
            
            _roleName = value;
            }
          
            public function get  DocTypeID(): int
            {
            return _docTypeID;
            }

            public function set  DocTypeID(value:int):void
            {
            
            _docTypeID = value;
            }
          
            public function get  IsSeller(): Boolean
            {
            return _isSeller;
            }

            public function set  IsSeller(value:Boolean):void
            {
            
            _isSeller = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Participantrole = new Participantrole();
          
          
            object.DocRoleID = this.DocRoleID;
          
            object.RoleName = this.RoleName;
          
            object.DocTypeID = this.DocTypeID;
          
            object.IsSeller = this.IsSeller;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        DocRoleID = object["DocRoleID"];
        RoleName = object["RoleName"];
        DocTypeID = object["DocTypeID"];
        IsSeller = object["IsSeller"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Participantrole;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Participantrole." 
            
              + DocRoleID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    