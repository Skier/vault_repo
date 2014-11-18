
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Participantentityparty extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _participantEntityPartyID: int;
      
        protected var _participantID: int;
      
        protected var _fName: String;
      
        protected var _mName: String;
      
        protected var _lName: String;
      
        protected var _sSN: String;
      
            public function get  ParticipantEntityPartyID(): int
            {
            return _participantEntityPartyID;
            }

            public function set  ParticipantEntityPartyID(value:int):void
            {
            
            _participantEntityPartyID = value;
            }
          
            public function get  ParticipantID(): int
            {
            return _participantID;
            }

            public function set  ParticipantID(value:int):void
            {
            
            _participantID = value;
            }
          
            public function get  fName(): String
            {
            return _fName;
            }

            public function set  fName(value:String):void
            {
            
            _fName = value;
            }
          
            public function get  mName(): String
            {
            return _mName;
            }

            public function set  mName(value:String):void
            {
            
            _mName = value;
            }
          
            public function get  lName(): String
            {
            return _lName;
            }

            public function set  lName(value:String):void
            {
            
            _lName = value;
            }
          
            public function get  SSN(): String
            {
            return _sSN;
            }

            public function set  SSN(value:String):void
            {
            
            _sSN = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Participantentityparty = new Participantentityparty();
          
          
            object.ParticipantEntityPartyID = this.ParticipantEntityPartyID;
          
            object.ParticipantID = this.ParticipantID;
          
            object.fName = this.fName;
          
            object.mName = this.mName;
          
            object.lName = this.lName;
          
            object.SSN = this.SSN;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        ParticipantEntityPartyID = object["ParticipantEntityPartyID"];
        ParticipantID = object["ParticipantID"];
        fName = object["fName"];
        mName = object["mName"];
        lName = object["lName"];
        SSN = object["SSN"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Participantentityparty;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Participantentityparty." 
            
              + ParticipantEntityPartyID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    