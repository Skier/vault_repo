
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Tract;
      
        public dynamic class _TractDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.TractDataMapper";
          }
          
      		public function load(tract:Tract, responder:Responder = null):Tract
          {
            if(IdentityMap.exists(tract))
              return Tract(IdentityMap.extract(tract.getURI()));

            IdentityMap.add(tract);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              tract.TractID),null,tract);
            
            return tract;
          }
          
      
          public function findByPrimaryKey(  tractID:int):Tract
          {
            var activeRecord:Tract = new Tract();

            
              activeRecord.TractID = tractID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    