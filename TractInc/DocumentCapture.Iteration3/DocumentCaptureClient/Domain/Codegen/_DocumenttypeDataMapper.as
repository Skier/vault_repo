
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Documenttype;
      
        public dynamic class _DocumenttypeDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.DocumenttypeDataMapper";
          }
          
      		public function load(documenttype:Documenttype, responder:Responder = null):Documenttype
          {
            if(IdentityMap.exists(documenttype))
              return Documenttype(IdentityMap.extract(documenttype.getURI()));

            IdentityMap.add(documenttype);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              documenttype.DocTypeID),null,documenttype);
            
            return documenttype;
          }
          
      
          public function findByPrimaryKey(  docTypeID:int):Documenttype
          {
            var activeRecord:Documenttype = new Documenttype();

            
              activeRecord.DocTypeID = docTypeID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    