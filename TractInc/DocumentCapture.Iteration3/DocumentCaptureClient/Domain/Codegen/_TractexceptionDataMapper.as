
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Tractexception;
      
        public dynamic class _TractexceptionDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.TractexceptionDataMapper";
          }
          
      		public function load(tractexception:Tractexception, responder:Responder = null):Tractexception
          {
            if(IdentityMap.exists(tractexception))
              return Tractexception(IdentityMap.extract(tractexception.getURI()));

            IdentityMap.add(tractexception);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              tractexception.TractExceptionsID),null,tractexception);
            
            return tractexception;
          }
          
      
          public function findByPrimaryKey(  tractExceptionsID:int):Tractexception
          {
            var activeRecord:Tractexception = new Tractexception();

            
              activeRecord.TractExceptionsID = tractExceptionsID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    