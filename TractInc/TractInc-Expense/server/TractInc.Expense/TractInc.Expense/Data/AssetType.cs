using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class AssetType
    {

        private static AssetType c_AssetType = new AssetType();

        public static AssetType GetInstance()
        {
            return c_AssetType;
        }

        private AssetType()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [Type]
            from    [AssetType]";

        public List<AssetTypeDataObject> GetAssetTypes(SqlTransaction tran)
        {
            List<AssetTypeDataObject> result = new List<AssetTypeDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    AssetTypeDataObject assetTypeInfo = new AssetTypeDataObject();

                    assetTypeInfo.Type = (string)dataReader.GetValue(0);

                    result.Add(assetTypeInfo);
                }
            }

            return result;
        }

    }

}
