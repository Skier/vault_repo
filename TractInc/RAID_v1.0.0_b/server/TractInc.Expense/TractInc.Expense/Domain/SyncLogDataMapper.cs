
          namespace TractInc.Expense.Domain
          {
            public class SyncLogDataMapper :_SyncLogDataMapper
            {
              public SyncLogDataMapper()
              {}
              public SyncLogDataMapper(TractIncRAIDDb database):base(database)
              {}
            }
        }
      