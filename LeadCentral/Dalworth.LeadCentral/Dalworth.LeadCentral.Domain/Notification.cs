using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;


namespace Dalworth.LeadCentral.Domain
{
    public partial class Notification
    {
        #region Constructor

        public Notification()
        {
        }

        #endregion

        #region Properties

        public string Type
        {
            get
            {
                var type = (NotificationTypeEnum)NotificationTypeId;
                return type.ToString();
            }
        }

        #endregion 

        #region Find 

        private const string SqlSelectByFilter = @"
        SELECT *
          FROM Notification
         WHERE 1=1
        ";

        public static List<Notification> Find(NotificationFilter filter, IDbConnection connection)
        {
            var sql = BuildFilterSql(SqlSelectByFilter, filter);

            using (var dbCommand = BuildFilterCommand(sql, filter, connection))
            {
                var result = new List<Notification>();

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }

                return result;
            }
        }

        private static IDbCommand BuildFilterCommand(string sql, NotificationFilter filter, IDbConnection connection)
        {
            var dbCommand = Database.PrepareCommand(sql, connection);

            if (filter != null)
            {
                if (filter.CreatedFrom != null)
                    Database.PutParameter(dbCommand, "?CreatedFrom", filter.CreatedFrom.Value);

                if (filter.CreatedTo != null)
                    Database.PutParameter(dbCommand, "?CreatedTo", filter.CreatedTo.Value);

                if (filter.IsProcessed != null)
                    Database.PutParameter(dbCommand, "?IsProcessed", filter.IsProcessed.Value ? 1 : 0);
            }

            return dbCommand;
        }

        private static string BuildFilterSql(string baseSql, NotificationFilter filter)
        {
            var result = baseSql;

            if (filter != null)
            {
                if (filter.CreatedFrom != null)
                    result += String.Format(" AND DateCreated > ?CreatedFrom ");

                if (filter.CreatedTo != null)
                    result += String.Format(" AND DateCreated < ?CreatedTo ");

                if (filter.IsProcessed != null)
                    result += String.Format(" AND IsProcessed = ?IsProcessed ");
            }

            result += " ORDER BY DateCreated DESC ";

            return result;
        }

        #endregion
    }

    public class NotificationFilter
    {
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public bool? IsProcessed { get; set; }
    }
}
      