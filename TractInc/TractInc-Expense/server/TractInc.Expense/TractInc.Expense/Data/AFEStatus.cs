using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class AFEStatus
    {

        private static AFEStatus c_AFEStatus = new AFEStatus();

        public static AFEStatus GetInstance()
        {
            return c_AFEStatus;
        }

        private AFEStatus()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [AFEStatus]
            from    [AFEStatus]";

        public List<AFEStatusDataObject> GetAFEStatuses(SqlTransaction tran)
        {
            List<AFEStatusDataObject> result = new List<AFEStatusDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    AFEStatusDataObject afeStatusInfo = new AFEStatusDataObject();

                    afeStatusInfo.Status = (string)dataReader.GetValue(0);

                    result.Add(afeStatusInfo);
                }
            }

            return result;
        }

    }

}
