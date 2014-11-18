
      package App.Domain
      {
        import App.Domain.Codegen.*;
        import weborb.data.ActiveCollection;
        import weborb.data.DatabaseAsyncToken;
        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        
	      public dynamic class BillItemDataMapper extends _BillItemDataMapper
	      {
	      	
          /*public function getBillItems(billId:int, responder:Responder = null):ActiveCollection
          {
          	
          	// var activeCollection:ActiveCollection = new ActiveCollection();
          	
          	var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().getBillItems(
                billId), responder);
          
          	/*for each (var item:BillItem in activeCollection) {
            	load(item);
           }
           return activeCollection;*/
           //return null;
          //}
        

	      }
      }
    