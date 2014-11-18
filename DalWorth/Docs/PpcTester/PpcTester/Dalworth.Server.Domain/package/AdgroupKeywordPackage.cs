using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  
namespace Dalworth.Server.Domain.Package
{
    public class AdgroupKeywordPackage
    {
        public Keyword Keyword { get; set; }
        public AdgroupKeyword AdgroupKeyword { get; set;}

        private const string SqlFindByImpressions =
        @"  SELECT k.*, ak.* FROM keyword k
            join adgroupkeyword ak on ak.keywordid = k.id
            join adgroup ag on ag.searchengineid = ak.searchengineid and ag.id = ak.adgroupid
            join keywordstats stats on stats.searchengineid = ak.searchengineid and stats.searchenginekeywordid = ak.searchenginekeywordid
            where ag.campaignid = ?CampaingId
            group by k.id, k.keywordstring
            having sum(stats.impressions) >?MaxImpressions";

        public static List<AdgroupKeywordPackage> FindByImpressions(long campaignId, int maxImpressions, IDbConnection connection)
        {
            List<AdgroupKeywordPackage> rv = new List<AdgroupKeywordPackage>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByImpressions, connection))
            {

                Database.PutParameter(dbCommand, "?CampaingId", campaignId);
                Database.PutParameter(dbCommand, "?MaxImpressions", maxImpressions);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        AdgroupKeywordPackage package = new AdgroupKeywordPackage();
                        package.Keyword = Keyword.Load(dataReader);
                        package.AdgroupKeyword = AdgroupKeyword.Load(dataReader, Keyword.FieldsCount);
                        rv.Add(package);
                    }

                    return rv;
                }
            }
            throw new DataNotFoundException("Keyword not found, search by primary key");
        }
    }
}
