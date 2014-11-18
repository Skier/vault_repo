
      package App.Domain
      {
        import App.Domain.Codegen.*;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.AfeStatus")]
        public dynamic class AfeStatus extends _AfeStatus
        {
			public static const AFE_STATUS_ISSUED: String   = "ISSUED";
			public static const AFE_STATUS_EXPIRED: String  = "EXPIRED";
			public static const AFE_STATUS_LOCKED: String   = "LOCKED";
			public static const AFE_STATUS_UNLOCKED: String = "UNLOCKED";
        }
      }
    