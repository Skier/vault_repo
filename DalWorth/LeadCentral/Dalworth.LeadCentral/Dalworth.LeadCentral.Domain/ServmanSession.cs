
using System;
using System.Data;
using Dalworth.LeadCentral.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class ServmanSession
    {
        public ServmanSession()
        {
        }

        private const String SqlSelectByTicketId = @"
SELECT * FROM ServmanSession
 WHERE Ticket = ?Ticket 
   AND IsActive = 1 ";

        public static ServmanSession GetByTicket(string ticket)
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

        private const String SqlSelectByIntuiTicket = @"
SELECT * FROM ServmanSession
 WHERE IntuitTicket = ?IntuitTicket ";

        public static ServmanSession GetByIntuitTicket(string intuitTicket)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByIntuiTicket))
            {
                Database.PutParameter(dbCommand, "?IntuitTicket", intuitTicket);
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlDeactivateByUserId = @"
UPDATE ServmanSession SET IsActive = 0
 WHERE QbUserId = ?QbUserId ";

        public static ServmanSession DeactivateByUserId(string qbUserId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeactivateByUserId))
            {
                Database.PutParameter(dbCommand, "?QbUserId", qbUserId);
                dbCommand.ExecuteNonQuery();
            }

            return null;
        }

    }
}
      