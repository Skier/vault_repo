
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Participanttype;
      
        public dynamic class _ParticipanttypeDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.ParticipanttypeDataMapper";
          }
          
      		public function load(participanttype:Participanttype, responder:Responder = null):Participanttype
          {
            if(IdentityMap.exists(participanttype))
              return Participanttype(IdentityMap.extract(participanttype.getURI()));

            IdentityMap.add(participanttype);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              participanttype.TypeID),null,participanttype);
            
            return participanttype;
          }
          
      
          public function findByPrimaryKey(  typeID:int):Participanttype
          {
            var activeRecord:Participanttype = new Participanttype();

            
              activeRecord.TypeID = typeID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    