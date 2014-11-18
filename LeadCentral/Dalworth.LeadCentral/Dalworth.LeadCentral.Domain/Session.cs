
using System;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class Session
    {
        public Session()
        {
        }

        private const String SqlSelectByTicketId = @"
SELECT * FROM Session
 WHERE Ticket = ?Ticket 
   AND IsActive = 1 ";

        public static Session GetByTicket(string ticket)
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
SELECT * FROM Session
 WHERE IntuitTicket = ?IntuitTicket ";

        public static Session GetByIntuitTicket(string intuitTicket)
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
UPDATE Session SET IsActive = 0
 WHERE QbUserId = ?QbUserId ";

        public static void DeactivateByUserId(string qbUserId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeactivateByUserId))
            {
                Database.PutParameter(dbCommand, "?QbUserId", qbUserId);
                dbCommand.ExecuteNonQuery();
            }
        }

    }
}
      