using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class ad_src
    {
        public ad_src(){ }

        #region FindNewAdSources

        private const string SqlFindNewAdSources =
            @"select * from ad_src 
                where Id_code > ?
             order by Id_code";

        public static List<ad_src> FindNewAdSources(string lastAdSourceId)
        {
            List<ad_src> result = new List<ad_src>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNewAdSources, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@Id_code", lastAdSourceId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion
    }
}
      