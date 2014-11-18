using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class WebUser
    {
        public WebUser(){}

        #region FindBy Login

        private const string SqlFindByLogin =
            @"SELECT * FROM WebUser
                WHERE Login = ?Login";

        public static WebUser FindByLogin(string login, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByLogin, connection))
            {
                Database.PutParameter(dbCommand, "?Login", login);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region FindByPartner

        private const string SqlFindByPartner =
            @"SELECT * FROM WebUser
                WHERE OrderSourceId = ?OrderSourceId";

        public static List<WebUser> FindByPartner(int orderSourceId)
        {
            List<WebUser> webUsers = new List<WebUser>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByPartner))
            {
                Database.PutParameter(dbCommand, "?OrderSourceId", orderSourceId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        webUsers.Add(Load(dataReader));
                }
            }

            return webUsers;
        }

        #endregion

        #region DisplayName

        public string DisplayName
        {
            get
            {
                return Utils.JoinStrings(" ", FirstName, LastName);
            }
        }

        #endregion
    }
}
      