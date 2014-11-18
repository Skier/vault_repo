using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class Asset
    {

        private static Asset c_Asset = new Asset();

        public static Asset GetInstance()
        {
            return c_Asset;
        }

        private Asset()
        {
        }

        private const string SQL_SELECT_BY_ID = @"
            select  [AssetId],
                    [Type],
                    [ChiefAssetId],
                    [BusinessName],
                    [FirstName],
                    [MiddleName],
                    [LastName],
                    [SSN],
                    [Deleted]
            from    [Asset]
            where   AssetId = @AssetId";

        public AssetDataObject GetAsset(SqlTransaction tran, int assetId)
        {
            AssetDataObject result = null;

            DbParameter assetIdParam = new SqlParameter("@AssetId", assetId);
            DbParameter[] parms = new DbParameter[1] { assetIdParam };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    result = new AssetDataObject();

                    result.AssetId = (int)dataReader.GetValue(0);
                    result.Type = (string)dataReader.GetValue(1);
                    result.ChiefAssetId = (int)dataReader.GetValue(2);
                    result.BusinessName = (string)dataReader.GetValue(3);
                    result.FirstName = (string)dataReader.GetValue(4);
                    result.MiddleName = (string)dataReader.GetValue(5);
                    result.LastName = (string)dataReader.GetValue(6);
                    result.SSN = (string)dataReader.GetValue(7);
                    result.Deleted = (bool)dataReader.GetValue(8);
                }
            }

            return result;
        }

        private const string SQL_SELECT_CURRENT = @"
            select  [AssetId],
                    [Type],
                    [ChiefAssetId],
                    [BusinessName],
                    [FirstName],
                    [MiddleName],
                    [LastName],
                    [SSN],
                    [Deleted]
            from    [Asset]
            where   Deleted = 0";

        public List<AssetDataObject> GetCurrentAssets(SqlTransaction tran)
        {
            List<AssetDataObject> result = new List<AssetDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CURRENT, parms))
            {
                while (dataReader.Read())
                {
                    AssetDataObject assetInfo = new AssetDataObject();

                    assetInfo.AssetId = (int)dataReader.GetValue(0);
                    assetInfo.Type = (string)dataReader.GetValue(1);
                    assetInfo.ChiefAssetId = (int)dataReader.GetValue(2);
                    assetInfo.BusinessName = (string)dataReader.GetValue(3);
                    assetInfo.FirstName = (string)dataReader.GetValue(4);
                    assetInfo.MiddleName = (string)dataReader.GetValue(5);
                    assetInfo.LastName = (string)dataReader.GetValue(6);
                    assetInfo.SSN = (string)dataReader.GetValue(7);
                    assetInfo.Deleted = (bool)dataReader.GetValue(8);

                    assetInfo.DefaultRates = DefaultBillRate.GetInstance().GetAssetRates(tran, assetInfo.AssetId);

                    assetInfo.UserInfo = User.GetInstance().GetUserByAsset(tran, assetInfo.AssetId);

                    result.Add(assetInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_CREW = @"
            select  [AssetId],
                    [Type],
                    [ChiefAssetId],
                    [BusinessName],
                    [FirstName],
                    [MiddleName],
                    [LastName],
                    [SSN],
                    [Deleted]
            from    [Asset]
            where   [Deleted] = 0
            and     [ChiefAssetId] = @ChiefAssetId";

        public List<AssetDataObject> GetCrewAssets(SqlTransaction tran, int chiefAssetId)
        {
            List<AssetDataObject> result = new List<AssetDataObject>();

            DbParameter chiefAssetIdParam = new SqlParameter("@ChiefAssetId", chiefAssetId);
            DbParameter[] parms = new DbParameter[1] { chiefAssetIdParam };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_CREW, parms))
            {
                while (dataReader.Read())
                {
                    AssetDataObject assetInfo = new AssetDataObject();

                    assetInfo.AssetId = (int)dataReader.GetValue(0);
                    assetInfo.Type = (string)dataReader.GetValue(1);
                    assetInfo.ChiefAssetId = (int)dataReader.GetValue(2);
                    assetInfo.BusinessName = (string)dataReader.GetValue(3);
                    assetInfo.FirstName = (string)dataReader.GetValue(4);
                    assetInfo.MiddleName = (string)dataReader.GetValue(5);
                    assetInfo.LastName = (string)dataReader.GetValue(6);
                    assetInfo.SSN = (string)dataReader.GetValue(7);
                    assetInfo.Deleted = (bool)dataReader.GetValue(8);

                    assetInfo.DefaultRates = DefaultBillRate.GetInstance().GetAssetRates(tran, assetInfo.AssetId);

                    assetInfo.Assignments = AssetAssignment.GetInstance().GetAssignments(tran, assetInfo.AssetId);

                    result.Add(assetInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [Asset]
              ( [Type],
                [ChiefAssetId],
                [BusinessName],
                [FirstName],
                [MiddleName],
                [LastName],
                [SSN],
                [Deleted] )
        values( @Type,
                @ChiefAssetId,
                @BusinessName,
                @FirstName,
                @MiddleName,
                @LastName,
                @SSN,
                @Deleted);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, AssetDataObject assetInfo)
        {
            DbParameter[] parms = new DbParameter[8] {
                new SqlParameter("@Type",         assetInfo.Type),
                new SqlParameter("@ChiefAssetId", assetInfo.ChiefAssetId),
                new SqlParameter("@BusinessName", assetInfo.BusinessName),
                new SqlParameter("@FirstName",    assetInfo.FirstName),
                new SqlParameter("@MiddleName",   assetInfo.MiddleName),
                new SqlParameter("@LastName",     assetInfo.LastName),
                new SqlParameter("@SSN",          assetInfo.SSN),
                new SqlParameter("@Deleted",      assetInfo.Deleted)
            };

            assetInfo.AssetId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [Asset]
        set     [Type] = @Type,
                [ChiefAssetId] = @ChiefAssetId,
                [BusinessName] = @BusinessName,
                [FirstName] = @FirstName,
                [MiddleName] = @MiddleName,
                [LastName] = @LastName,
                [SSN] = @SSN,
                [Deleted] = @Deleted
        where   [AssetId] = @AssetId";

        public void Update(SqlTransaction tran, AssetDataObject assetInfo)
        {
            DbParameter[] parms = new DbParameter[9] {
                new SqlParameter("@Type",         assetInfo.Type),
                new SqlParameter("@ChiefAssetId", assetInfo.ChiefAssetId),
                new SqlParameter("@BusinessName", assetInfo.BusinessName),
                new SqlParameter("@FirstName",    assetInfo.FirstName),
                new SqlParameter("@MiddleName",   assetInfo.MiddleName),
                new SqlParameter("@LastName",     assetInfo.LastName),
                new SqlParameter("@SSN",          assetInfo.SSN),
                new SqlParameter("@Deleted",      assetInfo.Deleted),
                new SqlParameter("@AssetId",      assetInfo.AssetId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        update  [Asset]
        set     [Deleted] = 1
        where   [AssetId] = @AssetId";

        public void Remove(SqlTransaction tran, int assetId)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@AssetId", assetId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

        private const string SQL_CAN_REMOVE_ASSET = @"
        select *
          from [BillItem] bi
         		inner join [Bill] b
        				on bi.[BillId] = b.[BillId]
         where bi.[Status] <> 'CONFIRMED'
           and b.[AssetId] = @assetId";

        public bool CanRemoveAsset(SqlTransaction tran, int assetId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssetId", assetId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_CAN_REMOVE_ASSET, parms))
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
