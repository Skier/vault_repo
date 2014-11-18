using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class PartnerInvitation
    {
        public PartnerInvitation(){}

        #region IsPasswordReset

        public bool IsPasswordReset()
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                if (WebUserId.HasValue)
                    return WebUser.FindByPrimaryKey(WebUserId.Value).PasswordHash != string.Empty;
                return false;
            }
        }

        #endregion

        #region FindByWebUser

        private const string SqlFindByWebUser =
            @"SELECT * FROM PartnerInvitation
                WHERE WebUserId = ?WebUserId";

        public static List<PartnerInvitation> FindByWebUser(int webUserId, IDbConnection connection)
        {
            List<PartnerInvitation> result = new List<PartnerInvitation>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWebUser, connection))
            {
                Database.PutParameter(dbCommand, "?WebUserId", webUserId);

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
      