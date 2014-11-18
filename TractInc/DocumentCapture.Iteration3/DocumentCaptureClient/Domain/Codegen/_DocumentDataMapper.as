
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Document;
      
        public dynamic class _DocumentDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.DocumentDataMapper";
          }
          
      		public function load(document:Document, responder:Responder = null):Document
          {
            if(IdentityMap.exists(document))
              return Document(IdentityMap.extract(document.getURI()));

            IdentityMap.add(document);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              document.DocID),null,document);
            
            return document;
          }
          
      
          public function findByPrimaryKey(  docID:int):Document
          {
            var activeRecord:Document = new Document();

            
              activeRecord.DocID = docID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    