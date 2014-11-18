
using System;
using System.Data;
using Dalworth.Common.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class ServmanCustomer
    {
        public ServmanCustomer()
        {
        }

        #region GetByRealmId

        private const String SqlSelectByRealmId = "SELECT * FROM ServmanCustomer WHERE RealmId = ?RealmId ";

        public static ServmanCustomer GetByRealmId(string realmId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByRealmId, connection))
            {
                Database.PutParameter(dbCommand, "?RealmId", realmId);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        private const String SqlSelectByTicketId = @"
SELECT c.* FROM ServmanCustomer c
 INNER JOIN ServmanSession s ON s.ServmanCustomerId = c.Id
 WHERE s.Ticket = ?Ticket 
   AND s.IsActive = 1 ";

        public static ServmanCustomer GetByTicketId(string ticket)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByTicketId))
            {
                Database.PutParameter(dbCommand, "?Ticket", ticket);
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }
    }
}
      