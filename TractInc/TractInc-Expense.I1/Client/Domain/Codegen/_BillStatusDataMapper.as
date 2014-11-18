
      package Domain.Codegen
      {
        import WDM.ActiveRecord;
        import WDM.DataMapper;
        import WDM.DataMapperProxy;
        import WDM.DatabaseAsyncToken;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        public dynamic class _BillStatusDataMapper extends DataMapper
        {
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.BillStatusDataMapper";
          }
        }

      }
    