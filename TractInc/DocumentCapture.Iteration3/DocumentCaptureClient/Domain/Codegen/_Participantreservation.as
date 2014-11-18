
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Participantreservation extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _docReservationID: int;
      
        protected var _participantID: int;
      
        protected var _details: String;
      
            public function get  DocReservationID(): int
            {
            return _docReservationID;
            }

            public function set  DocReservationID(value:int):void
            {
            
            _docReservationID = value;
            }
          
            public function get  ParticipantID(): int
            {
            return _participantID;
            }

            public function set  ParticipantID(value:int):void
            {
            
            _participantID = value;
            }
          
            public function get  Details(): String
            {
            return _details;
            }

            public function set  Details(value:String):void
            {
            
            _details = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Participantreservation = new Participantreservation();
          
          
            object.DocReservationID = this.DocReservationID;
          
            object.ParticipantID = this.ParticipantID;
          
            object.Details = this.Details;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        DocReservationID = object["DocReservationID"];
        ParticipantID = object["ParticipantID"];
        Details = object["Details"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Participantreservation;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Participantreservation." 
            
              + DocReservationID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    