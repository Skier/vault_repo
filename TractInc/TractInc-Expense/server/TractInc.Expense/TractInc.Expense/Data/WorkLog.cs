using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class WorkLog
    {

        private static WorkLog c_WorkLog = new WorkLog();

        public static WorkLog GetInstance()
        {
            return c_WorkLog;
        }

        private WorkLog()
        {
        }

        private const string SQL_UPDATE = @"
        update  [WorkLog]
        set     [LogMessage] = @LogMessage
        where   [BillItemId] = @BillItemId";

        public void Update(SqlTransaction tran, WorkLogDataObject workLogInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@BillItemId", workLogInfo.BillItemId),
                new SqlParameter("@LogMessage", workLogInfo.LogMessage)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_INSERT = @"
        insert  into [WorkLog]
              ( [BillItemId],
                [LogMessage] )
        values( @BillItemId,
                @LogMessage );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, WorkLogDataObject workLogInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@BillItemId", workLogInfo.BillItemId),
                new SqlParameter("@LogMessage", workLogInfo.LogMessage)
            };

            workLogInfo.WorkLogId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_SELECT_BY_BILL_ITEM_ID = @"
        select  [WorkLogId],
                [BillItemId],
                [LogMessage]
        from    [WorkLog]
        where   [BillItemId] = @BillItemId";

        public WorkLogDataObject getWorkLog(SqlTransaction tran, int billItemId)
        {
            WorkLogDataObject result = null;

            DbParameter billItemIdParam = new SqlParameter("@BillItemId", billItemId);
            DbParameter[] parms = new DbParameter[1] { billItemIdParam };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_BILL_ITEM_ID, parms))
            {
                if (dataReader.Read())
                {
                    result = new WorkLogDataObject();

                    result.WorkLogId = (int)dataReader.GetValue(0);
                    result.BillItemId = (int)dataReader.GetValue(1);
                    if (dataReader.IsDBNull(2))
                    {
                        result.LogMessage = null;
                    }
                    else
                    {
                        result.LogMessage = (String)dataReader.GetValue(2);
                    }
                }
            }

            return result;
        }

    }

}
