using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class TrackingPhone
    {
        public LeadSourceTrackingPhone[] LeadSourceTrackingPhones { get; set; }

        public TrackingPhone()
        {
        }

        private const String SqlSelectByPhoneNumber = @"
SELECT * 
  FROM TrackingPhone
 WHERE Number = ?PhoneNumber; ";

        public static TrackingPhone GetByPhoneNumber(string phoneNumber, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByPhoneNumber, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneNumber", phoneNumber);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlSelectActive = @"
SELECT * 
  FROM TrackingPhone
 WHERE IsRemoved = 0 ; ";

        public static List<TrackingPhone> GetPhoneNumbers(IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectActive, connection))
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

        private const String SqlSelectByLeadSourceId = @"
SELECT * 
  FROM TrackingPhone p
    INNER JOIN LeadSourceTrackingPhone lsp ON p.Id = lsp.TrackingPhoneId
 WHERE lsp.LeadSourceId = ?LeadSourceId
ORDER BY TimeLastDisplay ; ";

        public static List<TrackingPhone> GetByLeadSourceId(int leadSourceId, IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByLeadSourceId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadSourceId", leadSourceId);

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

        private const String SqlSelectByLeadSourceIds = @"
SELECT * 
  FROM TrackingPhone p
    INNER JOIN LeadSourceTrackingPhone lsp ON p.Id = lsp.TrackingPhoneId
 WHERE lsp.LeadSourceId IN ( 0 {0} )
ORDER BY TimeLastDisplay ; ";

        public static List<TrackingPhone> GetByLeadSourceIds(int[] leadSourceIds, IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            var ids = string.Empty;
            foreach(var id in leadSourceIds)
            {
                ids += " , ";
                ids += id.ToString();
            }

            using (var dbCommand = Database.PrepareCommand(string.Format(SqlSelectByLeadSourceIds, ids), connection))
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

        private const String SqlSelectWebPhones = @"
SELECT * 
  FROM TrackingPhone
 WHERE IsWebPhone = 1 
 ORDER BY TimeLastWebAccess ; ";

        public static List<TrackingPhone> GetWebPhones(IDbConnection connection)
        {
            var result = new List<TrackingPhone>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectWebPhones, connection))
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
    }
}
      