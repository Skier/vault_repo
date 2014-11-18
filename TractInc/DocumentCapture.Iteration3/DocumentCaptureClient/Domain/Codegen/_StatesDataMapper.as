
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.States;
      
        public dynamic class _StatesDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.StatesDataMapper";
          }
          
      		public function load(states:States, responder:Responder = null):States
          {
            if(IdentityMap.exists(states))
              return States(IdentityMap.extract(states.getURI()));

            IdentityMap.add(states);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              states.STATE_ID),null,states);
            
            return states;
          }
          
      
          public function findByPrimaryKey(  sTATE_ID:int):States
          {
            var activeRecord:States = new States();

            
              activeRecord.STATE_ID = sTATE_ID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    