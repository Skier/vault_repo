using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class BillItemStatus
    {

        private static BillItemStatus c_BillItemStatus = new BillItemStatus();

        public static BillItemStatus GetInstance()
        {
            return c_BillItemStatus;
        }

        private BillItemStatus()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [Status]
            from    [BillItemStatus]";

        public List<BillItemStatusDataObject> GetBillItemStatuses(SqlTransaction tran)
        {
            List<BillItemStatusDataObject> result = new List<BillItemStatusDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    BillItemStatusDataObject billItemStatusInfo = new BillItemStatusDataObject();

                    billItemStatusInfo.Status = (string)dataReader.GetValue(0);

                    result.Add(billItemStatusInfo);
                }
            }

            return result;
        }

    }

}
