using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class Customer
    {
        public decimal CurrentBalance { get; set; }
        public List<PhoneBlackList> BlackListPhones { get; set; }

        public Customer()
        {
        }

        #region GetByRealmId

        private const String SqlSelectByRealmId = "SELECT * FROM Customer WHERE RealmId = ?RealmId ";

        public static Customer GetByRealmId(string realmId, IDbConnection connection)
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
SELECT c.* FROM Customer c
 INNER JOIN Session s ON s.CustomerId = c.Id
 WHERE s.Ticket = ?Ticket 
   AND s.IsActive = 1 ";

        public static Customer GetByTicketId(string ticket)
        {
            if (ticket == null)
                return null;

            using (var dbCommand = Database.PrepareCommand(SqlSelectByTicketId))
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
      