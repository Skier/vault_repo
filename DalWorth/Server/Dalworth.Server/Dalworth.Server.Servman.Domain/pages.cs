using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class pages
    {
        #region Insert

        public static void Insert(pages pages, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                Database.PutParameter(dbCommand, "@phone", pages.phone);
                Database.PutParameter(dbCommand, "@max_baud", pages.max_baud);
                Database.PutParameter(dbCommand, "@d_start", pages.d_start);
                Database.PutParameter(dbCommand, "@t_start", pages.t_start);
                Database.PutParameter(dbCommand, "@d_end", pages.d_end);
                Database.PutParameter(dbCommand, "@t_end", pages.t_end);
                Database.PutParameter(dbCommand, "@pager_num", pages.pager_num);
                Database.PutParameter(dbCommand, "@message", pages.message);
                Database.PutParameter(dbCommand, "@response", pages.response);
                Database.PutParameter(dbCommand, "@status", pages.status);
                Database.PutParameter(dbCommand, "@seq_num", pages.seq_num);
                Database.PutParameter(dbCommand, "@ticket_num", pages.ticket_num);
                Database.PutParameter(dbCommand, "@count", pages.count);
                Database.PutParameter(dbCommand, "@station", pages.station);
                Database.PutParameter(dbCommand, "@email_dom", pages.email_dom);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion   

    }
}
      