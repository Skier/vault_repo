
      package App.Domain
      {
        import App.Domain.Codegen.*;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.BillItemStatus")]
        public dynamic class BillItemStatus extends _BillItemStatus
        {
			public static const BILL_ITEM_STATUS_NEW: String       = "NEW";
			public static const BILL_ITEM_STATUS_SUBMITTED: String = "SUBMITTED";
			public static const BILL_ITEM_STATUS_REJECTED: String  = "REJECTED";
			public static const BILL_ITEM_STATUS_CHANGED: String   = "CHANGED";
			public static const BILL_ITEM_STATUS_CORRECTED: String = "CORRECTED";
			public static const BILL_ITEM_STATUS_APPROVED: String  = "APPROVED";
			public static const BILL_ITEM_STATUS_CONFIRMED: String = "CONFIRMED";
			public static const BILL_ITEM_STATUS_DECLINED: String  = "DECLINED";
			public static const BILL_ITEM_STATUS_VERIFIED: String  = "VERIFIED";
        
        }
      }
    