
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Countys;
      
        public dynamic class _CountysDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.CountysDataMapper";
          }
          
      		public function load(countys:Countys, responder:Responder = null):Countys
          {
            if(IdentityMap.exists(countys))
              return Countys(IdentityMap.extract(countys.getURI()));

            IdentityMap.add(countys);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              countys.OBJECTID),null,countys);
            
            return countys;
          }
          
      
          public function findByPrimaryKey(  oBJECTID:int):Countys
          {
            var activeRecord:Countys = new Countys();

            
              activeRecord.OBJECTID = oBJECTID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    