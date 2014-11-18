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
    public partial class Campaign
    {
        public Campaign()
        {

        }

        #region Find
      

        public static List<Campaign> Find(long companyId, int status, IDbConnection connection)
        {
            String sql = SqlSelectAll + " where CompanyId = ?CompanyId and Status = ?Status";

            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                Database.PutParameter(dbCommand, "CompanyId", companyId);
                Database.PutParameter(dbCommand, "Status", status);

                List<Campaign> rv = new List<Campaign>();

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

        #endregion 
    }
}
      