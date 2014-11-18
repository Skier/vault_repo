using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class RateByAssignment
    {

        private static RateByAssignment c_RateByAssignment = new RateByAssignment();

        public static RateByAssignment GetInstance()
        {
            return c_RateByAssignment;
        }

        private RateByAssignment()
        {
        }

        private const string SQL_SELECT_BY_ASSIGNMENT = @"
            select  [RateByAssignmentId],
                    [AssetAssignmentId],
                    [BillItemTypeId],
                    [BillRate],
                    [InvoiceRate],
                    [ShouldNotExceedRate],
                    [Deleted]
            from    [RateByAssignment]
            where   [AssetAssignmentId] = @AssetAssignmentId";

        public List<RateByAssignmentDataObject> GetRatesByAssignment(SqlTransaction tran, int assignmentId)
        {
            List<RateByAssignmentDataObject> result = new List<RateByAssignmentDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssetAssignmentId", assignmentId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ASSIGNMENT, parms))
            {
                while (dataReader.Read())
                {
                    RateByAssignmentDataObject rateInfo = new RateByAssignmentDataObject();

                    rateInfo.RateByAssignmentId = (int)dataReader.GetValue(0);
                    rateInfo.AssetAssignmentId = (int)dataReader.GetValue(1);
                    rateInfo.BillItemTypeId = (int)dataReader.GetValue(2);
                    rateInfo.BillRate = (decimal)dataReader.GetValue(3);
                    rateInfo.InvoiceRate = (decimal)dataReader.GetValue(4);
                    rateInfo.ShouldNotExceedRate = (bool)dataReader.GetValue(5);
                    rateInfo.Deleted = (bool)dataReader.GetValue(6);

                    result.Add(rateInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [RateByAssignment]
              ( [AssetAssignmentId],
                [BillItemTypeId],
                [BillRate],
                [InvoiceRate],
                [ShouldNotExceedRate],
                [Deleted] )
        values( @AssetAssignmentId,
                @BillItemTypeId,
                @BillRate,
                @InvoiceRate,
                @ShouldNotExceedRate,
                @Deleted );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, RateByAssignmentDataObject assignmentRateInfo)
        {
            DbParameter[] parms = new DbParameter[6] {
                new SqlParameter("@AssetAssignmentId",      assignmentRateInfo.AssetAssignmentId),
                new SqlParameter("@BillItemTypeId",         assignmentRateInfo.BillItemTypeId),
                new SqlParameter("@BillRate",               assignmentRateInfo.BillRate),
                new SqlParameter("@InvoiceRate",            assignmentRateInfo.InvoiceRate),
                new SqlParameter("@ShouldNotExceedRate",    assignmentRateInfo.ShouldNotExceedRate),
                new SqlParameter("@Deleted",                assignmentRateInfo.Deleted)
            };

            assignmentRateInfo.RateByAssignmentId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_REMOVE = @"
        delete  from [RateByAssignment]
        where   [RateByAssignmentId] = @RateByAssignmentId";

        public void Remove(SqlTransaction tran, int assignmentRateId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@RateByAssignmentId", assignmentRateId) };
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_UPDATE = @"
        update  [RateByAssignment]
        set     [BillRate] = @BillRate,
                [InvoiceRate] = @InvoiceRate,
                [ShouldNotExceedRate] = @ShouldNotExceedRate
        where   [RateByAssignmentId] = @RateByAssignmentId";

        public void Update(SqlTransaction tran, RateByAssignmentDataObject assignmentRateInfo)
        {
            DbParameter[] parms = new DbParameter[4] {
                new SqlParameter("@RateByAssignmentId",     assignmentRateInfo.RateByAssignmentId),
                new SqlParameter("@BillRate",               assignmentRateInfo.BillRate),
                new SqlParameter("@InvoiceRate",            assignmentRateInfo.InvoiceRate),
                new SqlParameter("@ShouldNotExceedRate",    assignmentRateInfo.ShouldNotExceedRate)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_SELECT_BY_BILL_ITEM_ID = @"
            select  r.[RateByAssignmentId],
                    r.[AssetAssignmentId],
                    r.[BillItemTypeId],
                    r.[BillRate],
                    r.[InvoiceRate],
                    r.[ShouldNotExceedRate],
                    r.[Deleted]
            from    [RateByAssignment] r
                    inner join [BillItem] bi
                            on bi.[AssetAssignmentId] = r.[AssetAssignmentId]
                           and bi.[BillItemTypeId] = r.[BillItemTypeId]
            where   bi.[BillItemId] = @BillItemId";

        public RateByAssignmentDataObject GetRateByBillItemId(SqlTransaction tran, int billItemId)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@BillItemId", billItemId)
            };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_BILL_ITEM_ID, parms))
            {
                if (dataReader.Read()) {
                    RateByAssignmentDataObject rateInfo = new RateByAssignmentDataObject();

                    rateInfo.RateByAssignmentId = (int)dataReader.GetValue(0);
                    rateInfo.AssetAssignmentId = (int)dataReader.GetValue(1);
                    rateInfo.BillItemTypeId = (int)dataReader.GetValue(2);
                    rateInfo.BillRate = (decimal)dataReader.GetValue(3);
                    rateInfo.InvoiceRate = (decimal)dataReader.GetValue(4);
                    rateInfo.ShouldNotExceedRate = (bool)dataReader.GetValue(5);
                    rateInfo.Deleted = (bool)dataReader.GetValue(6);

                    return rateInfo;
                }
            }

            return null;
        }

    }

}
