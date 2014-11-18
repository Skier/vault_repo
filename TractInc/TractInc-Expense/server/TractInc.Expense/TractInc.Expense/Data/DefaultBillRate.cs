using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class DefaultBillRate
    {

        private static DefaultBillRate c_DefaultBillRate = new DefaultBillRate();

        public static DefaultBillRate GetInstance()
        {
            return c_DefaultBillRate;
        }

        private DefaultBillRate()
        {
        }

        private const string SQL_SELECT_BY_ASSET = @"
            select  [DefaultBillRateId],
                    [AssetId],
                    [BillItemTypeId],
                    [BillRate]
            from    [DefaultBillRate]
            where   [AssetId] = @AssetId";

        public List<DefaultBillRateDataObject> GetAssetRates(SqlTransaction tran, int assetId)
        {
            List<DefaultBillRateDataObject> result = new List<DefaultBillRateDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssetId", assetId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ASSET, parms))
            {
                while (dataReader.Read())
                {
                    DefaultBillRateDataObject assetRateInfo = new DefaultBillRateDataObject();

                    assetRateInfo.DefaultBillRateId = (int)dataReader.GetValue(0);
                    assetRateInfo.AssetId = (int)dataReader.GetValue(1);
                    assetRateInfo.BillItemTypeId = (int)dataReader.GetValue(2);
                    assetRateInfo.BillRate = (decimal)dataReader.GetValue(3);

                    result.Add(assetRateInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [DefaultBillRate]
              ( [AssetId],
                [BillItemTypeId],
                [BillRate] )
        values( @AssetId,
                @BillItemTypeId,
                @BillRate );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, DefaultBillRateDataObject rateInfo)
        {
            DbParameter[] parms = new DbParameter[3] {
                new SqlParameter("@AssetId",        rateInfo.AssetId),
                new SqlParameter("@BillItemTypeId", rateInfo.BillItemTypeId),
                new SqlParameter("@BillRate",       rateInfo.BillRate)
            };

            rateInfo.DefaultBillRateId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [DefaultBillRate]
        set     [BillRate] = @BillRate
        where   [DefaultBillRateId] = @DefaultBillRateId";

        public void Update(SqlTransaction tran, DefaultBillRateDataObject rateInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@DefaultBillRateId", rateInfo.DefaultBillRateId),
                new SqlParameter("@BillRate",          rateInfo.BillRate)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        delete  from [DefaultBillRate]
        where   [DefaultBillRateId] = @DefaultBillRateId";

        public void Remove(SqlTransaction tran, DefaultBillRateDataObject rateInfo)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@DefaultBillRateId", rateInfo.DefaultBillRateId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);

            rateInfo.DefaultBillRateId = 0;
        }

    }

}
