using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class UserAsset
    {

        private static UserAsset c_UserAsset = new UserAsset();

        public static UserAsset GetInstance()
        {
            return c_UserAsset;
        }

        private UserAsset()
        {
        }

        private const string SQL_SELECT_BY_USER_ID = @"
            select  [UserAssetId],
                    [UserId],
                    [AssetId]
            from    [UserAsset]
            where   [UserId] = @UserId";

        public UserAssetDataObject GetUserAsset(SqlTransaction tran, int userId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@UserId", userId) };

            UserAssetDataObject userAssetInfo = null;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_USER_ID, parms))
            {
                if (dataReader.Read())
                {
                    userAssetInfo = new UserAssetDataObject();

                    userAssetInfo.UserAssetId = (int)dataReader.GetValue(0);
                    userAssetInfo.UserId = (int)dataReader.GetValue(1);
                    userAssetInfo.AssetId = (int)dataReader.GetValue(2);
                }
            }

            return userAssetInfo;
        }

        private const string SQL_INSERT = @"
        insert  into [UserAsset]
              ( [UserId],
                [AssetId],
                [Deleted] )
        values( @UserId,
                @AssetId,
                0);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, UserAssetDataObject userAssetInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@UserId",       userAssetInfo.UserId),
                new SqlParameter("@AssetId",      userAssetInfo.AssetId)
            };

            userAssetInfo.UserAssetId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

    }

}
