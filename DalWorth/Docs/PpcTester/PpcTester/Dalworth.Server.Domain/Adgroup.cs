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
    public partial class AdGroup
    {
        public AdGroup()
        {

        }

        public static List<AdGroup> Find(int searchEngineId, long campaignId,  IDbConnection connection)
        {
            string sql = SqlSelectAll + " where searchengineid = ?SearchEngineId and campaignid = ?CampaignId";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
                Database.PutParameter(dbCommand, "SearchEngineId", searchEngineId);
                Database.PutParameter(dbCommand, "CampaignId", campaignId);

                List<AdGroup> rv = new List<AdGroup>();

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
      