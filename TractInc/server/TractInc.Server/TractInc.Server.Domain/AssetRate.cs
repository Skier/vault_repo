using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class AssetRate
{
    private const string SQL_SELECT_BY_ASSET_ID =
        @"select c.*
            from AssetRate c
           where c.AssetId = @AssetId";
        
    public AssetRate()
    {
    }

    public static List<AssetRate> findByAsset(Asset asset)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                AssetRate.SQL_SELECT_BY_ASSET_ID))
        {
            Database.PutParameter(dbCommand, "@AssetId", asset.AssetId);
            List<AssetRate> result = new List<AssetRate>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
            return result;
        }
    }

    public static void Save(List<AssetRate> rates) {
        Database.Begin();
        try
        {
            foreach(AssetRate assetRate in rates) {
                if ( 0 != assetRate.AssetRateId ) {
                    AssetRate.Update(assetRate);
                } else {
                    AssetRate.Insert(assetRate);
                }
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(List<AssetRate> rates) {
        Database.Begin();
        try
        {
            foreach(AssetRate assetRate in rates) {
                AssetRate.Delete(assetRate);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

}
}
