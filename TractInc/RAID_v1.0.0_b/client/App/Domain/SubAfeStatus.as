
      package App.Domain
      {
        import App.Domain.Codegen.*;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.SubAfeStatus")]
        public dynamic class SubAfeStatus extends _SubAfeStatus
        {
			public static const SUBAFE_STATUS_ISSUED: String   = "ISSUED";
			public static const SUBAFE_STATUS_EXPIRED: String  = "EXPIRED";
			public static const SUBAFE_STATUS_LOCKED: String   = "LOCKED";
			public static const SUBAFE_STATUS_UNLOCKED: String = "UNLOCKED";
        }
      }
    