
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Participantrole;
      
        public dynamic class _ParticipantroleDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.ParticipantroleDataMapper";
          }
          
      		public function load(participantrole:Participantrole, responder:Responder = null):Participantrole
          {
            if(IdentityMap.exists(participantrole))
              return Participantrole(IdentityMap.extract(participantrole.getURI()));

            IdentityMap.add(participantrole);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              participantrole.DocRoleID),null,participantrole);
            
            return participantrole;
          }
          
      
          public function findByPrimaryKey(  docRoleID:int):Participantrole
          {
            var activeRecord:Participantrole = new Participantrole();

            
              activeRecord.DocRoleID = docRoleID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    