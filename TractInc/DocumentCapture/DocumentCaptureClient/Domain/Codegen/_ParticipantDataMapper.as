
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Participant;
      
        public dynamic class _ParticipantDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.ParticipantDataMapper";
          }
          
      		public function load(participant:Participant, responder:Responder = null):Participant
          {
            if(IdentityMap.exists(participant))
              return Participant(IdentityMap.extract(participant.getURI()));

            IdentityMap.add(participant);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              participant.ParticipantID),null,participant);
            
            return participant;
          }
          
      
          public function findByPrimaryKey(  participantID:int):Participant
          {
            var activeRecord:Participant = new Participant();

            
              activeRecord.ParticipantID = participantID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    