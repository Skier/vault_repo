
      package App.Domain
      {
        import App.Domain.Codegen.*;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.InvoiceItemStatus")]
        public dynamic class InvoiceItemStatus extends _InvoiceItemStatus
        {
			public static const INVOICE_ITEM_STATUS_NEW: String       = "NEW";
			public static const INVOICE_ITEM_STATUS_SUBMITTED: String = "SUBMITTED";
			public static const INVOICE_ITEM_STATUS_VOID: String      = "VOID";
			public static const INVOICE_ITEM_STATUS_PAID: String      = "PAID";
        }
      }
    