
      package Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import Domain.Addresstype;
      
        public dynamic class _AddresstypeDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.DocCapture.Domain.AddresstypeDataMapper";
          }
          
      		public function load(addresstype:Addresstype, responder:Responder = null):Addresstype
          {
            if(IdentityMap.exists(addresstype))
              return Addresstype(IdentityMap.extract(addresstype.getURI()));

            IdentityMap.add(addresstype);
      
            var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
              addresstype.AddressTypeID),null,addresstype);
            
            return addresstype;
          }
          
      
          public function findByPrimaryKey(  addressTypeID:int):Addresstype
          {
            var activeRecord:Addresstype = new Addresstype();

            
              activeRecord.AddressTypeID = addressTypeID;
            
            
            
            
            return load(activeRecord);
          }


        }

      }
    