using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Asset
{
    private const string SQL_SELECT_BY_COMPANY_ID =
        @"select a.*
            from Asset a
           where a.CompanyId = @CompanyId";

    public List<AssetRate> AssetRateList = null;

    public Asset()
    {
    }

    public static List<Asset> findByCompany(Company company, bool populate)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Asset.SQL_SELECT_BY_COMPANY_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            List<Asset> result = new List<Asset>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }

            if ( populate ) {
                foreach (Asset asset in result) {
                    asset.AssetRateList = AssetRate.findByAsset(asset);
                }
            }

            return result;
        }
    }

    public static void Save(Asset asset) {
        Database.Begin();
        try
        {
            if ( 0 != asset.AssetId ) {
                Asset.Update(asset);
            } else {
                Asset.Insert(asset);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(Asset asset) {
        Database.Begin();
        try
        {
            Asset.Delete(asset);
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
      