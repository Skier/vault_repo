using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class WebSiteArticlePart
    {

        public WebSiteArticlePart()
        {

        }

        #region FindByArticleId
        private const String SqlSelectByArticleId = 
          "Select "
          + " WebSiteArticleId, "
          + " WebSiteArticlePartTypeId, "
          + " ContentText "
        + " From WebSiteArticlePart "
        + " where WebSiteArticleId = ?WebSiteArticleId ";

        public static List<WebSiteArticlePart> FindByArticleId(int webSiteArticleId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByArticleId, connection))
            {
                Database.PutParameter(dbCommand, "?WebSiteArticleId", webSiteArticleId);

                List<WebSiteArticlePart> rv = new List<WebSiteArticlePart>();

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
      