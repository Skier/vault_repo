
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Participant extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _participantID: int;
      
        protected var _docID: int;
      
        protected var _docRoleID: int;
      
        protected var _asNamed: String;
      
        protected var _phoneHome: String;
      
        protected var _phoneOffice: String;
      
        protected var _phoneCell: String;
      
        protected var _phoneAlt: String;
      
        protected var _entityName: String;
      
        protected var _firstName: String;
      
        protected var _middleName: String;
      
        protected var _lastName: String;
      
        protected var _contactPosition: String;
      
        protected var _tAXID: String;
      
        protected var _sSN: String;
      
        protected var _parentID: int;
      
        protected var _typeId: int;
      
            public function get  ParticipantID(): int
            {
            return _participantID;
            }

            public function set  ParticipantID(value:int):void
            {
            
            _participantID = value;
            }
          
            public function get  DocID(): int
            {
            return _docID;
            }

            public function set  DocID(value:int):void
            {
            
            _docID = value;
            }
          
            public function get  DocRoleID(): int
            {
            return _docRoleID;
            }

            public function set  DocRoleID(value:int):void
            {
            
            _docRoleID = value;
            }
          
            public function get  AsNamed(): String
            {
            return _asNamed;
            }

            public function set  AsNamed(value:String):void
            {
            
            _asNamed = value;
            }
          
            public function get  PhoneHome(): String
            {
            return _phoneHome;
            }

            public function set  PhoneHome(value:String):void
            {
            
            _phoneHome = value;
            }
          
            public function get  PhoneOffice(): String
            {
            return _phoneOffice;
            }

            public function set  PhoneOffice(value:String):void
            {
            
            _phoneOffice = value;
            }
          
            public function get  PhoneCell(): String
            {
            return _phoneCell;
            }

            public function set  PhoneCell(value:String):void
            {
            
            _phoneCell = value;
            }
          
            public function get  PhoneAlt(): String
            {
            return _phoneAlt;
            }

            public function set  PhoneAlt(value:String):void
            {
            
            _phoneAlt = value;
            }
          
            public function get  EntityName(): String
            {
            return _entityName;
            }

            public function set  EntityName(value:String):void
            {
            
            _entityName = value;
            }
          
            public function get  FirstName(): String
            {
            return _firstName;
            }

            public function set  FirstName(value:String):void
            {
            
            _firstName = value;
            }
          
            public function get  MiddleName(): String
            {
            return _middleName;
            }

            public function set  MiddleName(value:String):void
            {
            
            _middleName = value;
            }
          
            public function get  LastName(): String
            {
            return _lastName;
            }

            public function set  LastName(value:String):void
            {
            
            _lastName = value;
            }
          
            public function get  ContactPosition(): String
            {
            return _contactPosition;
            }

            public function set  ContactPosition(value:String):void
            {
            
            _contactPosition = value;
            }
          
            public function get  TAXID(): String
            {
            return _tAXID;
            }

            public function set  TAXID(value:String):void
            {
            
            _tAXID = value;
            }
          
            public function get  SSN(): String
            {
            return _sSN;
            }

            public function set  SSN(value:String):void
            {
            
            _sSN = value;
            }
          
            public function get  ParentID(): int
            {
            return _parentID;
            }

            public function set  ParentID(value:int):void
            {
            
            _parentID = value;
            }
          
            public function get  TypeId(): int
            {
            return _typeId;
            }

            public function set  TypeId(value:int):void
            {
            
            _typeId = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Participant = new Participant();
          
          
            object.ParticipantID = this.ParticipantID;
          
            object.DocID = this.DocID;
          
            object.DocRoleID = this.DocRoleID;
          
            object.AsNamed = this.AsNamed;
          
            object.PhoneHome = this.PhoneHome;
          
            object.PhoneOffice = this.PhoneOffice;
          
            object.PhoneCell = this.PhoneCell;
          
            object.PhoneAlt = this.PhoneAlt;
          
            object.EntityName = this.EntityName;
          
            object.FirstName = this.FirstName;
          
            object.MiddleName = this.MiddleName;
          
            object.LastName = this.LastName;
          
            object.ContactPosition = this.ContactPosition;
          
            object.TAXID = this.TAXID;
          
            object.SSN = this.SSN;
          
            object.ParentID = this.ParentID;
          
            object.TypeId = this.TypeId;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        ParticipantID = object["ParticipantID"];
        DocID = object["DocID"];
        DocRoleID = object["DocRoleID"];
        AsNamed = object["AsNamed"];
        PhoneHome = object["PhoneHome"];
        PhoneOffice = object["PhoneOffice"];
        PhoneCell = object["PhoneCell"];
        PhoneAlt = object["PhoneAlt"];
        EntityName = object["EntityName"];
        FirstName = object["FirstName"];
        MiddleName = object["MiddleName"];
        LastName = object["LastName"];
        ContactPosition = object["ContactPosition"];
        TAXID = object["TAXID"];
        SSN = object["SSN"];
        ParentID = object["ParentID"];
        TypeId = object["TypeId"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Participant;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Participant." 
            
              + ParticipantID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    