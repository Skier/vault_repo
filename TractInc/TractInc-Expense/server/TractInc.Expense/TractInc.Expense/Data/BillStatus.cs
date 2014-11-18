using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class BillStatus
    {

        private static BillStatus c_BillStatus = new BillStatus();

        public static BillStatus GetInstance()
        {
            return c_BillStatus;
        }

        private BillStatus()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [Status]
            from    [BillStatus]";

        public List<BillStatusDataObject> GetBillStatuses(SqlTransaction tran)
        {
            List<BillStatusDataObject> result = new List<BillStatusDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    BillStatusDataObject billStatusInfo = new BillStatusDataObject();

                    billStatusInfo.Status = (string)dataReader.GetValue(0);

                    result.Add(billStatusInfo);
                }
            }

            return result;
        }

    }

}
