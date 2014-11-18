using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class ActivityLog
    {
        public User RelatedUser { get; set; }
        
        public ActivityLog()
        {
        }

        private const string SqlSelectLast = @" 
SELECT * 
  FROM ActivityLog 
 ORDER BY DateCreated DESC 
";

        public static List<ActivityLog> GetLast(int limit, IDbConnection connection)
        {
            var sql = SqlSelectLast;
            if (limit > 0)
                sql += (" LIMIT " + limit.ToString());

            var result = new List<ActivityLog>();

            using (var dbCommand = Database.PrepareCommand(sql, connection))
            {
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        private const string SqlSelectByUserId = @" 
SELECT * 
  FROM ActivityLog 
 WHERE UserId = ?UserId
 ORDER BY DateCreated DESC 
";

        public static List<ActivityLog> GetByUserId(int userId, IDbConnection connection)
        {
            var result = new List<ActivityLog>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByUserId, connection))
            {
                Database.PutParameter(dbCommand, "?UserId", userId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }
    }
}
      