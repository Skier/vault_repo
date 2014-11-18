using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class ddeflood
    {
        public ddeflood(){ }

        #region FindByTicket

        private const string SqlFindByTicket =
            @"select *  from [ddeflood] 
                where ticket_num = ?";

        public static List<ddeflood> FindByTicket(string ticketNumber, IDbConnection connection)
        {
            List<ddeflood> ddefloods = new List<ddeflood>();

            IDbCommand dbCommand;
            if (connection == null)
                dbCommand = Database.PrepareCommand(SqlFindByTicket, ConnectionKeyEnum.Servman);
            else
                dbCommand = Database.PrepareCommand(SqlFindByTicket, connection);

            using (dbCommand)
            {
                Database.PutParameter(dbCommand, "@ticket_num", ticketNumber);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ddefloods.Add(Load(dataReader));
                    }
                }
            }

            return ddefloods;
        }

        public static List<ddeflood> FindByTicket(string ticketNumber)
        {
            return FindByTicket(ticketNumber, null);
        }

        #endregion

        #region DeleteByTicket

        private const string SqlDeleteByTicket =
            @"delete from [ddeflood] 
                where ticket_num = ?";

        public static void DeleteByTicket(string ticketNumber)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByTicket, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@ticket_num", ticketNumber);
                dbCommand.ExecuteNonQuery();
            }            
        }

        #endregion
    }
}
      