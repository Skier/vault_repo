
      package App.Domain
      {
        import App.Domain.Codegen.*;
        
        [Bindable]
        [RemoteClass(alias="TractInc.Expense.Domain.InvoiceStatus")]
        public dynamic class InvoiceStatus extends _InvoiceStatus
        {
			public static const INVOICE_STATUS_NEW: String       = "NEW";
			public static const INVOICE_STATUS_SUBMITTED: String = "SUBMITTED";
			public static const INVOICE_STATUS_PAID: String      = "PAID";
			public static const INVOICE_STATUS_VOID: String      = "VOID";
        }
      }
    