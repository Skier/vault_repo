
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Participantentityparty;
      
        public dynamic class _ParticipantentitypartyDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.ParticipantentitypartyDataMapper";
          }
          
      		public function load(participantentityparty:Participantentityparty, responder:Responder = null):Participantentityparty
          {
            if(IdentityMap.exists(participantentityparty))
              return Participantentityparty(IdentityMap.extract(participantentityparty.getURI()));

            IdentityMap.add(participantentityparty);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              participantentityparty.ParticipantEntityPartyID),null,participantentityparty);
            
            return participantentityparty;
          }
          
      
          public function findByPrimaryKey(  participantEntityPartyID:int):Participantentityparty
          {
            var activeRecord:Participantentityparty = new Participantentityparty();

            
              activeRecord.ParticipantEntityPartyID = participantEntityPartyID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    