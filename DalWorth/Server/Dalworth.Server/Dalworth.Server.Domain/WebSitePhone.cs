using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class WebSitePhone
    {
        
        public WebSitePhone()
        {

        }

        #region FindByPhoneKey

        private const String SqlSelectByPhoneKey = SqlSelectAll +
                        " where PhoneKey=?PhoneKey";

        public static WebSitePhone FindByPhoneKey(string phoneKey, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPhoneKey, connection))
            {

                Database.PutParameter(dbCommand, "?PhoneKey", phoneKey);


                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("WebSitePhone not found, search by PhoneKey");
        }

        #endregion
    }
}
      