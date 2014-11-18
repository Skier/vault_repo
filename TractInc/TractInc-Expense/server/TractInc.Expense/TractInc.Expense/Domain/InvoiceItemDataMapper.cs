
          namespace TractInc.Expense.Domain
          {
            public class InvoiceItemDataMapper :_InvoiceItemDataMapper
            {
              public InvoiceItemDataMapper()
              {}
              public InvoiceItemDataMapper(TractIncRAIDDb database):base(database)
              {}
            }
        }
      