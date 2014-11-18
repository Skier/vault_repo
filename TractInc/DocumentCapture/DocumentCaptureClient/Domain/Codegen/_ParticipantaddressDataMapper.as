
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Participantaddress;
      
        public dynamic class _ParticipantaddressDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.ParticipantaddressDataMapper";
          }
          
      		public function load(participantaddress:Participantaddress, responder:Responder = null):Participantaddress
          {
            if(IdentityMap.exists(participantaddress))
              return Participantaddress(IdentityMap.extract(participantaddress.getURI()));

            IdentityMap.add(participantaddress);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              participantaddress.AddressID),null,participantaddress);
            
            return participantaddress;
          }
          
      
          public function findByPrimaryKey(  addressID:int):Participantaddress
          {
            var activeRecord:Participantaddress = new Participantaddress();

            
              activeRecord.AddressID = addressID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    