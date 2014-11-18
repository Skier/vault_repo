using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
 
namespace Dalworth.Server.Domain
{
    public partial class KeywordStats
    {
        public KeywordStats()
        {

        }

        private const string SqlGetAvailableStatDates = @"
            SELECT ks.DateStart FROM keywordstats ks
            join adgroupkeyword agk on agk.searchengineid = ks.searchengineid and agk.searchenginekeywordid = ks.searchenginekeywordid
            join adgroup ag  on ag.id = agk.adgroupid
            where ag.campaignid = ?CampaignId and ag.searchengineid = ?SearchEngineId and ks.DateStart >= ?LookFromDate
            group by datestart
            order by datestart
            ";

        public static List<DateTime> FindAvailableStatsDates(long campaignId, int searchEngineId, DateTime lookFromDate, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetAvailableStatDates,  connection))
            {
                Database.PutParameter(dbCommand, "?CampaignId", campaignId);
                Database.PutParameter(dbCommand, "?SearchEngineId", searchEngineId);
                Database.PutParameter(dbCommand, "?LookFromDate", lookFromDate);

                List<DateTime> rv = new List<DateTime>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(dataReader.GetDateTime(0));
                    }
                }

                return rv;
            }
        }

        public static List<KeywordStats> Find(int searchEngineId, long searchEngineKeywordId, DateTime dateStart, IDbConnection connection)
        {
            string sql = SqlSelectAll + " where searchengineid = ?SearchEngineId and searchenginekeywordid = ?SearchEngineKeywordId and Datestart = ?DateStart";
            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                Database.PutParameter(dbCommand, "?SearchEngineId", searchEngineId);
                Database.PutParameter(dbCommand, "?SearchEngineKeywordId", searchEngineKeywordId);
                Database.PutParameter(dbCommand, "?Datestart", dateStart);

                List<KeywordStats> rv = new List<KeywordStats>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }
                }

                return rv;
            }

        }
    }
}
      