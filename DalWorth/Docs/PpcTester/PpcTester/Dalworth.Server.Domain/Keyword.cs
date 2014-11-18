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
    public partial class Keyword
    {
        public Keyword()
        {

        }

        #region FindByPrimaryKey

        private const string SqlSelectByKeywordString = "Select "


            + " Id, "

            + " KeywordString "


          + " From Keyword "


            + " Where "

              + " KeywordString = ?KeywordString "

          ;

        public static Keyword FindByKeywordString(
            string keywordString, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByKeywordString, connection))
            {

                Database.PutParameter(dbCommand, "?KeywordString", keywordString);


                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("Keyword not found, search by primary key");
        }

        #endregion 

       /* #region FindByImpressions

        private const string SqlSelectByNumberOfImpressions = @"
            SELECT k.id, k.keywordstring FROM keyword k
            join adgroupkeyword ak on ak.keywordid = k.id
            join adgroup ag on ag.searchengineid = ak.searchengineid and ag.id = ak.adgroupid
            join keywordstats stats on stats.searchengineid = ak.searchengineid and stats.searchenginekeywordid = ak.searchenginekeywordid
            where ag.campaignid = ?CampaingId
            group by k.id, k.keywordstring
            having sum(stats.impressions) >?MaxImpressions";

        public static List<Keyword> FindByImpressions(
            long campaignId, int maxImpressions, IDbConnection connection)
        {
            List<Keyword> rv = new List<Keyword>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByNumberOfImpressions, connection))
            {

                Database.PutParameter(dbCommand, "?CampaingId", campaignId);
                Database.PutParameter(dbCommand, "?MaxImpressions", maxImpressions);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                    return rv;
                }
            }
            throw new DataNotFoundException("Keyword not found, search by primary key");
        }

        #endregion */
    }
}


      