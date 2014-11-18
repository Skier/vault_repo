
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Participantreservation;
      
        public dynamic class _ParticipantreservationDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.ParticipantreservationDataMapper";
          }
          
      		public function load(participantreservation:Participantreservation, responder:Responder = null):Participantreservation
          {
            if(IdentityMap.exists(participantreservation))
              return Participantreservation(IdentityMap.extract(participantreservation.getURI()));

            IdentityMap.add(participantreservation);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              participantreservation.DocReservationID),null,participantreservation);
            
            return participantreservation;
          }
          
      
          public function findByPrimaryKey(  docReservationID:int):Participantreservation
          {
            var activeRecord:Participantreservation = new Participantreservation();

            
              activeRecord.DocReservationID = docReservationID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    