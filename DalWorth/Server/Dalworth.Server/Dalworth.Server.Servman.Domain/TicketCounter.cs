using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class TicketCounter
    {
        public TicketCounter(){ }

        #region FindNextOrderNumberAndIncrement

        private const String SqlLockTicket
            = @"SELECT RLOCK('1', 'ticket') FROM 'ticket.dbf'";

        private const String SqlFindTicketNumber
            = @"SELECT * from 'ticket.dbf'";

        private const String SqlIncrementAndSave
            = @"update 'ticket.dbf' set ticket = ? where ticket = ?";

        public static string FindNextOrderNumberAndIncrement()
        {
            using (IDbConnection servmanConnection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
            {
                servmanConnection.Open();

                bool isTicketLocked = false;
                while (!isTicketLocked)
                {
                    using (IDbCommand dbCommand = Database.PrepareCommand(SqlLockTicket, servmanConnection))
                    {
                        isTicketLocked = (bool)dbCommand.ExecuteScalar();
                    }                    
                }

                string ticketNumberString;
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTicketNumber, servmanConnection))
                {
                    ticketNumberString = (string)dbCommand.ExecuteScalar();
                }

                int ticketNumber = int.Parse(ticketNumberString);

                using (IDbCommand dbCommand = Database.PrepareCommand(SqlIncrementAndSave, servmanConnection))
                {
                    Database.PutParameter(dbCommand, "@ticket", (ticketNumber + 1).ToString("000000"));
                    Database.PutParameter(dbCommand, "@ticket", ticketNumberString);                

                    dbCommand.ExecuteNonQuery();
                }

                return ticketNumberString;
            }
        }

        #endregion
    }
}
