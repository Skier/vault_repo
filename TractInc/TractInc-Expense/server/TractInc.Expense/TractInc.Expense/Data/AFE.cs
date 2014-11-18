using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class AFE
    {

        private static AFE c_Afe = new AFE();

        public static AFE GetInstance()
        {
            return c_Afe;
        }

        private AFE()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [AFE],
                    [ClientId],
                    [AFEName],
                    [AFEStatus],
                    [Deleted]
            from    [AFE]";

        public List<AFEDataObject> GetAFEs(SqlTransaction tran)
        {
            List<AFEDataObject> result = new List<AFEDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    AFEDataObject afeInfo = new AFEDataObject();

                    afeInfo.AFE = (string)dataReader.GetValue(0);
                    afeInfo.ClientId = (int)dataReader.GetValue(1);
                    afeInfo.AFEName = (string)dataReader.GetValue(2);
                    afeInfo.AFEStatus = (string)dataReader.GetValue(3);
                    afeInfo.Deleted = (bool)dataReader.GetValue(4);

                    result.Add(afeInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [AFE]
              ( [AFE],
                [ClientId],
                [AFEName],
                [AFEStatus],
                [Deleted])
        values( @AFE,
                @ClientId,
                @AFEName,
                @AFEStatus,
                @Deleted )";

        public void Insert(SqlTransaction tran, AFEDataObject afeInfo)
        {
            DbParameter[] parms = new DbParameter[5] {
                new SqlParameter("@AFE",       afeInfo.AFE),
                new SqlParameter("@ClientId",  afeInfo.ClientId),
                new SqlParameter("@AFEName",   afeInfo.AFEName),
                new SqlParameter("@AFEStatus", afeInfo.AFEStatus),
                new SqlParameter("@Deleted",   afeInfo.Deleted)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [AFE]
        set     [AFEName]   = @AFEName,
                [AFEStatus] = @AFEStatus,
                [Deleted]   = @Deleted
        where   [AFE]       = @AFE";

        public void Update(SqlTransaction tran, AFEDataObject afeInfo)
        {
            DbParameter[] parms = new DbParameter[5] {
                new SqlParameter("@AFE",       afeInfo.AFE),
                new SqlParameter("@ClientId",  afeInfo.ClientId),
                new SqlParameter("@AFEName",   afeInfo.AFEName),
                new SqlParameter("@AFEStatus", afeInfo.AFEStatus),
                new SqlParameter("@Deleted",   afeInfo.Deleted)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        update  [AFE]
        set     [Deleted] = 1
        where   [AFE] = @AFE";

        public void Remove(SqlTransaction tran, string afe)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@AFE", afe)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_CAN_REMOVE_AFE = @"
        select  *
        from    [BillItem] bi
                inner join [AssetAssignment] aa
                        on bi.[AssetAssignmentId] = aa.[AssetAssignmentId]
        where   bi.[Status] <> 'CONFIRMED'
        and     aa.[AFE] = @Afe";

        public bool CanRemoveAFE(SqlTransaction tran, string afe)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@Afe", afe) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_CAN_REMOVE_AFE, parms))
            {
                if (dataReader.Read())
                {
                    return false;
                }
            }

            return true;
        }

    }

}
